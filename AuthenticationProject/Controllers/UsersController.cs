using AuthenticationProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationProject.Controllers;

public class UsersController : Controller
{
    #region Constructor
    private readonly UserManager<ApplicationUser> _userManager;
    //private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly PasswordHasher<ApplicationUser> _passwordHasher;

    public UsersController(PasswordHasher<ApplicationUser> passwordHasher)
    {
        _passwordHasher = passwordHasher;
    }

    private readonly PasswordValidator<ApplicationUser> _passwordValidator;

    public UsersController(
        UserManager<ApplicationUser> userManager,
        //RoleManager<ApplicationRole> roleManager,
        PasswordValidator<ApplicationUser> passwordValidator,
        PasswordHasher<ApplicationUser> passwordHasher)
    {
        this._userManager = userManager;
        //this._roleManager = roleManager;
        this._passwordValidator = passwordValidator;
        this._passwordHasher = passwordHasher;
    }

    #endregion

    public async Task<IActionResult> Index()
    {
        var users = await _userManager.Users.ToListAsync();
        return View(model: users);
    }
}
