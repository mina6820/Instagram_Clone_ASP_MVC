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

            ViewBag.CurrentUserName = currentUser.UserName;

            return View("Index",chats);

        }

        //[HttpPost]
        //public async Task<IActionResult> Create(Message message)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        ApplicationUser sender = await userManager.GetUserAsync(User);
        //        message.Sender = sender;

        //        // Insert the message using your repository
        //        await messageRepository.InsertAsync(message);

        //        // Save changes after inserting the message
        //        messageRepository.Save();

        //        return RedirectToAction("Index");
        //    }

        //    // If ModelState is not valid, return back to the view with the invalid message
        //    return View(message);
        //}
    }
}
