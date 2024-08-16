using AuthenticationProject.Dtos;
using AuthenticationProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AuthenticationProject.Controllers
{
    [Authorize] // Uncomment this if you want to require authentication for all actions in this controller
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
       

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginUserDto model, [FromQuery] string? returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.UserName) ??
                            await _userManager.FindByNameAsync(model.UserName);

                if (user != null)
                {
                    await _signInManager.SignOutAsync();

                    var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe,  false);

                    bool islockout = result.IsLockedOut;


                    if (result.Succeeded)
                    {
                        if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
                        {

                            return Redirect(returnUrl ?? "/");
                        }

                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        //string errors = "";
                        //foreach(var error in result.)
                        //{
                        //   errors += error.Description;
                        //}
                        ModelState.AddModelError(string.Empty, "Invalid login attempt. Please check your  Username and Password.");
                    }
                }
                
            }
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }

        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
