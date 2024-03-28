using Instagram_Clone.Authentication;
using System.ComponentModel.DataAnnotations.Schema;

namespace Instagram_Clone.Models.photo
{
    public class profilePhoto:Photo
    {
        [ForeignKey("User")]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
