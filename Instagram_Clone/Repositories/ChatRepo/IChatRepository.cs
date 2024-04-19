namespace Instagram_Clone.Repositories.ChatRepo
{
    public interface IChatRepository: IRepository<Chat>
    {
        public Chat GetChatById(int id);
    }
}
