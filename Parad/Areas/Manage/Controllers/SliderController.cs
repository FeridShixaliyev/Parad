using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Parad.DAL;
using Parad.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parad.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize(Roles = "Admin,Moderator")]
    public class SliderController : Controller
    {
        private readonly AppDbContext _sql;

        public SliderController(AppDbContext sql)
        {
            _sql = sql;
        }
        public async Task<IActionResult> Index()
        {
            List<Slider> sliders = await _sql.Sliders.ToListAsync();
            return View(sliders);
        }
         public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Slider slider)
        {
            if (!ModelState.IsValid) return View();
            if (slider == null) return NotFound();
            await _sql.Sliders.AddAsync(slider);
            await _sql.SaveChangesAsync();
            return RedirectToAction("Index","Slider");
        }

        public async Task<IActionResult> Edit(int? id)
        {
            Slider slider = await _sql.Sliders.FindAsync(id);
            if (slider == null) return NotFound();
            return View(slider);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id,Slider slider)
        {
            if (!ModelState.IsValid) return View();
            Slider existSlider = await _sql.Sliders.FindAsync(id);
            if (slider == null) return NotFound();
            existSlider.Title = slider.Title;
            existSlider.Content = slider.Content;
            await _sql.SaveChangesAsync();
            return RedirectToAction("Index","Slider");
        }
        public async Task<IActionResult> Delete(int? id)
        {
            Slider slider = await _sql.Sliders.FindAsync(id);
            if (slider == null) return NotFound();
            _sql.Sliders.Remove(slider);
            await _sql.SaveChangesAsync();
            return RedirectToAction("Index","Slider");

        }
    }
}
