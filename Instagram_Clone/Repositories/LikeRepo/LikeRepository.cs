using Instagram_Clone.Models;

namespace Instagram_Clone.Repositories.LikeRepo
{
    public class LikeRepository:Repository<Like> , ILikeRepository
    {
        Context context;
        public LikeRepository(Context context) : base(context)
        {
            // No need to store the context separately if not used elsewhere
            this.context = context;
        }

        public Like? GetByUserIdAndPostId(string userId, int postId)
        {
            return context.Likes.FirstOrDefault(k =>  k.UserId == userId  && k.PostId == postId);
        }
    }
}
