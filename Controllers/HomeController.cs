using EBIN.Data;
using EBIN.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;

namespace EBIN.Controllers
{
    public class HomeController : Controller
    {
        private readonly EBINContext _context;
            
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, EBINContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var posts = _context.Posts.ToList();
            
            return View(posts);
        }

        public IActionResult Profile()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
