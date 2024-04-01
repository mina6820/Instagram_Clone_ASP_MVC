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
        
        public profilePhoto? ProfilePicture { get; set; }

    }
}
