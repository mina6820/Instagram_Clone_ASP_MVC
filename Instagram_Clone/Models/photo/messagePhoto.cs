using System.ComponentModel.DataAnnotations.Schema;

namespace Instagram_Clone.Models.photo
{
    public class messagePhoto:Photo
    {
        [ForeignKey("Message")]
        public int MessageId { get; set; }
        public Message Message { get; set; }
    }
}
