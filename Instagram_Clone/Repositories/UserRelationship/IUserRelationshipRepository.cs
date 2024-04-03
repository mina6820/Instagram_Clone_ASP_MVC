using Instagram_Clone.Authentication;

namespace Instagram_Clone.Repositories.UserFollowRepo
{
    public interface IUserRelationshipRepository : IRepository<UserRelationship>
    {
        public List<UserRelationship> GetFollowers(string id);

        public ApplicationUser GetById(string id);

        public List<UserRelationship> GetFollowees(string id);

        public List<ApplicationUser> searchFollowers(string Name);

        public List<ApplicationUser> searchFollowees(string Name);

        public List<UserRelationship> searchFollowers2(string Name, string id);

        public List<UserRelationship> searchFollowees2(string Name, string id);




    }
}
