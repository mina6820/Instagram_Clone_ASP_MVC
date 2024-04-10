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
           // ResetPasswordViewMode restPass = new ResetPasswordViewMode();
            return View("ChangePasword");
        }




        //public async Task<IActionResult> SaveChangePassword(ResetPasswordViewMode editUserViewModel)
        //{
        //    Claim claim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
        //    ApplicationUser user = context.Users.FirstOrDefault(u => u.Id == claim.Value);
        //    if (ModelState.IsValid)
        //    {
        //        if (editUserViewModel.CurrentPassword != null && editUserViewModel.ConfirmPassword != null && editUserViewModel.NewPassword != null)
        //        {
        //            if (await userManager.CheckPasswordAsync(user, editUserViewModel.CurrentPassword))
        //            {
        //                await userManager.ChangePasswordAsync(user, editUserViewModel.CurrentPassword, editUserViewModel.NewPassword);
        //                context.Users.Update(user);
        //                context.SaveChanges();
        //                return RedirectToAction("Index", "Profile");

        //            }
        //            else
        //            {
        //                ModelState.AddModelError("", "Not matched with current password");
        //                // Return the view with the provided view model
        //                return View("ChangePasword", editUserViewModel);
        //            }
        //        }
        //        else
        //        {
        //            // Return the view with the provided view model
        //            return View("ChangePasword", editUserViewModel);
        //        }
        //    }
        //    else
        //    {
        //        // Return the view with the provided view model
        //        return View("ChangePasword", editUserViewModel);
        //    }
        //}

        public async Task<IActionResult> SaveChangePassword(ResetPasswordViewMode editUserViewModel)
        {
            // Retrieve the user ID from the claims
            Claim claim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            string userId = claim?.Value;

            // Find the user by ID
            ApplicationUser user = await userManager.FindByIdAsync(userId);

            if (ModelState.IsValid)
            {
                // Check if the provided passwords are not null
                if (!string.IsNullOrEmpty(editUserViewModel.CurrentPassword) &&
                    !string.IsNullOrEmpty(editUserViewModel.NewPassword) &&
                    !string.IsNullOrEmpty(editUserViewModel.ConfirmPassword))
                {
                    // Check if the current password matches the user's actual password
                    if (await userManager.CheckPasswordAsync(user, editUserViewModel.CurrentPassword))
                    {
                        // Change the user's password
                        var changePasswordResult = await userManager.ChangePasswordAsync(user, editUserViewModel.CurrentPassword, editUserViewModel.NewPassword);

                        // Check if password change was successful
                        if (changePasswordResult.Succeeded)
                        {
                            // Save changes to the user
                            await userManager.UpdateAsync(user);

                            // Redirect to profile index
                            return RedirectToAction("Index", "Profile");
                        }
                        else
                        {
                            // If password change fails, add errors to ModelState
                            foreach (var error in changePasswordResult.Errors)
                            {
                                ModelState.AddModelError("", error.Description);
                            }

                            // Return the view with the provided view model
                            return View("ChangePasword", editUserViewModel);
                        }
                    }
                    else
                    {
                        // If current password doesn't match, add error to ModelState
                        ModelState.AddModelError("", "Current password is incorrect.");

                        // Return the view with the provided view model
                        return View("ChangePasword", editUserViewModel);
                    }
                }
            }

            // If ModelState is not valid or passwords are null, return the view with the provided view model
            return View("ChangePasword", editUserViewModel);
        }



        public IActionResult ForgetPassword()
        {
            
            return View("ForgetPassword");
        }

        //public async Task< IActionResult> SaveForgetPassword(ForgetPasswordViewModel forgetPasswordViewModel)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        ApplicationUser user = await userManager.FindByNameAsync(forgetPasswordViewModel.UserName);
        //        if (user != null && user.YourFavirotePerson==forgetPasswordViewModel.YourFavirotePerson && user.UserName==forgetPasswordViewModel.UserName)
        //        {
        //            var resetToken = await userManager.GeneratePasswordResetTokenAsync(user);
        //            await userManager.ResetPasswordAsync(user, resetToken, forgetPasswordViewModel.NewPassword);

        //            context.Users.Update(user);
        //            context.SaveChanges();
        //            return RedirectToAction("Login");
        //        }
        //    }

        //    return View("ForgetPassword");
        //}

        public async Task<IActionResult> SaveForgetPassword(ForgetPasswordViewModel forgetPasswordViewModel)
        {
            if (ModelState.IsValid)
            {
                // Find the user by username
                ApplicationUser user = await userManager.FindByNameAsync(forgetPasswordViewModel.UserName);

                // Check if user exists and verification information matches
                if (user != null &&
                    user.YourFavirotePerson == forgetPasswordViewModel.YourFavirotePerson &&
                    user.UserName == forgetPasswordViewModel.UserName)
                {
                    // Generate password reset token
                    var resetToken = await userManager.GeneratePasswordResetTokenAsync(user);

                    // Reset user's password
                    var resetResult = await userManager.ResetPasswordAsync(user, resetToken, forgetPasswordViewModel.NewPassword);

                    // Check if password reset was successful
                    if (resetResult.Succeeded)
                    {
                        // Save changes to the user
                        await userManager.UpdateAsync(user);

                        // Redirect to login page
                        return RedirectToAction("Login");
                    }
                    else
                    {
                        // If password reset fails, add errors to ModelState
                        foreach (var error in resetResult.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
                else
                {
                    // If user not found or verification information incorrect, add error to ModelState
                    ModelState.AddModelError(string.Empty, "Invalid username or Your Favirote Person Name");
                }
            }

            // If ModelState is not valid or if user not found, return the ForgetPassword view with errors
            return View("ForgetPassword", forgetPasswordViewModel);
        }

    }

}
