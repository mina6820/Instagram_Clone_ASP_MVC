using Azure.Core;
using Azure.Messaging;
using Instagram_Clone.Repositories.MessageRepo;
using Microsoft.AspNetCore.SignalR;
using NuGet.Protocol.Plugins;
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

        // Method to send a message to a specific user
        public async Task SendMessage(string messageInput, string receiverId, string chatId)
        {
            //create new message
            //Message message = new Message
            //{
            //    Content = messageInput,
            //    Date = DateTime.Now,
            //    chatId = int.Parse(chatId),
            //    IsDeleted = false
            //};

            // save the message in database 
            //await MessageRepository.InsertAsync(message);



            var senderId = Context.UserIdentifier;

            // Send the message to the specified user
            await Clients.Users(senderId,receiverId).SendAsync("ReceiveMessage", messageInput);



        }
    }
}


