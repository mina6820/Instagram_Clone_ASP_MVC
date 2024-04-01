using Instagram_Clone.Models.photo;
using TestingMVC.Repo;

namespace Instagram_Clone.Repositories.PhotoRepo
{
    public interface IPhotoRepository<T> : IRepository<T> where T : class //IPhotoRepository : IRepository<Photo>
    {


    }
}