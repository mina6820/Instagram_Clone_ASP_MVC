﻿using Instagram_Clone.Authentication;
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

        public void AddUserRelation(string followeeId, string loginUser);

        //public List<UserRelationship> MutualFollowers(List<UserRelationship> userFollowers, string loggedInUserId, string friendUserId);
        //public List<UserRelationship> GetFollowersAndFollowings(string id);

        ///////// Abadeer
        //public UserRelationship GetFollowerRelationship(string followerId, string followeeId);
        //public UserRelationship GetFollowingRelationship(string followerId, string followeeId);


        //public void removeFollower(string id);
        //public void removeFollowing(string id);



    }
}
