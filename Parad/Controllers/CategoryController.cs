using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Parad.DAL;
using Parad.Models;
using Parad.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parad.Controllers
{
    public class CategoryController : Controller
    {
        private readonly AppDbContext _sql;
        private readonly UserManager<AppUser> _userManager;

        public CategoryController(AppDbContext sql,UserManager<AppUser> userManager)
        {
            _sql = sql;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            CategoriesVM categories = new CategoriesVM
            {
                AppUser=await _userManager.FindByNameAsync(User.Identity.Name),
                Categories = await _sql.Categories.ToListAsync(),
                Images = await _sql.Images.Include(i => i.User).ToListAsync()
            };
            return View(categories);
        }
        public async Task<IActionResult> GetCategory(int? id)
        {
            GetCategoryVM getCategoryVM = new GetCategoryVM
            {
                AppUser = await _userManager.FindByNameAsync(User.Identity.Name),
                Images = await _sql.Images.Include(i => i.User).Where(i=>i.CategoryId==id).ToListAsync(),
                Category = await _sql.Categories.FindAsync(id),
                Likes = await _sql.Likes.ToListAsync()
            };
            return View(getCategoryVM);
        }
    }
}
