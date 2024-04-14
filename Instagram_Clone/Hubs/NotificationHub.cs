using Instagram_Clone.Repositories.MessageRepo;
using Instagram_Clone.Repositories.NotificationRepo.FollowRequestContainer;
using Microsoft.AspNetCore.SignalR;

namespace Instagram_Clone.Hubs
{
    public class NotificationHub:Hub
    {
        public readonly IFollowRequestRepository followRequestRepository;

        public NotificationHub(IFollowRequestRepository _followRequestRepository) 
        {
            followRequestRepository = _followRequestRepository;
        }



    }
}
