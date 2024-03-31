using Instagram_Clone.Authentication;

namespace Instagram_Clone.Repositories.UserFollowRepo
{
    public interface IUserRelationshipRepository : IRepository<UserRelationship>
    {
        public List<ApplicationUser> GetFollowers(string id);

        public ApplicationUser GetById(string id);

        public List<ApplicationUser> GetFollowees(string id);

        public List<ApplicationUser> searchFollowers(string name);

        public List<ApplicationUser> searchFollowees(string name);




    }
}
