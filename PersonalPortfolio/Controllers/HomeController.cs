using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PersonalPortfolio.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PersonalPortfolio.Controllers
{
    public class HomeController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            //return View();
            var projects = GitHubProject.GetProjects();
            return View(projects);
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Project()
        {
            var projects = GitHubProject.GetProjects();
            return View(projects);
        }
    }
}
