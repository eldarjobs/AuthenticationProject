using AuthenticationProject.Dtos;
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
    //private readonly PasswordHasher<ApplicationUser> _passwordHasher;
    private readonly IPasswordValidator<ApplicationUser> _passwordValidator;

    public UsersController(
        UserManager<ApplicationUser> userManager,
        RoleManager<ApplicationRole> roleManager,
        IPasswordValidator<ApplicationUser> passwordValidator
        //PasswordHasher<ApplicationUser> passwordHasher
        )
    {
        this._userManager = userManager;
        this._roleManager = roleManager;
        this._passwordValidator = passwordValidator;
        //this._passwordHasher = passwordHasher;
    }

    #endregion

    public async Task<IActionResult> Index()
    {
        var users = await _userManager.Users.ToListAsync();
        return View(model: users);
    }


    public async Task<IActionResult> Create() => View();

    [HttpPost]
    public async Task<IActionResult> Create([Bind("UserName,Email,Password")] RegisterUserDto model)
    {
        if (ModelState.IsValid)
        {
            var user = new ApplicationUser
            {
                UserName = model.UserName,
                Email = model.Email
            };


            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                string defaultRole = "User";
                if (!await _roleManager.RoleExistsAsync(defaultRole))
                {
                    await _roleManager.CreateAsync(new ApplicationRole
                    {
                        Name = defaultRole
                    });
                }
                await _userManager.AddToRoleAsync(user, defaultRole);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
        }

        return View(model);
    }


}
