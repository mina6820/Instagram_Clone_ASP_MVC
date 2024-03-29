using Instagram_Clone.Models;
using Microsoft.Identity.Client;
using System.ComponentModel;
namespace Instagram_Clone.Repositories.UserFollowRepo
{
    public class UserRelationshipRepository:Repository<UserRelationship> , IUserRelationshipRepository
    {
        Context context;
        public UserRelationshipRepository(Context context) : base(context)
        {
            // No need to store the context separately if not used elsewhere
        }

      
        public List<UserRelationship> GetAll(string include="")
        {
             List < UserRelationship > Lists = context.UserRelationship.ToList();
            return Lists;

        }


    }
}
