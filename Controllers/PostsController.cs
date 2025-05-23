using Microsoft.AspNetCore.Mvc;
using EBIN.Models;
using EBIN.Data;
using Microsoft.AspNetCore.Authorization;

namespace EBIN.Controllers
{
    [Authorize]
    public class PostsController : Controller
    {

        private readonly EBINContext _context;

        public PostsController(EBINContext context)
        {
            _context = context;
        }

        
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create([Bind("Name, Description")] Posts Post, IFormFile File)
        {
            var userName = HttpContext.User.Identity.Name;
            var user = _context.Profiles.SingleOrDefault(x => x.UserName == userName);
            if (ModelState.IsValid)
            {
                if (File != null && File.Length > 0)
                {
                    var pathToFile = Path.Combine("/uploads", File.FileName);
                    var filePath = Path.Combine("wwwroot/uploads", File.FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        File.CopyTo(stream);
                    }
                    Post.PathToFile = pathToFile;
                    Post.Profile = user;
                    Post.ProfilesID = user.Id;
                    _context.Posts.Add(Post);
                    _context.SaveChanges();
                    return RedirectToAction("Index", "Home");
                }
            }
            return BadRequest(ModelState);

        }


    }
}
