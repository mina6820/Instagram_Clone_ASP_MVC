using Instagram_Clone.Repositories;
using Instagram_Clone.Models;
using Instagram_Clone;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace Instagram_Clone.Repositories.NotificationRepo
{
    public class NotificationRepository : Repository<FollowRequest_notification>, INotificationRepository
    {
        private readonly Context context;

        public NotificationRepository(Context _context) : base(_context)
        {
            this.context = _context;
        }

             
        public List<FollowRequest_notification> GetNotifications(string UserId)
        {
            return context.FollowRequest_notifications
                .Include(n => n.Receiver)
                .Where(n => n.ReceiverId == UserId && n.IsAccepted==false)
                .ToList();
        }



    }
}

