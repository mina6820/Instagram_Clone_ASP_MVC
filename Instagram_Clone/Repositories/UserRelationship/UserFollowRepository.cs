using Instagram_Clone.Models;
namespace Instagram_Clone.Repositories.UserFollowRepo
{
    public class UserFollowRepository:Repository<UserRelationship> , IUserFollowRepository
    {
        Context context;
        public UserFollowRepository(Context context) : base(context)
        {
            // No need to store the context separately if not used elsewhere
        }


    }
}
