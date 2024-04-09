using System.ComponentModel.DataAnnotations;

namespace Instagram_Clone.ViewModels
{
    public class RegisterViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [Unique(ErrorMessage = "Name Must Be Unique")]
        public string UserName { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword{ get; set; }
        public string Email { get; set;}
        [Required]
        public string YourFavirotePerson { get; set; }

    }
}
