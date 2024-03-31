using System.ComponentModel.DataAnnotations.Schema;

namespace Instagram_Clone.Models.photo
{
    public class postPhoto:Photo
    {
        [ForeignKey("Post")]

        // update the postID make it nullable :  taken by k.wessa
        public int? PostId { get; set; }
        public Post Post { get; set; }

       
    }
}
