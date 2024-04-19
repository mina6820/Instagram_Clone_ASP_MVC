using Instagram_Clone.Repositories.CommentRepo;
using Microsoft.EntityFrameworkCore;
using TestingMVC.Repo;

namespace Instagram_Clone.Repositories.ChatRepo
{
    public class ChatRepository: Repository<Chat>, IChatRepository
    {
        Context context;
        public ChatRepository(Context _context) : base(_context)
        {
            context = _context;
        }


        public Chat GetChatById(int id)
        {
            return
                context.Chats
                .Include(ch => ch.messages)
                .FirstOrDefault(ch => ch.id == id);

        }
    }
}
