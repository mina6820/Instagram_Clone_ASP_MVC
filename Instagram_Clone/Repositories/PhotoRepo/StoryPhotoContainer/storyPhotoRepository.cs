namespace Instagram_Clone.Repositories.PhotoRepo.Story
{
    public class storyPhotoRepository :PhotoRepository<storyPhoto> , IstoryPhotoRepository
    {
        Context context;
        public storyPhotoRepository(Context context) : base(context)
        {
            // No need to store the context separately if not used elsewhere
        }
    }
}
