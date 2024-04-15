using Instagram_Clone.Authentication;
using System.ComponentModel.DataAnnotations.Schema;

namespace Instagram_Clone.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; } = false;


        public string Content { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;

        [ForeignKey("User")]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        

        [ForeignKey("Post")]
        public int PostId { get; set; }
        public Post Post { get; set; }
    }
}
