using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SMT_project_brand.Models;

namespace SMT_project_brand.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;

            _signInManager = signInManager;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(
            RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var existingUser =
                await _userManager.FindByNameAsync(
                    model.EmployeeId);

            if (existingUser != null)
            {
                ModelState.AddModelError(
                    "EmployeeId",
                    "This Employee ID is already registered.");

                return View(model);
            }

            var user = new ApplicationUser
            {
                EmployeeId = model.EmployeeId,

                FullName = model.FullName,

                UserName = model.EmployeeId
            };

            var result =
                await _userManager.CreateAsync(
                    user,
                    model.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(
                    user,
                    isPersistent: false);

                return RedirectToAction(
                    "Index",
                    "Home");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(
                    string.Empty,
                    error.Description);
            }

            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(
            string? returnUrl = null)
        {
            ViewBag.ReturnUrl = returnUrl;

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(
            LoginViewModel model,
            string? returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result =
                await _signInManager.PasswordSignInAsync(
                    model.EmployeeId,
                    model.Password,
                    false,
                    lockoutOnFailure: true);

            if (result.Succeeded)
            {
                if (!string.IsNullOrEmpty(returnUrl)
                    && Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }

                return RedirectToAction(
                    "Index",
                    "Home");
            }

            if (result.IsLockedOut)
            {
                ModelState.AddModelError(
                    string.Empty,
                    "Your account is locked. Please try again later.");

                return View(model);
            }

            ModelState.AddModelError(
                string.Empty,
                "Invalid Employee ID or password.");

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction(
                "Login",
                "Account");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}