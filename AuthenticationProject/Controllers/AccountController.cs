using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationProject.Controllers;


[Authorize]
public class AccountController : Controller
{
    [AllowAnonymous]
    public IActionResult Login()
    {
        return View();
    }
    
    public IActionResult AccessDenied()
    {
        return View();
    }
}
