using Instagram_Clone.Authentication;
using Instagram_Clone.Models;
using Instagram_Clone.ViewModels;

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

        public void GetFollowingRelationship(string followerId, string LoginUsrer);
        public void GetFollowersRelationship(string followerId, string LoginUsrer);
        public List<ApplicationUser> GetFollowersAndFollowings(string id);

        //public  Task Follow(string followeeId, string followerId);

        //public void Follow(string followeeid, string loginuser);


        public List<ApplicationUser> GetNonFollowees(string id);
        public List<ApplicationUser> GetRandomlyTopFive(string id);
        public List<ApplicationUser> GetAppUserFollowees(string id);
        // public List<ApplicationUser> GetNonFolloweesFromFriendProfile(string id);
        //public List<ApplicationUser> GetMutualFollowers(string loggedInUserId, string friendUserId);
        //public List<ApplicationUser> GetNonMutualFollowers(string loggedInUserId, string friendUserId);
        //public List<ApplicationUser> GetNonMutualFollowers(string loggedInUserId, string friendUserId);
        //public List<UserRelationship> MutualFollowers(List<UserRelationship> userFollowers, string loggedInUserId, string friendUserId);
        //public List<UserRelationship> GetFollowersAndFollowings(string id);

        ///////// Abadeer
        //public UserRelationship GetFollowerRelationship(string followerId, string followeeId);
        //public UserRelationship GetFollowingRelationship(string followerId, string followeeId);


        //public void removeFollower(string id);
        //public void removeFollowing(string id);

        public  Task<bool> IsFollowing(string followerId, string followeeId);
        //public  Task Follow(string followeeId, string followerId);
        public Task Accept_FollowRequest(string receiverId, string senderId);
        public  Task Followback(string followeeid, string loginuser);
        public  List<ApplicationUser> GetRequestedUsers(string id);



    }
}
