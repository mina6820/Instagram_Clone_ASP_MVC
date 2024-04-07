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
       





        public List<UserRelationship> ?Followers { get; set; }

        
        public List<UserRelationship>? Following { get; set; }
    }
}
