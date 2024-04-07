using Azure.Core;
using Azure.Messaging;
using Instagram_Clone.Repositories.MessageRepo;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Instagram_Clone.Hubs
{
    public class ChatterHub : Hub
    {

        public readonly IMessageRepository MessageRepository;
        private static Dictionary<string, string> userConnectionMap = new Dictionary<string, string>();
        public ChatterHub(IMessageRepository messageRepository)
        {
            MessageRepository = messageRepository;
        }

        public override Task OnConnectedAsync()
        {
            var userId = Context.UserIdentifier;
            var connectionId = Context.ConnectionId;

            userConnectionMap[userId] = connectionId;

            return base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var userId = Context.UserIdentifier;
            userConnectionMap.Remove(userId);

            await base.OnDisconnectedAsync(exception);
        }

        // Method to send a message to a specific user
        public async Task SendMessage(string messageInput, string receiverId, string chatId)
        {
            // create new message
            //Message message = new Message
            //{
            //    Content = messageInput,
            //    Date = DateTime.Now,
            //    chatId = int.Parse(chatId),
            //    IsDeleted = false
            //};

            //// save the message in database 
            //await MessageRepository.InsertAsync(message);


            // Send the message to the specified user
             await Clients.All.SendAsync("ReciveMessage", messageInput);

            var senderId = Context.UserIdentifier;

            await Clients.User(receiverId).SendAsync("ReceiveMessage", messageInput);


            //var connectionId = userConnectionMap[receiverId];

            //// Send the message to the specified user using their connection ID
            // await Clients.Client(connectionId).SendAsync("ReceiveMessage", messageInput);


        }

    }
}

public class CustomEmailProvider : IUserIdProvider
{
    public virtual string GetUserId(HubConnectionContext connection)
    {
        return connection.User?.FindFirst(ClaimTypes.Email)?.Value;
    }
}
