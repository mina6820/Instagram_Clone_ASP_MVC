namespace Instagram_Clone.Repositories.NotificationRepo
{
    public interface INotificationRepository : IRepository<FollowRequest_notification>
    {
        
        public List<FollowRequest_notification> GetNotifications(string UesrId);
        public FollowRequest_notification Hide_Request(int NotificationID);

    }
}
