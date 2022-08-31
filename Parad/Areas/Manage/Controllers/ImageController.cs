using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Parad.DAL;
using Parad.Models;
using Parad.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parad.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize(Roles = "Admin,Moderator")]
    public class ImageController : Controller
    {
        private readonly AppDbContext _sql;
        private readonly IWebHostEnvironment _env;

        public ImageController(AppDbContext sql, IWebHostEnvironment env)
        {
            _sql = sql;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            GetCategoryVM getCategoryVM = new GetCategoryVM
            {
                Images = await _sql.Images.Include(i => i.User).ToListAsync(),
                Likes = await _sql.Likes.Include(l=>l.Image).ToListAsync(),
            };
            return View(getCategoryVM);
        }


        public async Task<IActionResult> DeleteImage(int? id)
        {
            Image image = await _sql.Images.FindAsync(id);
            if (image == null) return NotFound();
            Helpers.Helper.DeleteImg(_env.WebRootPath, "assets/images", image.ImageStr);
            _sql.Images.Remove(image);
            await _sql.SaveChangesAsync();
            return RedirectToAction("Index", "Account");
        }
    }
}
