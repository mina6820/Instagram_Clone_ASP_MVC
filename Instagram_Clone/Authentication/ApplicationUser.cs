using Instagram_Clone.Models.photo;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Instagram_Clone.Authentication
{
    public class ApplicationUser:IdentityUser
    {
         
        public string FirstName { get; set; }
        public string LastName { get; set; }
       
        public string? Bio { get; set; }
        public bool IsDeleted { get; set; } = false;


        public List<Story>? Stories { get; set; }
        public List<Post>? Posts { get; set; }


        [ForeignKey("ProfilePicture")]
        public int? PhotoId { get; set; }
        public profilePhoto? ProfilePicture { get; set; }


        [InverseProperty("Follower")]
        public List<UserRelationship>? Followers { get; set; }

        [InverseProperty("Followee")]
        public List<UserRelationship>? Following { get; set; }


        public string? YourFavirotePerson { get; set; }

        //public List<Notification>? notifications { get; set; } 

    }
}
