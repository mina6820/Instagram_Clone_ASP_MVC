using Instagram_Clone.Repositories.PhotoRepo.message;

namespace Instagram_Clone.Repositories.PhotoRepo.ProfilePhotoContainer
{
    public class profilePhotoRepository: PhotoRepository<profilePhoto>, IprofilePhotoRepository
    {
        Context context;
        public profilePhotoRepository(Context context) : base(context)
        {
            // No need to store the context separately if not used elsewhere
        }
    
    }
}
