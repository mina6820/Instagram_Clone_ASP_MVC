using Instagram_Clone.Models;
using TestingMVC.Repo;

namespace Instagram_Clone.Repositories.LikeRepo
{
    public interface ILikeRepository:IRepository<Like>
    {
        public Like? GetByUserIdAndPostId(string userId , int postId);
    }
}
