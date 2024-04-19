

using Instagram_Clone.Repositories.NotificationRepo;
using Instagram_Clone.Repositories;
using Instagram_Clone;
using Microsoft.EntityFrameworkCore;
using System;

namespace Instagram_Clone.Repositories.NotificationRepo
{
    public class NotificationRepository<T> : Repository<T>, INotificationRepository<T> where T : class
    {
        private readonly Context context;

        public NotificationRepository(Context _context) : base(_context)
        {
            this.context = _context;
        }
        public async Task CreateNotification(string senderId, string receiverId)
        {
            var notification = new Notification
            {
                SenderId = senderId,
                ReceiverId = receiverId,
               // NotificationType = type,
                Date = DateTime.Now
            };

            context.Notifications.Add(notification);
            await context.SaveChangesAsync();
        }

        public Task DeleteNotification(int notificationId)
        {
            throw new NotImplementedException();
        }

        public Task<Notification> GetNotificationById(int notificationId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Notification>> GetNotificationsForUser(string userId, bool onlyUnread = false)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetUnreadNotificationCount(string userId)
        {
            throw new NotImplementedException();
        }

        public Task HandleNotification(Notification notification)
        {
            throw new NotImplementedException();
        }

        public Task MarkNotificationAsRead(int notificationId)
        {
            throw new NotImplementedException();
        }
    }
}

