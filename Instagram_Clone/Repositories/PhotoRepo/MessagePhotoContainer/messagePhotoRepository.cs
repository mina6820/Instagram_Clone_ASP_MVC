namespace Instagram_Clone.Repositories.PhotoRepo.message
{
    public class messagePhotoRepository : PhotoRepository<messagePhoto>, ImessagePhotoRepository
    {
        Context context;
        public messagePhotoRepository(Context context) : base(context)
        {
            // No need to store the context separately if not used elsewhere
        }
    }
}
