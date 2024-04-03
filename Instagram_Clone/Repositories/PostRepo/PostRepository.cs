using Instagram_Clone.Models;
using Microsoft.EntityFrameworkCore;

namespace Instagram_Clone.Repositories.PostRepo
{
    public class PostRepository : Repository<Post> , IPostRepository
    {
        Context context;
        public PostRepository(Context context) : base(context)
        {
            // No need to store the context separately if not used elsewhere
            this.context = context;
        }

        public List<Post>? GetAllPostsByUserID(string userID)
        {
            return context.Posts.Where(p => p.UserId == userID).ToList();
        }

        public List<Post>? GetAllPostsWithPhotosAndLikes()
        {
            return context.Posts.Where(p => p.IsDeleted == false).Include(p => p.Likes).Include(p => p.User).ToList();
        }
    }
}
