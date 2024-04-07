using System.ComponentModel.DataAnnotations.Schema;

namespace Instagram_Clone.Models.photo
{
    public class storyPhoto:Photo
    {


        [ForeignKey("story")]
        public int storyId { get; set; }
        public Story story { get; set; }
    }
}
