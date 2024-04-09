using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Instagram_Clone.ViewModels
{
    public class EditUserViewModel
    {
        public string ? Id { get; set; }
        public string ?UserName { get; set; }
        public string ?Email { get; set; }
        public string ?PhoneNumber { get; set; }
        public string ?FirstName { get; set; }

        public string ?LastName { get; set; }

        public string? Bio { get; set; }
        
        public IFormFile? ProfilePicture { get; set; }
        public string? ImgName { get; set; }

        [Required(ErrorMessage = "Current password is required")]
        [DataType(DataType.Password)]
        public string? CurrentPassword { get; set; }

        [Required(ErrorMessage = "New password is required")]
        [DataType(DataType.Password)]
        //[RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$", ErrorMessage = "Password must have at least 8 characters, one uppercase letter, one lowercase letter, one digit, and one special character.")]
        public string? NewPassword { get; set; }

        [Required(ErrorMessage = "Confirm password is required")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string? ConfirmPassword { get; set; }

    }
}
