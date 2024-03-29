using Instagram_Clone.Models;
namespace Instagram_Clone.Repositories.UserFollowRepo
{
    public class UserRelationshipRepository:Repository<UserRelationship> , IUserRelationshipRepository
    {
        Context context;
        public UserRelationshipRepository(Context context) : base(context)
        {
            // No need to store the context separately if not used elsewhere
        }


    }
}
