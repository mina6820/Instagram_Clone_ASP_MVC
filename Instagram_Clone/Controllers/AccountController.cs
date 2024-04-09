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
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IWebHostEnvironment webHost, Context context)
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
                    YourFavirotePerson=userVM.YourFavirotePerson
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
        public IActionResult ChangePasword()
        {
            ResetPasswordViewMode restPass = new ResetPasswordViewMode();
            return View(restPass);
        }




        public async Task<IActionResult> SaveChangePassword(ResetPasswordViewMode editUserViewModel)
        {
            Claim claim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            ApplicationUser user = context.Users.FirstOrDefault(u => u.Id == claim.Value);
            if (ModelState.IsValid)
            {
                if (editUserViewModel.CurrentPassword != null && editUserViewModel.ConfirmPassword != null && editUserViewModel.NewPassword != null)
                {
                    if (await userManager.CheckPasswordAsync(user, editUserViewModel.CurrentPassword))
                    {
                        await userManager.ChangePasswordAsync(user, editUserViewModel.CurrentPassword, editUserViewModel.NewPassword);
                        context.Users.Update(user);
                        context.SaveChanges();
                        return RedirectToAction("Index", "Profile");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Not matched with current password");
                        // Return the view with the provided view model
                        return View("ChangePasword", editUserViewModel);
                    }
                }
                else
                {
                    // Return the view with the provided view model
                    return View("ChangePasword", editUserViewModel);
                }
            }
            else
            {
                // Return the view with the provided view model
                return View("ChangePasword", editUserViewModel);
            }
        }

    }

    }
