using Instagram_Clone.Models;

namespace Instagram_Clone.Repositories.CommentRepo
{
    public class CommentRepository: Repository<Comment>, ICommentRepository
    {
        Context context;
        public CommentRepository(Context context) : base(context)
        {
            // No need to store the context separately if not used elsewhere
        }
    }
}
