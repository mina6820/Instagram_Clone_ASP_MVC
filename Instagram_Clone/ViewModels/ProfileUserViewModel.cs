using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Instagram_Clone.ViewModels
{
    public class ProfileUserViewModel
    {
        public string UserName { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string? Bio { get; set; }
        public bool IsDeleted { get; set; } = false;


        public List<Story>? Stories { get; set; }
        public List<StoryViewer>? StoryViews { get; set; }
        public List<Post>? Posts { get; set; }

        public profilePhoto? ProfilePicture { get; set; }

        public IFormFile? ProfilePicture2 { get; set; }


        public string? Id { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }

        public string? ImgName { get; set; }


        public List<UserRelationship> ?Followers { get; set; }

        
        public List<UserRelationship>? Following { get; set; }
        //
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
