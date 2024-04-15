using Instagram_Clone.Repositories.MessageRepo;
using Instagram_Clone.Repositories.NotificationRepo;
using Instagram_Clone.Repositories.NotificationRepo.FollowRequestContainer;
using Microsoft.AspNetCore.SignalR;

namespace Instagram_Clone.Hubs
{
    public class NotificationHub:Hub
    {
      
        public async Task SendNotification(string userId, string message)
        {
            // Send the notification to the specific user
            await Clients.User(userId).SendAsync("ReceiveNotification", message);
        }

    }
}
