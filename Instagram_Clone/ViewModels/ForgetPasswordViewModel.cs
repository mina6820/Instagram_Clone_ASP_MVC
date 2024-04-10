using System.ComponentModel.DataAnnotations;

namespace Instagram_Clone.ViewModels
{
    public class ForgetPasswordViewModel
    {
        
        public string UserName { get; set; }
        public string YourFavirotePerson { get; set; }

        [Required(ErrorMessage = "New password is required")]
        [DataType(DataType.Password)]
        public string? NewPassword { get; set; }

        [Required(ErrorMessage = "Confirm password is required")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string? ConfirmPassword { get; set; }
    }
}
