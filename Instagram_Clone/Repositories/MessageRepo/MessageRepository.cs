using Instagram_Clone.Models;
using Microsoft.EntityFrameworkCore;

namespace Instagram_Clone.Repositories.MessageRepo
{
    public class MessageRepository :Repository<Message> , IMessageRepository
    {

        Context context;
        public MessageRepository(Context _context) : base(_context)
        {
            context = _context;
        }

        public async Task InsertAsync(Message message)
        {
            context.Add(message);
        }

        public List<Chat> GetAllChats(string userId)
        {
            return context.Chats
                .Include(ch => ch.Reciever)
                .ThenInclude(r => r.ProfilePicture)
                .Include(ch => ch.Sender)
                .ThenInclude(r => r.ProfilePicture)
                .Where(ch => (ch.RecieverId == userId || ch.SenderId == userId))
                .ToList();
        }

    }
}
