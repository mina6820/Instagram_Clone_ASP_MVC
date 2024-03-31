using Instagram_Clone.Authentication;

namespace Instagram_Clone.Repositories.UserFollowRepo
{
    public interface IUserRelationshipRepository : IRepository<UserRelationship>
    {
        public List<UserRelationship> GetFollowers(string id);

        public ApplicationUser GetById(string id);

        public List<UserRelationship> GetFollowees(string id);

        public List<UserRelationship> searchFollowers(string name);

        public List<UserRelationship> searchFollowees(string name);




    }
}
