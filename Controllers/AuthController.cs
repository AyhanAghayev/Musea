using Azure.Identity;
using EBIN.Data;
using EBIN.Models;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace EBIN.Controllers;

public class AuthController : Controller
{
    private readonly EBINContext _context;

    public AuthController(EBINContext context)
    {
        _context = context;
    }
    public IActionResult Login()
    {
        return View();
    }
    
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }

    [HttpPost]
    public async Task<IActionResult> Login([Bind("UserName", "Password")] Profiles profile)
    {
        if (!ValidateCredentials(profile.UserName, profile.Password))
        {
            return RedirectToAction("Login", "Auth");
        };
        var user = _context.Profiles.Single(x => x.UserName == profile.UserName);

        await Authenticate(user);
        return RedirectToAction("Index", "Home");
    }

    public IActionResult Register()
    {
        return View();
    }
    
    [HttpPost]
    public IActionResult Register([Bind("UserName", "Password")] Profiles profile)
    {
        _context.Profiles.Add(profile);
        _context.SaveChanges();
        return  RedirectToAction("Login", "Auth");
    }

    private bool ValidateCredentials(string userName, string password)
    {
        var user = _context.Profiles
            .Where(x => x.UserName == userName)
            .Where(x => x.Password == password)
            .SingleOrDefault();
        
        return user != null;
    }

    private async Task Authenticate(Profiles profile)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, profile.UserName),
            new Claim("UserId", profile.Id.ToString()),
        };

        var identity = new ClaimsIdentity(claims, authenticationType: "Cookie");
        var principal = new ClaimsPrincipal(identity);

        await HttpContext.SignInAsync(principal);

    }
}