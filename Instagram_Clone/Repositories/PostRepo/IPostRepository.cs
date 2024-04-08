using Instagram_Clone.Models;
using TestingMVC.Repo;

namespace Instagram_Clone.Repositories.PostRepo
{
    public interface IPostRepository : IRepository<Post> 
    {
     
        public List<Post> GetAllPostsByUserID(string userID);

        public List<Post> GetAllPostsWithPhotosAndLikes();


        public Post? GetPostByIDWithLikes(int id);

        public Post? GetPostwithUserAndCommentsAndFollowersById(int id);
    }
}
