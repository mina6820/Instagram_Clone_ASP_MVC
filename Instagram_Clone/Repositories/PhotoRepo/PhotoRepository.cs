using Instagram_Clone.Authentication;
using Instagram_Clone.Models.photo;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;

namespace Instagram_Clone.Repositories.PhotoRepo
{
    public class PhotoRepository : Repository<Photo>, IPhotoRepository
    {
        Context context;
        public PhotoRepository(Context context) : base(context)
        {
            // No need to store the context separately if not used elsewhere
        }
    }
}
