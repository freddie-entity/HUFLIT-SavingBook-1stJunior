using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using test.Areas.Admin.ViewModels;
using test.Models;
using test.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace test.Areas.Admin.Controllers
{
    public class AccountController : Controller
    {
        
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        public IActionResult Index()
        {
            return View();
        }
        [AcceptVerbs("Get", "Post")]
        [AllowAnonymous]
        public async Task<IActionResult> IsUsernameInUse(string username)
        {
            var user = await userManager.FindByEmailAsync(username);
            if (user == null)
                return Json(true);
            return Json($"Username {username} is already in use!");
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModels model)
        {           
            if (ModelState.IsValid)
            {               

                var user = new AppUser { UserName = model.Username, Email = model.Username, IdS = model.IdS };
                var res = await userManager.CreateAsync(user, model.Password);

                if(res.Succeeded)
                {
                    if (signInManager.IsSignedIn(User) && User.IsInRole("Admin"))
                    {
                        return RedirectToAction("UserList", "Administration", new { area = "Admin"});
                        //return View("~/Admin/Views/Administration/UserList.cshtml");
                    }
                    else
                    {
                        await signInManager.SignInAsync(user, isPersistent: false); //+ ispersistent false de luu cookie trong session thoi
                        return RedirectToAction("Index", "Home");
                    }
                }
                foreach (var error in res.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }
        
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index","Home","Views");
        }
        [HttpGet]       
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModels model)
        {
            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }
            var _user = await userManager.FindByEmailAsync("Admin");
            if (_user == null)
            {
                //Here you could create the super admin who will maintain the web app
                var poweruser = new AppUser
                {
                    UserName = "Admin",
                    Email = "Admin",
                };
                string adminPassword = "123";

                var createPowerUser = await userManager.CreateAsync(poweruser, adminPassword);
                if (createPowerUser.Succeeded)
                {
                    //here we tie the new user to the role
                    await userManager.AddToRoleAsync(poweruser, "Admin");

                }
            }
            if (ModelState.IsValid)
            {

                var res = await signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, false);

                if (res.Succeeded)
                {  
                        return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Login Failed");
            }
            return View(model);
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View("~/Views/Shared/AccessDenied.cshtml");
        }
    }
}
