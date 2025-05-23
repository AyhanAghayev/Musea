using Microsoft.AspNetCore.Mvc;
using System.Linq;
using EBIN.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Azure.Identity;
using EBIN.Models;

namespace EBIN.Controllers;

public class ProfilesController : Controller
{
    private readonly EBINContext _context;

    public ProfilesController(EBINContext context)
    {
        _context = context;
    }
    public IActionResult Index(string? userName)
    {
        if(userName == null) {
            return RedirectToAction("Register", "Auth");
        }

        var user = _context.Profiles
            .Include(p => p.Post)
            .Include(p => p.Followers)
            .Include(p => p.Following)
            .SingleOrDefault(x => x.UserName == userName);
        return View(user);
    }

    [HttpPost]
    [Route("Profiles/Follow/{userName}")]
    public IActionResult Follow(string? userName) 
    {
        var name = HttpContext.User.Identity?.Name;
        if(userName == null || userName == name || name == null) {
            return BadRequest();
        }
        Console.WriteLine(userName, name);

        var userToFollow = _context.Profiles
            .SingleOrDefault(x => x.UserName == userName);

        var user = _context.Profiles.SingleOrDefault(x => x.UserName == name);
        
        if (userToFollow == null || user == null) {
            return BadRequest();
        }
        bool alreadyFollowing = _context.ProfileFollowers.Any(pf =>
        pf.FollowerId == user.Id && pf.FollowingId == userToFollow.Id);

        if (!alreadyFollowing) {

            var follow = new ProfileFollowers{
                FollowerId = user.Id,
                FollowingId = userToFollow.Id
            };

            _context.ProfileFollowers.Add(follow);
        }

        _context.SaveChanges();
        return RedirectToAction("Index", new {userName = name});
    }
}