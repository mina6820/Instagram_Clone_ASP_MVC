using System.ComponentModel.DataAnnotations.Schema;

namespace Instagram_Clone.Models
{
    public class Notification
    {
        public int Id { get; set; }

        [ForeignKey("Sender")]
        public string SenderId { get; set; }
        public ApplicationUser Sender { get; set; }

        [ForeignKey("Receiver")]
        public string ReceiverId { get; set; }
        public ApplicationUser Receiver { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;

        public virtual NotificationType NotificationType { get; } 
    }

    public enum NotificationType
    {
        FollowRequest,
        Like,
        Comment
    }
}
