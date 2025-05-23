using EBIN.Data;
using Microsoft.AspNetCore.Mvc;

namespace EBIN.ViewComponents;

public class ProfilePicture: ViewComponent
{
    private readonly EBINContext _context;
    public ProfilePicture(EBINContext context)
    {
        _context = context;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var userName = HttpContext.User.Identity.Name;
        var user = _context.Profiles.SingleOrDefault(x => x.UserName == userName);
        
        var userProfilePicture = user?.ProfilePicturePath ?? "/pfps/default-user.png";
        var model = new
        {
            ProfilePicturePath = userProfilePicture,
            Profile = userName
        };
        return View("Default", model);
    }
}