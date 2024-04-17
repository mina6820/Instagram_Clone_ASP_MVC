namespace Instagram_Clone.Repositories.NotificationRepo
{
    public interface INotificationRepository<T> :IRepository<T> where T : class
    {
        Task CreateNotification(string senderId, string receiverId);
        Task<List<Notification>> GetNotificationsForUser(string userId, bool onlyUnread = false);
        Task MarkNotificationAsRead(int notificationId);
        Task DeleteNotification(int notificationId);
        Task<int> GetUnreadNotificationCount(string userId);
        Task<Notification> GetNotificationById(int notificationId);
        Task HandleNotification(Notification notification);
    }
}
