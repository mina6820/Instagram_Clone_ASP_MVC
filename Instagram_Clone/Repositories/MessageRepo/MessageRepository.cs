using Instagram_Clone.Models;

namespace Instagram_Clone.Repositories.MessageRepo
{
    public class MessageRepository :Repository<Message> , IMessageRepository
    {

        Context context;
        public MessageRepository(Context context) : base(context)
        {
            // No need to store the context separately if not used elsewhere
        }
    }
}
