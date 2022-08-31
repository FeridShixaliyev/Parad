using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Parad.DAL;
using Parad.Models;
using Parad.ViewModels;
using System.Threading.Tasks;

namespace Parad.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _sql;
        private readonly UserManager<AppUser> _userManager;

        public HomeController(AppDbContext sql, UserManager<AppUser> userManager)
        {
            _sql = sql;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            HomeVM homeVM = new HomeVM
            {

                AppUser = await _userManager.Users.ToListAsync(),
                Sliders=await _sql.Sliders.ToListAsync(),
                Images = await _sql.Images.Include(i => i.User).ToListAsync(),
                Likes = await _sql.Likes.ToListAsync(),
                Categories = await _sql.Categories.ToListAsync()
            };
            return View(homeVM);
        }
        public async Task<IActionResult> About()
        {
            ViewBag.AppUser = await _userManager.FindByNameAsync(User.Identity.Name);
            return View();
        }
    }
}
