using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Parad.DAL;
using Parad.Extentions;
using Parad.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parad.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize(Roles = "Admin,Moderator")]
    public class CategoryController : Controller
    {
        private readonly AppDbContext _sql;
        private readonly IWebHostEnvironment _env;

        public CategoryController(AppDbContext sql,IWebHostEnvironment env)
        {
            _sql = sql;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            List<Category> categories = await _sql.Categories.ToListAsync();
            return View(categories);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost] 
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category)
        {
            if (!ModelState.IsValid) return View();
            if(_sql.Categories.Any(c => c.CategoryName == category.CategoryName))
            {
                ModelState.AddModelError("", "Bu adda Category movcuddur");
                return View();
            }
            if (category.DescriptionImageFile != null)
            {
                if (!category.DescriptionImageFile.IsImage())
                {
                    ModelState.AddModelError("DescriptionImageFile", "Sekilin formati duzgun deyil !!");
                    return View();
                }
                if (!category.DescriptionImageFile.IsSizeOk(5))
                {
                    ModelState.AddModelError("DescriptionImageFile", "Sekil 5 mb-dan boyuk ola bilmez !!");
                    return View();
                }
                category.DescriptionImage = category.DescriptionImageFile.SavaImage(_env.WebRootPath,"assets/images");
            }
            await _sql.Categories.AddAsync(category);
            await _sql.SaveChangesAsync();
            return RedirectToAction("Index","Category");
        }
        public IActionResult Edit(int? id)
        {
            Category category =  _sql.Categories.Find(id);
            return View(category);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id,Category category)
        {
            if (!ModelState.IsValid) return View();
            if(_sql.Categories.Any(c => c.CategoryName == category.CategoryName))
            {
                ModelState.AddModelError("", "Bu adda Category movcuddur");
                return View();
            }
            Category existCategory = await _sql.Categories.FindAsync(id);
            if (existCategory == null) return NotFound();
            if (category.DescriptionImageFile != null)
            {
                if (!category.DescriptionImageFile.IsImage())
                {
                    ModelState.AddModelError("DescriptionImageFile", "Sekilin formati duzgun deyil !!");
                    return View();
                }
                if (!category.DescriptionImageFile.IsSizeOk(5))
                {
                    ModelState.AddModelError("DescriptionImageFile", "Sekil 5 mb-dan boyuk ola bilmez !!");
                    return View();
                }
                Helpers.Helper.DeleteImg(_env.WebRootPath,"assets/images",existCategory.DescriptionImage);
                existCategory.DescriptionImage = category.DescriptionImageFile.SavaImage(_env.WebRootPath,"assets/images");
            }
            else
            {
                ModelState.AddModelError("","Category-nin Description Image-in secin!!");
                return View();
            }
            existCategory.CategoryName = category.CategoryName;
            await _sql.SaveChangesAsync();
            return RedirectToAction("Index", "Category");
        }
        public async Task<IActionResult> Delete(int? id)
        {
            Category category = await _sql.Categories.FindAsync(id);
            if (category == null) return NotFound();
            _sql.Categories.Remove(category);
            await _sql.SaveChangesAsync();
            return RedirectToAction("Index", "Category");
        }
    }
}
