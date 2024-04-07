using Instagram_Clone.Models;
using TestingMVC.Repo;

namespace Instagram_Clone.Repositories.MessageRepo
{
    public interface IMessageRepository : IRepository<Message>
    {
        public List<Chat> GetAllChats(string userId);
        public Task InsertAsync(Message message);

    }
}
