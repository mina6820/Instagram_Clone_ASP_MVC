using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Instagram_Clone.Models
{
    public interface INotification
    {
        int Id { get; set; }

        [ForeignKey("Sender")]
        string SenderId { get; set; }
        ApplicationUser Sender { get; set; }

        [ForeignKey("Receiver")]
        string ReceiverId { get; set; }
        ApplicationUser Receiver { get; set; }

        DateTime Date { get; set; }
        NotificationType NotificationType { get; }
    }

    public enum NotificationType
    {
        FollowRequest,
        Like,
        Comment
    }
}
