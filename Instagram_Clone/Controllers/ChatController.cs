using Instagram_Clone.Authentication;
using Instagram_Clone.Repositories.MessageRepo;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Instagram_Clone.Controllers
{
    public class ChatController: Controller
    {
        public readonly IMessageRepository messageRepository;
        public readonly UserManager<ApplicationUser> userManager;

        public ChatController(IMessageRepository _messageRepository, UserManager<ApplicationUser> _userManager)
        {
            messageRepository = _messageRepository;
            userManager = _userManager;
        }

        public async Task<IActionResult> Index()
        {
            ApplicationUser currentUser = await userManager.GetUserAsync(User);
            

            List<Chat> chats = messageRepository.GetAllChats(currentUser.Id);


            ViewBag.UserName = currentUser?.UserName;
            ViewBag.SenderId = currentUser?.Id;


            return View("Index",chats);

        }

        public async Task<IActionResult> OpenChat(string senderId, int chatId, string receiverId)
        {
            var userRecord = await userManager.GetUserAsync(User);

            if (userRecord.Id == senderId)
            {
                var receiver = await userManager.FindByIdAsync(receiverId);
                ViewBag.senderName = receiver?.UserName;
                ViewBag.receiverId = receiverId;

            }
            else if (userRecord.Id == receiverId)
            {
                var sender = await userManager.FindByIdAsync(senderId);
                ViewBag.senderName = sender?.UserName;
                ViewBag.receiverId = senderId;

            }

            ViewBag.chatId = chatId;

            return View();
        }



    }
}
