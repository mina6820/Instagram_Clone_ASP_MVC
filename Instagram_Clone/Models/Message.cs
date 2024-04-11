using Instagram_Clone.Authentication;
using Instagram_Clone.Models.photo;
using System.ComponentModel.DataAnnotations.Schema;

namespace Instagram_Clone.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public bool IsDeleted { get; set; } = false;

        public DateTime Date { get; set; } = DateTime.Now;

        [ForeignKey("Photo")]
        public int? photoId { get; set; }
        public messagePhoto? Photo { get; set; }


        [ForeignKey("chat")]
        public int chatId { get; set; }
        public Chat chat { get; set; }



        [ForeignKey("Sender")]
        public string SenderId { get; set; }
        public ApplicationUser Sender { get; set; }

        [ForeignKey("Reciever")]
        public string RecieverId { get; set; }
        public ApplicationUser Reciever { get; set; }

    }
}
