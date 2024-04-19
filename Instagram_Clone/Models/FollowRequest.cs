namespace Instagram_Clone.Models
{
    public class FollowRequest : Notification
    {
        public bool IsAccepted { get; set; } = false;
        public override NotificationType NotificationType => NotificationType.FollowRequest;
    }
}
