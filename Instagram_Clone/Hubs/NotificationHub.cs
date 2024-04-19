using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using Instagram_Clone.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using NuGet.Protocol.Plugins;
using System.Threading.Tasks;

public class NotificationHub : Hub
{
    public async Task SendFollowNotification(string receiverUserId, string senderUserName)
    {
        await Clients.User(receiverUserId).SendAsync("ReceiveFollowNotification", senderUserName);
    }
}



//using Instagram_Clone.Models;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.SignalR;
//using NuGet.Protocol.Plugins;
//using System.Threading.Tasks;

//namespace Instagram_Clone.Hubs
//{
//    [Authorize]
//    public class NotificationHub : Hub
//    {
//        public async Task SendFollowNotification(string receiverUserId, string senderUserName)
//        {
//            try
//            {
//                // Log a message to verify the method is being called
//                Console.WriteLine("Sending follow notification...");

//                // Verify that Clients is not null before invoking SendAsync
//                if (Clients != null)
//                {

//                    await Clients.User(receiverUserId).SendAsync("ReceiveFollowNotification", senderUserName);
//                }
//                else
//                {
//                    Console.WriteLine("Clients object is null.");
//                }
//            }
//            catch (Exception ex)
//            {
//                // Log any exceptions that occur
//                Console.WriteLine("Exception in SendFollowNotification: " + ex.Message);
//            }
//        }
//    }

//}
