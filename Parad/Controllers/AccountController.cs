using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Parad.DAL;
using Parad.Extentions;
using Parad.Models;
using Parad.ViewModels;
using System.Threading.Tasks;

namespace Parad.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _sql;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IWebHostEnvironment _env;

        public AccountController(AppDbContext sql, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IWebHostEnvironment env)
        {
            _sql = sql;
            _userManager = userManager;
            _signInManager = signInManager;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            AccountVM accountVM = new AccountVM
            {
                AppUser = await _userManager.FindByNameAsync(User.Identity.Name),
                Images = await _sql.Images.Include(i => i.User).ToListAsync(),
                Likes = await _sql.Likes.ToListAsync()
            };
            return View(accountVM);
        }

        public async Task<IActionResult> EditUser(string? id)
        {
            if (id == null) return NotFound();
            AppUser user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == id);
            return View(user);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUser(AppUser user, string? id)
        {
            if (!ModelState.IsValid) return NotFound();
            AppUser existUser = await _userManager.FindByIdAsync(id);
            if (existUser == null) return NotFound();
            if (user.ProfileImageFile != null)
            {
                if (!user.ProfileImageFile.IsImage())
                {
                    ModelState.AddModelError("ProfileImageFile", "Sekilin formati duzgun deyil!!");
                    return View();
                }
                if (!user.ProfileImageFile.IsSizeOk(5))
                {
                    ModelState.AddModelError("ProfileImageFile", "Sekil 5 mb-dan boyuk ola bilmez!!");
                    return View();
                }
                if (existUser.ProfileImage == "default-profile-img.jpg")
                {
                    existUser.ProfileImage = user.ProfileImageFile.SavaImage(_env.WebRootPath, "assets/images");
                }
                else
                {
                    Helpers.Helper.DeleteImg(_env.WebRootPath, "assets/images", existUser.ProfileImage);
                    existUser.ProfileImage = user.ProfileImageFile.SavaImage(_env.WebRootPath, "assets/images");
                }
            }
            else
            {
                user.ProfileImage = existUser.ProfileImage;
            }
            existUser.FirstName = user.FirstName;
            existUser.LastName = user.LastName;
            existUser.UserName = user.UserName;
            existUser.Email = user.Email;
            //if ()
            //{

            //}
            IdentityResult result = await _userManager.RemovePasswordAsync(existUser);
            if (result.Succeeded)
            {
                result = await _userManager.AddPasswordAsync(existUser, user.PasswordHash);
                if (!result.Succeeded)
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("PasswordHash", item.Description);
                        return View();
                    }
                }
            }

            await _userManager.UpdateAsync(existUser);
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Auth");
        }

        //User's Favorites list
        public async Task<IActionResult> Favorites()
        {
            FavoritesVM favoritesVM = new FavoritesVM
            {
                User = await _userManager.FindByNameAsync(User.Identity.Name),
                Favorites = await _sql.Favorites.Include(f => f.User).Include(f => f.Image).ThenInclude(i => i.User).ToListAsync(),
                //Images = await _sql.Images.Where();
            };
            return View(favoritesVM);
        }
    }
}
