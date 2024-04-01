using Instagram_Clone.Models;
using TestingMVC.Repo;

namespace Instagram_Clone.Repositories.PostRepo
{
    public interface IPostRepository : IRepository<Post> 
    {
     
        public List<Post> GetAllPostsByUserID(string userID);

    }
}
