using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Parad.DAL;
using Parad.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parad.Controllers
{
    public class TagController : Controller
    {
        private readonly AppDbContext _sql;
        private readonly UserManager<AppUser> _userManager;

        public TagController(AppDbContext sql,UserManager<AppUser> userManager) 
        {
            _sql = sql;
            _userManager = userManager;
        }
        public async Task<IActionResult> Create()
        {
            ViewBag.AppUser = await _userManager.FindByNameAsync(User.Identity.Name);
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Tag tag)
        {
            if (!ModelState.IsValid) return View();
            if (tag == null) return NotFound();
            if( _sql.Tags.Any(t => t.TagName == tag.TagName))
            {
                ModelState.AddModelError("","Bu adda tag movcuddur !!");
                return View();
            }
            await _sql.Tags.AddAsync(tag);
            await _sql.SaveChangesAsync();
            return RedirectToAction("AddImage","Image");
        }
    }
}
