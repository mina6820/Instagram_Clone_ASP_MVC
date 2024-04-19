//using Instagram_Clone.Repositories;
//using Instagram_Clone.Models;
//using Instagram_Clone;
//using Microsoft.EntityFrameworkCore;
//using System.Collections.Generic;
//using System.Threading.Tasks;
//using System.Linq;

//namespace Instagram_Clone.Repositories.NotificationRepo
//{
//    public class NotificationRepository : Repository<INotification>, INotificationRepository
//    {
//        private readonly Context context;

//        public NotificationRepository(Context _context) : base(_context)
//        {
//            this.context = _context;
//        }

//        public async Task AddNotificationAsync(INotification notification)
//        {
//            context.Set<INotification>().Add(notification);
//            await SaveAsync();
//        }

//        public Task AddNotificationAsync(Notification notification)
//        {
//            throw new NotImplementedException();
//        }

//        public List<INotification> GetNotifications(string UserId)
//        {
//            return context.Set<INotification>()
//                .Include(n => n.Receiver)
//                .Include(n => n.Sender)
//                .Where(n => n.ReceiverId == UserId)
//                .ToList();
//        }

//        public void Insert(Notification obj)
//        {
//            throw new NotImplementedException();
//        }

//        public void Update(Notification obj)
//        {
//            throw new NotImplementedException();
//        }

//        List<Notification> IRepository<Notification>.GetAll()
//        {
//            throw new NotImplementedException();
//        }

//        Notification IRepository<Notification>.GetById(string id)
//        {
//            throw new NotImplementedException();
//        }

//        List<Notification> INotificationRepository.GetNotifications(string UesrId)
//        {
//            throw new NotImplementedException();
//        }

//        // Implement the rest of the interface methods as needed
//    }
//}

