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
    public class CommentController : Controller
    {
        private readonly AppDbContext _sql;

        public CommentController(AppDbContext sql)
        {
            _sql = sql;
        }
        public async Task<IActionResult> Index()
        {
            List<Comment> comments = await _sql.Comments.Include(c=>c.Image).Include(c=>c.User).ToListAsync();
            return View(comments);
        }
        public IActionResult Delete(int? id)
        {
            Comment comment = _sql.Comments.Find(id);
            _sql.Comments.Remove(comment);
            _sql.SaveChanges();
            return RedirectToAction("Index","Comment");
        }

        public async Task<IActionResult> ReportComment()
        {
            List<CommentReport> commentReports = await _sql.CommentReports.Include(c => c.ReasonReport).Include(c => c.User).Include(c=>c.Comment).ToListAsync();
            return View(commentReports);
        }
    }
}
