using Instagram_Clone.Authentication;
using System.ComponentModel.DataAnnotations.Schema;

namespace Instagram_Clone.Models
{
    public class UserRelationship
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; } = false;



        [ForeignKey("Follower")]
        public string FollowerId { get; set; }
        public ApplicationUser Follower { get; set; }


        [ForeignKey("Followee")]
        public string FolloweeId { get; set; }
        public ApplicationUser Followee { get; set; }



    }
}
