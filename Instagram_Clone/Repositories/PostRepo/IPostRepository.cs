using Instagram_Clone.Models;
using TestingMVC.Repo;

namespace Instagram_Clone.Repositories.PostRepo
{
    public interface IPostRepository : IRepository<Post> 
    {
     
        public List<Post> GetAllPostsByUserID(string userID);

        public List<Post> GetAllPostsWithPhotosAndLikes(string id);


        public Post? GetPostByIDWithLikes(int id);

        public Post? GetPostwithUserAndCommentsAndFollowersById(int id);

        public List<Post>? GetAllPosts();

        public List<Post>? GetMyPosts(string id);

        public Post? GetPostByIDWithUser(int id);
    }
}
