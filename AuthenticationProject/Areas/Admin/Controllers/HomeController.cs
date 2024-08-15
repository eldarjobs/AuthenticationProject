using Microsoft.AspNetCore.Mvc;

namespace AuthenticationProject.Areas.Admin.Controllers;

[Area("Admin")]
public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
