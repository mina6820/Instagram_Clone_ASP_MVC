using Instagram_Clone.Authentication;
using System.ComponentModel.DataAnnotations.Schema;

namespace Instagram_Clone.Models
{
    public class StoryViewer
    {
        public int Id { get; set; }

        [ForeignKey("Story")]
        public int StoryId { get; set; }
        public Story Story { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
