using System.ComponentModel.DataAnnotations.Schema;

namespace Instagram_Clone.Models.photo
{
    public class postPhoto:Photo
    {
        [ForeignKey("Post")]
        public int PostId { get; set; }
        public Post Post { get; set; }
        public string ayhaga { get; set; }
        public string ayhaga2 { get; set; }
        public string ayhaga22 { get; set; }
        //////comment 33333
        // comment 4444444
        //comment 5555
        //comment 666666

    }
}
