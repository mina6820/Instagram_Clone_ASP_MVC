using Instagram_Clone.Models.photo;
using Instagram_Clone.ViewModels;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Instagram_Clone.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IWebHostEnvironment webHost;
        private readonly Context context;
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IWebHostEnvironment webHost,Context context)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.webHost = webHost;
            this.context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        //Register
        [HttpGet]
        public IActionResult Register()
        {
            return View("Register");
        }
        //[HttpPost]
        //public async Task<IActionResult> Register(RegisterViewModel userVM)
        //{

        //        profilePhoto profilePhoto = new profilePhoto();
        //        profilePhoto.Name = "messi.jpg";
        //        profilePhoto.Path = Path.Combine(webHost.WebRootPath, "Images");


        //    if (ModelState.IsValid) {
        //        ApplicationUser user = new ApplicationUser()
        //        {

        //            UserName = userVM.UserName,
        //            Email = userVM.Email,
        //            FirstName = userVM.FirstName,
        //            LastName = userVM.LastName,
        //            PasswordHash = userVM.Password,
        //            ProfilePicture = profilePhoto,

        //        };

        //        //Save
        //       IdentityResult result= await userManager.CreateAsync(user ,userVM.Password);
        //        if (result.Succeeded)
        //        {
        //            profilePhoto.UserId = user.Id;
        //            await signInManager.SignInAsync(user,false);

        //            profilePhoto.User = user;

        //            return RedirectToAction("Login");
        //        }
        //        foreach (var item in result.Errors)
        //        {
        //            ModelState.AddModelError(string.Empty, item.Description);
        //        }
        //        //save db add

        //    }
        //    return View("Register", userVM);
        //}

        public async Task<IActionResult> Register(RegisterViewModel userVM)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser()
                {
                    UserName = userVM.UserName,
                    Email = userVM.Email,
                    FirstName = userVM.FirstName,
                    LastName = userVM.LastName,
                    PasswordHash = userVM.Password,
                };



                // Save user
                IdentityResult result = await userManager.CreateAsync(user, userVM.Password);
                if (result.Succeeded)
                {
                    await userManager.AddClaimAsync(user, new Claim(ClaimTypes.Email, userVM.Email));

                    // Create profile photo
                    profilePhoto profilePhoto = new profilePhoto
                    {
                        Name = "profileuser.jpeg",
                        Path = Path.Combine(webHost.WebRootPath, "Images"),
                        UserId = user.Id // Set UserId to the newly created user's ID
                    };

                    // Save profile photo
                    context.ProfilePhoto.Add(profilePhoto);
                    await context.SaveChangesAsync();

                    // Assign profile photo to user
                    user.ProfilePicture = profilePhoto;

                    // Update user
                    await userManager.UpdateAsync(user);

                    // Sign in user
                    await signInManager.SignInAsync(user, false);

                    return RedirectToAction("Login");
                }

                // Handle errors
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If ModelState is not valid or if user creation failed, return to the Register view
            return View("Register", userVM);
        }




        //Login
        [HttpGet]
        public IActionResult Login()
        {
            return View("Login");
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
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

        


        ////Reset password
        //// POST: /Account/ForgotPassword
        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var user = await UserManager.FindByNameAsync(model.Username);
        //        if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
        //        {
        //            // Don't reveal that the user does not exist or is not confirmed
        //            return View("ForgotPasswordConfirmation");
        //        }

        //        string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
        //        var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
        //        await UserManager.SendEmailAsync(user.Id, "Reset Password", $"Please reset your password by clicking <a href =\"{callbackUrl}\">here</a>");

        //        return RedirectToAction("ForgotPasswordConfirmation", "Account");
        //    }

        //    // If we got this far, something failed, redisplay form
        //    return View(model);
        //}

        //// POST: /Account/ResetPassword
        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(model);
        //    }
        //    var user = await UserManager.FindByNameAsync(model.Username);
        //    if (user == null)
        //    {
        //        // Don't reveal that the user does not exist
        //        return RedirectToAction("ResetPasswordConfirmation", "Account");
        //    }
        //    var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
        //    if (result.Succeeded)
        //    {
        //        return RedirectToAction("ResetPasswordConfirmation", "Account");
        //    }
        //    AddErrors(result);
        //    return View();
        //}

    }




}
