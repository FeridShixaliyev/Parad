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
    public class CommentController : Controller
    {
        private readonly AppDbContext _sql;
        private readonly UserManager<AppUser> _userManager;

        public CommentController(AppDbContext sql, UserManager<AppUser> userManager)
        {
            _sql = sql;
            _userManager = userManager;
        }
        public async Task<IActionResult> CommentReports(int? id)
        {
            Comment comment = await _sql.Comments.FindAsync(id);
            AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
            CommentReportVM commentReportVM = new CommentReportVM
            {
                AppUser=user,
                Comment=comment,
                CommentReport=new CommentReport(),
                ReasonReports=await _sql.ReasonReports.ToListAsync()
            };
            return View(commentReportVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CommentReports(int? id,CommentReport commentReport)
        {
            if (!ModelState.IsValid) return View();
            Comment comment = await _sql.Comments.FindAsync(id);
            if (comment == null) return NotFound();
            AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (comment == null) return NotFound();
            commentReport.User = user;
            commentReport.UserId = user.Id;
            commentReport.Comment = comment;
            commentReport.CommentId = comment.Id;
            await _sql.CommentReports.AddAsync(commentReport);
            await _sql.SaveChangesAsync();
            return RedirectToAction("Index","Home");
        }


        public async Task<IActionResult> DeleteComment(int? id)
        {
            Comment comment = await _sql.Comments.FindAsync(id);
            if (comment == null) return NotFound();
            _sql.Comments.Remove(comment);
            await _sql.SaveChangesAsync();
            return RedirectToAction("Index","Home");
        }
    }
}
