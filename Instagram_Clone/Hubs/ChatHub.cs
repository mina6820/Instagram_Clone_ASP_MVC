using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Instagram_Clone.Hubs
{
    public class ChatHub : Hub
    {
        // Method to send a message to a specific user
        public async Task SendMessage(Message message, string receiverId)
        {
            // Send the message to the specified user
            await Clients.User(receiverId).SendAsync("ReceiveMessage", message);
        }
    }
}
