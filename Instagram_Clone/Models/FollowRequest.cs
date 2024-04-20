using System.ComponentModel.DataAnnotations.Schema;

namespace Instagram_Clone.Models
{
    public class FollowRequest_notification : INotification
    {
        public int Id { get; set; }

        [ForeignKey("Sender")]
        public string SenderId { get; set; }
        public ApplicationUser Sender { get; set; }

        [ForeignKey("Receiver")]
        public string ReceiverId { get; set; }
        public ApplicationUser Receiver { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;

        public  NotificationType NotificationType => NotificationType.FollowRequest;

        public bool IsAccepted { get; set; } = false;
        public bool IsRequested { get; set; } = false;  
    }
}
