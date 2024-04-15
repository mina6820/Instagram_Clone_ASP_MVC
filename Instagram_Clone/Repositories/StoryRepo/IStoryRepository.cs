using TestingMVC.Repo;

namespace Instagram_Clone.Repositories.StoryRepo
{
    public interface IStoryRepository: IRepository<Story>
    {
        public List<Story> GetAllStories(string id);

        public List<Story> GetMyStories(string id);

    }


}
