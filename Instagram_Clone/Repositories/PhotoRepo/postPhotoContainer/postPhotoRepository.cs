namespace Instagram_Clone.Repositories.PhotoRepo.postPhotoContainer
{
    public class postPhotoRepository:PhotoRepository<postPhoto>, IpostPhotoRepository
    {
        Context context;
        public postPhotoRepository(Context context) : base(context)
        {
            // No need to store the context separately if not used elsewhere
        }
    
    
    }
}
