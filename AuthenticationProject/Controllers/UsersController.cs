using AuthenticationProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationProject.Controllers;

public class UsersController : Controller
{
    #region Constructor
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly PasswordValidator<ApplicationUser> _passwordValidator;

    public UsersController(
        UserManager<ApplicationUser> userManager,
        RoleManager<ApplicationRole> roleManager,
        PasswordValidator<ApplicationUser> passwordValidator)
    {
        this._userManager = userManager;
        this._roleManager = roleManager;
        this._passwordValidator = passwordValidator;
    }

    #endregion

    public async Task<IActionResult> Index()
    {
        var users = await _userManager.Users.ToListAsync();
        return View(model: users);
    }
}
