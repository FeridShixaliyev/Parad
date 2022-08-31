using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Parad.DAL;
using Parad.Extentions;
using Parad.Models;
using Parad.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parad.Controllers
{
    [Authorize]
    public class ImageController : Controller
    {
        private readonly AppDbContext _sql;
        private readonly IWebHostEnvironment _env;
        private readonly UserManager<AppUser> _userManager;

        public ImageController(AppDbContext sql, UserManager<AppUser> userManager, IWebHostEnvironment env)
        {
            _sql = sql;
            _env = env;
            _userManager = userManager;
        }

        public async Task<IActionResult> AddImage()
        {
            ViewBag.AppUser = await _userManager.FindByNameAsync(User.Identity.Name);
            ViewBag.Categories = await _sql.Categories.ToListAsync();
            ViewBag.Tags = await _sql.Tags.ToListAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddImage(ImageAndTagsVM imageAndTagsVM)
        {
            //return Json(imageAndTagsVM.TagIds.Count);
            if (!ModelState.IsValid) return View();
            if (imageAndTagsVM == null) return NotFound();
            if (imageAndTagsVM.TagIds.Count > 3)
            {
                ModelState.AddModelError("TagIds", "Max 3 tag sece bilersiz !!");
                return View();
            }
            var image = imageAndTagsVM.Image;
            if (image.ImageFile != null)
            {
                if (!image.ImageFile.IsImage())
                {
                    ModelState.AddModelError("", "Sekilin formati duzgun deyil!!");
                    return View();
                }
                if (!image.ImageFile.IsSizeOk(5))
                {
                    ModelState.AddModelError("", "Sekil 5 mb-dan boyuk ola bilmez!!");
                    return View();
                }
                image.ImageStr = image.ImageFile.SavaImage(_env.WebRootPath, "assets/images");
            }
            image.User = await _userManager.FindByNameAsync(User.Identity.Name);
            image.DownloadDate = DateTime.UtcNow;


            //Create Like object

            Like like = new Like
            {
                LikeCount = 0,
                Image = image,
                ImageId = image.Id

            };

            

            await _sql.Images.AddAsync(image);
            await _sql.Likes.AddAsync(like);
            await _sql.SaveChangesAsync();

            //Create TagImages

            foreach (var item in imageAndTagsVM.TagIds)
            {
                TagImage tagImage = new TagImage
                {
                    ImageId = image.Id,
                    TagId = item
                };
                await _sql.TagImages.AddAsync(tagImage);
            }
            await _sql.SaveChangesAsync();
            return RedirectToAction("Index", "Account");
        }

        //Image like system

        public async Task<int> Like(int? id)
        {
            if (id == null) return 404;
            Like like = await _sql.Likes.FirstOrDefaultAsync(l => l.ImageId == id);
            AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);

            List<LikeUser> likeUsers = await _sql.LikeUsers.Where(lu => lu.LikeId == like.Id).ToListAsync();
            foreach (var item in likeUsers)
            {
                if (item.UserId == user.Id)
                {
                    like.LikeCount--;
                    _sql.LikeUsers.Remove(item);
                    await _sql.SaveChangesAsync();
                    return like.LikeCount;
                }

            }

            LikeUser likeUser = new LikeUser
            {
                User = user,
                UserId = user.Id,
                Like = like,
                LikeId = like.Id
            };

            like.LikeCount++;
            
            await _sql.LikeUsers.AddAsync(likeUser);
            await _sql.SaveChangesAsync();
            return like.LikeCount; 
        }
        public async Task<IActionResult> DeleteImage(int? id)
        {
            Image image = await _sql.Images.FindAsync(id);
            if (image == null) return NotFound();
            Helpers.Helper.DeleteImg(_env.WebRootPath,"assets/images",image.ImageStr);
            _sql.Images.Remove(image);
            await _sql.SaveChangesAsync();
            return RedirectToAction("Index", "Account");
        }
        public async Task<IActionResult> AddFavorites(int? id)
        {
            Image image = await _sql.Images.FindAsync(id);
            AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);

            List<Favorite> favorites = await _sql.Favorites.Where(f => f.ImageId == image.Id).ToListAsync();

            foreach (var item in favorites)
            {
                if (item.UserId == user.Id)
                {
                    _sql.Favorites.Remove(item);
                    await _sql.SaveChangesAsync();
                    return RedirectToAction("Index", "Home");
                }
            }
            Favorite favorite = new Favorite
            {
                User = user,
                UserId = user.Id,
                Image = image,
                ImageId = image.Id
            };
            await _sql.Favorites.AddAsync(favorite);
            await _sql.SaveChangesAsync();
            return RedirectToAction("Index", "Home");

        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            DetailsVM details = new DetailsVM
            {
                User = await _userManager.FindByNameAsync(User.Identity.Name),
                Image = await _sql.Images.Include(i => i.User).FirstOrDefaultAsync(i => i.Id == id),
                Images = await _sql.Images.Include(i => i.User).ToListAsync(),
                TagImages=await _sql.TagImages.Include(t=>t.Tag).Where(t=>t.ImageId==id).ToListAsync(),
                Comments = await _sql.Comments.Where(c=>c.ImageId==id).Include(c=>c.User).ToListAsync(),
                Like=await _sql.Likes.Where(l=>l.ImageId==id).FirstOrDefaultAsync(),
                Comment = new Comment()
            };
            return View(details);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Details(int? id,Comment comment)
        {
            if (id == null) return NotFound();
            Image image = await _sql.Images.FindAsync(id);
            AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
            comment.User = user;
            comment.UserId = user.Id;
            comment.Image = image;
            comment.ImageId = image.Id;
            comment.CommentDate = DateTime.UtcNow;
            await _sql.Comments.AddAsync(comment);
            await _sql.SaveChangesAsync();
            return RedirectToAction("Index","Home");
        }

    }
}
