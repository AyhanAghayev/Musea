using EBIN.Data;
using Microsoft.AspNetCore.Mvc;

namespace EBIN.Controllers;

public class AccountController : Controller
{
    private readonly EBINContext _context;

    public AccountController(EBINContext context)
    {
        _context = context;
    }
    
    public IActionResult ChangePfp(IFormFile File)
    {
        var userName = HttpContext.User.Identity.Name;
        
        if (File != null && File.Length > 0)
        {
            var pathToFile = Path.Combine("/pfps", File.FileName);
            var filePath = Path.Combine("wwwroot/pfps", File.FileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                File.CopyTo(stream);
            }

            var user = _context.Profiles.SingleOrDefault(p => p.UserName == userName);
            if (user != null)
            {
                user.ProfilePicturePath = pathToFile;
                _context.SaveChanges();
            }
            return RedirectToAction("Index", "Home");
        }

        return BadRequest();
    }
}