using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using PersonalPortfolio.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PersonalPortfolio.Controllers
{
    [Authorize]
    public class CommentController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public CommentController(UserManager<ApplicationUser> userManager, ApplicationDbContext db)
        {
            _userManager = userManager;
            _db = db;
        }

        private ApplicationDbContext db = new ApplicationDbContext();

        public IActionResult Index()
        {
            return View(_db.Comments.Include(x => x.BlogPost).ToList());
        }



        public IActionResult Create()
        {
            ViewBag.BlogPostId = new SelectList(_db.BlogPosts, "BlogPostId", "Title");
            return View();
        }

        [HttpPost]
        public IActionResult Create(string Author, string Body, int BlogPostId)
        {
            var newComment = new Comment(Author, Body, BlogPostId);

            if (ModelState.IsValid)
            {
                _db.Comments.Add(newComment);
                _db.SaveChanges();
            }

            return Json(newComment);
        }

        [HttpPost]
        public IActionResult NewComment(string Author, string Body, int BlogPostId)
        {
            Comment newComment = new Comment(Author, Body, BlogPostId);
            _db.Comments.Add(newComment);
            _db.SaveChanges();
            return Json(newComment);
        }


        public IActionResult Details(int id)
        {
            var thisBlogPost = _db.BlogPosts
                                  .Include(x => x.BlogPostId)
                                  .FirstOrDefault(items => items.BlogPostId == id);
            return View(thisBlogPost);
        }

        public IActionResult Delete(int id)
        {
            var thisComment = _db.Comments.FirstOrDefault(names => names.CommentId == id);
            return View(thisComment);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var thisComment = _db.Comments.FirstOrDefault(names => names.CommentId == id);
            _db.Remove(thisComment);
            _db.SaveChanges();
            return RedirectToAction("Details", "Blog", new { id = thisComment.BlogPostId });
        }

    }
}