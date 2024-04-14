

using Instagram_Clone.Repositories.NotificationRepo;
using Instagram_Clone.Repositories;
using Instagram_Clone;

namespace Instagram_Clone.Repositories.NotificationRepo
{
    public class NotificationRepository<T> : Repository<T>, INotificationRepository<T> where T : class
    {
        public NotificationRepository(Context context) : base(context)
        {
            // No need to store the context separately if not used elsewhere
        }
    }
}

