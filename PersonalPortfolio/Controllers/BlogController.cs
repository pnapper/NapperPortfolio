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

namespace PersonalPortfolio.Controllers
{

    public class BlogController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public BlogController(UserManager<ApplicationUser> userManager, ApplicationDbContext db)
        {
            _userManager = userManager;
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currentUser = await _userManager.FindByIdAsync(userId);
            return View(_db.BlogPosts.Where(x => x.User.Id == currentUser.Id));
        }

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(BlogPost blogpost)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currentUser = await _userManager.FindByIdAsync(userId);
            blogpost.User = currentUser;
            _db.BlogPosts.Add(blogpost);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            var thisBlogPost = _db.BlogPosts
                                  .Include(x => x.Comments)
                                  .FirstOrDefault(items => items.BlogPostId == id);
            return View(thisBlogPost);
        }

        public IActionResult Edit(int id)
        {
            var thisBlogPost = _db.BlogPosts.FirstOrDefault(names => names.BlogPostId == id);
            return View(thisBlogPost);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(BlogPost blogpost)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currentUser = await _userManager.FindByIdAsync(userId);
            blogpost.User = currentUser;
            _db.BlogPosts.Update(blogpost);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var thisBlogPost = _db.BlogPosts.FirstOrDefault(names => names.BlogPostId == id);
            return View(thisBlogPost);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var thisBlogPost = _db.BlogPosts.FirstOrDefault(names => names.BlogPostId == id);
            _db.Remove(thisBlogPost);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}