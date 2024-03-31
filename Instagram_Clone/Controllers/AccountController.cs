using Instagram_Clone.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Instagram_Clone.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        //Register
        public IActionResult Register()
        {
            return View("Register");
        }
        [HttpPost]
        public async Task<IActionResult> SaveRegister(RegisterViewModel userVM)
        {
            if (ModelState.IsValid) {
                ApplicationUser user = new ApplicationUser()
                {
                    UserName = userVM.UserName,
                    Email = userVM.Email,
                    FirstName = userVM.FirstName,
                    LastName = userVM.LastName,
                    PasswordHash = userVM.Password
                };
                //Save
               IdentityResult result= await userManager.CreateAsync(user ,userVM.Password);
                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user,false);
                    return RedirectToAction("Login");
                }
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, item.Description);
                }
                //save db add

            }
            return View("Register", userVM);
        }
        //Login
        public IActionResult Login()
        {
            return View("Login");
        }

        public async Task<IActionResult> SaveLogin(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid == true)
            {
                ApplicationUser applicationUserDB = await userManager.FindByNameAsync(loginViewModel.Username);
                if (applicationUserDB != null)
                {
                    bool found = await userManager.CheckPasswordAsync(applicationUserDB, loginViewModel.Password);
                    if (found == true)
                    {
                        //List<Claim> claims = new List<Claim>();
                        //claims.Add(new Claim("Institute", "ITI"));
                        ////await  signInManager.SignInWithClaimsAsync(applicationUserDB,loginViewModel.RememberMe, claims);   
                        await signInManager.SignInAsync(applicationUserDB, loginViewModel.RememberMe);
                        return RedirectToAction("Index", "profile");
                    }
                }

                ModelState.AddModelError("", "Invalid Data Please Try Again");
            }
            return View("Login", loginViewModel);
        }

        public async Task<IActionResult> LogOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}
