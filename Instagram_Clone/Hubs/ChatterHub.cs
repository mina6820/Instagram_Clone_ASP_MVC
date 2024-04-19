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
        public ChatterHub(IMessageRepository messageRepository)
        {
            MessageRepository = messageRepository;
        }

        // Method to send a message to a specific user
        public async Task SendMessage(string messageInput, string receiverId, string chatId)
        {

            var senderId = Context.UserIdentifier;



            //create new message
            Instagram_Clone.Models.Message message = new Instagram_Clone.Models.Message
            {
                Content = messageInput,
                Date = DateTime.Now,
                chatId = int.Parse(chatId),
                IsDeleted = false,
                SenderId = senderId,
                RecieverId = receiverId
            };

            //save the message in database
            await MessageRepository.InsertAsync(message);
            MessageRepository.Save(); 

            // Send the message to the specified user
            //await Clients.Users(senderId,receiverId).SendAsync("ReceiveMessage", messageInput);

            await Clients.Users(senderId, receiverId).SendAsync("ReceiveMessage", messageInput, senderId, message.Date);

        }
    }
}


