using Azure.Messaging;
using Instagram_Clone.Repositories.MessageRepo;
using Microsoft.AspNetCore.SignalR;
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
            //await Clients.All.SendAsync("ReciveMessage", messageInput);

            await Clients.User(receiverId).SendAsync("ReceiveMessage", messageInput);

        }

        public override Task OnConnectedAsync()
        {

            return base.OnConnectedAsync();
        }
    }
}
