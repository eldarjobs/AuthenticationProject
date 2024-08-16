using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationProject.Areas.Admin.Controllers;


[Area("Admin")]
//[Authorize]
public class AccountController : Controller
{
    //[AllowAnonymous]
    public IActionResult Login()
    {
        return View();
    }
    
    public IActionResult AccessDenied()
    {
        return View();
    }
}
