namespace Instagram_Clone.Repositories.StoryRepo
{
    public class StoryRepository :Repository<Story> , IStoryRepository
    {
        Context context;
        public StoryRepository(Context context) : base(context)
        {
            // No need to store the context separately if not used elsewhere
        }
    }
}
