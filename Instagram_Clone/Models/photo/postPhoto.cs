using System.ComponentModel.DataAnnotations.Schema;

namespace Instagram_Clone.Models.photo
{
    public class postPhoto:Photo
    {
        [ForeignKey("Post")]
        public int PostId { get; set; }
        public Post Post { get; set; }
        public string ayhaga { get; set; }
    }
}
