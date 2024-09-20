using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using QueryBase.Areas.Users.Controllers;
using QueryBase.Models;
using QueryBase.Models.ViewModels;
using QueryBase.Utility;
using System.Reflection.Metadata.Ecma335;

namespace QueryBase.Areas.Identity.Controllers
{
    [Area("Identity")]
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<ApplicationRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVM, string? ReturnUrl)
        {
            if(!ModelState.IsValid)
            {
                ViewBag.Errors = ModelState.Values.SelectMany(temp => temp.Errors).Select(temp => temp.ErrorMessage);
                return View(registerVM);
            }

            ApplicationUser user = new ApplicationUser()
            {
                Email = registerVM.Email,
                UserName = registerVM.UserName,
                PhoneNumber = registerVM.PhoneNumber,
            };

            IdentityResult result = await _userManager.CreateAsync(user, registerVM.Password);          //add user to db

            if (result.Succeeded)
            {
                //check status of radio button
                if(registerVM.UserType == UserTypeOptions.Admin)
                {
                    //create 'Admin' role
                    if(await _roleManager.FindByNameAsync(UserTypeOptions.Admin.ToString()) is null)
                    {
                        ApplicationRole applicationRole = new ApplicationRole() { Name = UserTypeOptions.Admin.ToString() }; 
                        await _roleManager.CreateAsync(applicationRole);
                    }

                    // add the new user into 'Admin' role
                    await _userManager.AddToRoleAsync(user, UserTypeOptions.Admin.ToString());
                }
                else
                {
                    //create 'User' role
                    if (await _roleManager.FindByNameAsync(UserTypeOptions.User.ToString()) is null)
                    {
                        ApplicationRole applicationRole = new ApplicationRole() { Name = UserTypeOptions.User.ToString() };
                        await _roleManager.CreateAsync(applicationRole);
                    }

                    // add the new user into 'User' role
                    await _userManager.AddToRoleAsync(user, UserTypeOptions.User.ToString());
                }

                //signin
                await _signInManager.SignInAsync(user, isPersistent: false);
                if(!ReturnUrl.IsNullOrEmpty() && Url.IsLocalUrl(ReturnUrl))
                {
                    return LocalRedirect(ReturnUrl);
                }
                return RedirectToAction(nameof(HomeController.Index), "Home", new { area = "Users" });
            }
            else
            {
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("Register", error.Description);
                }
                return View(registerVM);
            }

        }


        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM, string? ReturnUrl)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Errors =  ModelState.Values.SelectMany(temp => temp.Errors).Select(temp => temp.ErrorMessage);
                return View(loginVM);
            }

            var result = await _signInManager.PasswordSignInAsync(loginVM.UserName, loginVM.Password, isPersistent: false, lockoutOnFailure: false);
            
            if(result.Succeeded)
            {
                if(!ReturnUrl.IsNullOrEmpty() && Url.IsLocalUrl(ReturnUrl)){
                    return LocalRedirect(ReturnUrl);
                }
                return RedirectToAction(nameof(HomeController.Index), "Home", new { area = "Users" });
            }

            ModelState.AddModelError("Login", "Invalid UserName or Password.");
            return View(loginVM);

        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(HomeController.Index), "Home", new {area= "Users"});
        }

        


        public async Task<IActionResult> ValidateUserName(string userName)
        {
            ApplicationUser? user = await _userManager.FindByNameAsync(userName);

            if(user == null)
            {
                return Json(true);
            }
            else
            {
                return Json(false);
            }
        }
    }
}
