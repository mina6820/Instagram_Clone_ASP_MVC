using System.ComponentModel.DataAnnotations.Schema;

namespace Instagram_Clone.Models
{
    public class Chat
    {
        public int id { get; set; }
        public List<Message>? messages { get; set; }



        [ForeignKey("Sender")]
        public string SenderId { get; set; }
        public ApplicationUser Sender { get; set; }

        [ForeignKey("Reciever")]
        public string RecieverId { get; set; }
        public ApplicationUser Reciever { get; set; }
    }
}
