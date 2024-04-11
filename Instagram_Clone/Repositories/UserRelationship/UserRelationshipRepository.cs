using Instagram_Clone.Authentication;
using Instagram_Clone.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;

namespace Instagram_Clone.Repositories.UserFollowRepo
{
    public class UserRelationshipRepository : Repository<UserRelationship>, IUserRelationshipRepository
    {
        private readonly Context context;

        public UserRelationshipRepository(Context _context) : base(_context)
        {
            context = _context;
        }
        public ApplicationUser GetById(string id)
        {
            ApplicationUser user = context.Users.Include(i => i.Followers)
                .FirstOrDefault(i => i.Id == id);
            return user;
        }
        public List<UserRelationship> GetFollowers(string id)
        {
            List<UserRelationship> followers = context.UserRelationship
                .Where(ur => ur.FolloweeId == id && ur.Follower.IsDeleted ==false && ur.IsDeleted==false)
                .Include(ur => ur.Follower)
                .Include(ur => ur.Follower.ProfilePicture)
                .ToList();
            return followers;
        }
        public List<UserRelationship> GetFollowees(string id)
        {
            List<UserRelationship> Following = context.UserRelationship
                .Where(ur => ur.FollowerId == id && ur.Followee.IsDeleted == false && ur.IsDeleted == false)
                .Include(ur => ur.Followee)
                .Include(ur => ur.Followee.ProfilePicture)
                .ToList();
            return Following;
        }

        //public List<UserRelationship> GetFollowersAndFollowings(string id)
        //{
        //    List<UserRelationship> followers = context.UserRelationship
        //        .Where(ur => ur.FolloweeId == id && ur.Follower.IsDeleted == false && ur.IsDeleted == false)
        //        .Include(ur => ur.Follower)
        //        .Include(ur => ur.Follower.ProfilePicture)
        //        .ToList();

        //    List<UserRelationship> followings = context.UserRelationship
        //        .Where(ur => ur.FollowerId == id && ur.Followee.IsDeleted == false && ur.IsDeleted == false)
        //        .Include(ur => ur.Followee)
        //        .Include(ur => ur.Followee.ProfilePicture)
        //        .ToList();

        //    List<UserRelationship> followersAndFollowings = followers.Concat(followings).ToList();

        //    return followersAndFollowings;
        //}

        public List<ApplicationUser> GetFollowersAndFollowings(string id)
        {
            List<ApplicationUser> followers = context.UserRelationship
                .Where(ur => ur.FolloweeId == id && ur.Follower.IsDeleted == false && ur.IsDeleted == false)
                .Select(ur => ur.Follower)
                .Include(u => u.ProfilePicture)
                .ToList();

            List<ApplicationUser> followings = context.UserRelationship
                .Where(ur => ur.FollowerId == id && ur.Followee.IsDeleted == false && ur.IsDeleted == false)
                .Select(ur => ur.Followee)
                .Include(u => u.ProfilePicture)
                .ToList();

            List<ApplicationUser> followersAndFollowings = followers.Concat(followings).ToList();

            return followersAndFollowings;
        }


        


        //انا هديك ال فلويي و انت تبعتلى الفلور
        //public List<UserRelationship> searchFollowers(string Name)
        //{


        //   List< UserRelationship> searchedUsers =
        //        context.UserRelationship
        //        .Include(ur => ur.Follower)
        //        .Where(ur => ur.Follower.UserName.Contains(Name))
        //        .ToList();

        //    return searchedUsers;
        //}

        public List<ApplicationUser> searchFollowers(string Name )
        {
            string lowerName = Name.ToLower();
            List<ApplicationUser> searchedUsers =
                 context.Users
                 .Include(user => user.ProfilePicture)

                 .Include(user => user.Followers)
                 .Where(ur => ur.UserName.ToLower().Contains(lowerName) ).ToList();

            return searchedUsers;
        }

        public List<ApplicationUser> searchFollowees(string Name)
        {
            string lowerName = Name.ToLower();
            List<ApplicationUser> searchedUsers =
                context.Users
                 .Include(user => user.ProfilePicture)

                .Include(u => u.Following)
                .Where(u =>u.UserName.ToLower().Contains(lowerName) ).ToList();
            return searchedUsers;
        }
        /// <summary>
        /// ///
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="id"></param>
        /// <returns></returns>
       public List<UserRelationship> searchFollowers2(string Name, string id)
        {
            
            string lowerName = Name.ToLower();
            List<UserRelationship> Followers = GetFollowers(id);
            List<UserRelationship> searchedUsers = new List<UserRelationship>();
            foreach (var item in Followers)
            {
                if(item.Follower.UserName.ToLower().Contains(lowerName))
                {
                    searchedUsers.Add(item);
                }
            }



            return searchedUsers;
        }

        public List<UserRelationship> searchFollowees2(string Name, string id)
        {
            string lowerName = Name.ToLower();
            List<UserRelationship> Followees = GetFollowees(id);
            List<UserRelationship> searchedUsers = new List<UserRelationship>();
            foreach (var item in Followees)
            {
                if (item.Followee.UserName.ToLower().Contains(lowerName))
                {
                    searchedUsers.Add(item);
                }
            }
            return searchedUsers;
        }


        //public List<UserRelationship> searchFollowers(string name)
        //{
        //    List<UserRelationship> searchedUsers =
        //        context.UserRelationship
        //        .Include(ur => ur.Follower)
        //        .Where(ur => ur.Follower.UserName.Contains(name))
        //        .ToList();
        //    return searchedUsers;
        //}

        /////Abadeer

        //public UserRelationship GetFollowerRelationship(string followerId, string followeeId)
        //{
        //    return context.UserRelationship.FirstOrDefault(ur => ur.FollowerId == followeeId && ur.FolloweeId == followerId);
        //}
        //public UserRelationship GetFollowingRelationship(string followerId, string followeeId)
        //{
        //    return context.UserRelationship.FirstOrDefault(ur => ur.FollowerId == followerId && ur.FolloweeId == followeeId);
        //}
        //public void removeFollowing(string id)
        //{

        //    UserRelationship following = context.UserRelationship.FirstOrDefault(u => u.FolloweeId == id);
        //    if (following != null)
        //    {
        //        following.IsDeleted = true;
        //        context.SaveChanges();
        //    }

        //}
        //public void removeFollower(string id)
        //{
        //    UserRelationship Follower = context.UserRelationship.FirstOrDefault(u => u.FollowerId == id);
        //    if (Follower != null)
        //    {
        //        Follower.IsDeleted = true;
        //        context.SaveChanges();
        //    }

        //}

        //public bool UnFollow(string LoginUserID , string FollowerID)
        //{
        //    var user = context.Users.FirstOrDefault(u => u.Id == LoginUserID);
        //    var Relation = context.UserRelationship
        //   .FirstOrDefault(ur=>ur.user.Id == LoginUserID && ur=>ur.Follower.Id == FollowerID);
        //}


        public void GetFollowingRelationship(string followeeId, string LoginUsrer)
        {
            var relation= context.UserRelationship
                .Where(ur=>ur.IsDeleted==false)
                .FirstOrDefault(ur => ur.FollowerId == LoginUsrer && ur.FolloweeId == followeeId);
            if (relation != null)
            {
                relation.IsDeleted=true;
                Save();
                //context.SaveChanges();
            }
        }

        public void GetFollowersRelationship(string followerId, string LoginUsrer)
        {
            var relation = context.UserRelationship
                .Where(ur => ur.IsDeleted == false)
                .FirstOrDefault(ur => ur.FolloweeId == LoginUsrer && ur.FollowerId == followerId);
            if (relation != null)
            {
                relation.IsDeleted = true;
                Save();

                //context.SaveChanges();
            }

        }

        public void AddUserRelation(string followeeId, string loginUser)
        {
            // Assuming context is your DbContext instance
            ApplicationUser followerUser = context.Users.FirstOrDefault(u => u.Id == loginUser);
            ApplicationUser followingUser = context.Users.FirstOrDefault(u => u.Id == followeeId);

            if (followingUser != null && followerUser != null)
            {
                // Check if the relationship already exists
                bool alreadyExists = context.UserRelationship.Any(ur => ur.FollowerId == followerUser.Id && ur.FolloweeId == followingUser.Id);

                if (!alreadyExists)
                {
                    var relation = new UserRelationship
                    {
                        IsDeleted = false, // Assuming default value
                        FolloweeId = followingUser.Id,
                        FollowerId = followerUser.Id
                    };

                    //var relation2 = new UserRelationship
                    //{
                    //    IsDeleted = false, // Assuming default value
                    //    FollowerId = followingUser.Id,
                    //    FolloweeId = followerUser.Id
                    //};

                    //context.UserRelationship.Add(relation2);

                    context.UserRelationship.Add(relation);
                    Save();
                    //context.SaveChanges();
                }
                else
                {
                    // Handle case where the relationship already exists
                    // You can add logging or throw an exception here
                }
            }
            else
            {
                // Handle case where either followerUser or followingUser is null
                // You can add logging or throw an exception here
            }
        }

        //////////////////////////////////////////////////////////////

        //public List<ApplicationUser> GetMutualFollowers(string loggedInUserId, string friendUserId)
        //{
        //    // Get followers of the logged-in user
        //    List<UserRelationship> loggedInUserFollowers = GetFollowers(loggedInUserId);

        //    // Get followers of the friend
        //    List<UserRelationship> friendFollowers = GetFollowers(friendUserId);

        //    // Extract user IDs from the followers
        //    var loggedInUserFollowerIds = loggedInUserFollowers.Select(ur => ur.FollowerId).ToList();
        //    var friendFollowerIds = friendFollowers.Select(ur => ur.FollowerId).ToList();

        //    // Find mutual follower IDs
        //    var mutualFollowerIds = loggedInUserFollowerIds.Intersect(friendFollowerIds).ToList();

        //    // Get the ApplicationUser objects corresponding to the mutual follower IDs
        //    var mutualFollowers = context.Users
        //        .Include(u => u.ProfilePicture)
        //        .Where(u => mutualFollowerIds.Contains(u.Id))
        //        .ToList();

        //    return mutualFollowers;

        //}

        //public List<ApplicationUser> GetNonMutualFollowers(string loggedInUserId, string friendUserId)
        //{
        //    // Get followers of the logged-in user
        //    List<UserRelationship> loggedInUserFollowers = GetFollowers(loggedInUserId);

        //    // Get followers of the friend
        //    List<UserRelationship> friendFollowers = GetFollowers(friendUserId);

        //    // Extract user IDs from the followers
        //    var loggedInUserFollowerIds = loggedInUserFollowers.Select(ur => ur.FollowerId).ToList();
        //    var friendFollowerIds = friendFollowers.Select(ur => ur.FollowerId).ToList();

        //    // Find followers of the logged-in user who are not also followers of the friend
        //    var nonMutualFollowerIds = loggedInUserFollowerIds.Except(friendFollowerIds).ToList();

        //    // Remove the friend's user ID from the non-mutual follower IDs
        //    nonMutualFollowerIds.Remove(friendUserId);

        //    // Get the ApplicationUser objects corresponding to the non-mutual follower IDs
        //    var nonMutualFollowers = context.Users
        //        .Include(u => u.ProfilePicture)
        //        .Where(u => nonMutualFollowerIds.Contains(u.Id))
        //        .ToList();

        //    return nonMutualFollowers;
        //}

        public List<ApplicationUser> GetNonFollowees(string id)
        {
        List<ApplicationUser> allUsers = context.Users.Include(u => u.ProfilePicture).ToList();

            // Get the IDs of the users followed by the given user
            List<string> followedUserIds = context.UserRelationship
                .Where(ur => ur.FollowerId == id && ur.Followee.IsDeleted == false && ur.IsDeleted == false)
                .Select(ur => ur.FolloweeId)
                .ToList();

            // Remove followed users from the list of all users
            var nonFollowedUsers = allUsers.Where(u => !followedUserIds.Contains(u.Id) && u.Id != id).ToList();

            return nonFollowedUsers;

        }
        //public List<ApplicationUser> GetNonFolloweesFromFriendProfile(string id)
        //{
        //    List<ApplicationUser> allUsers = context.Users.Include(u => u.ProfilePicture).ToList();

        //    // Get the IDs of the users followed by the given user
        //    List<string> followedUserIds = context.UserRelationship
        //        .Where(ur => ur.FollowerId == id && ur.Followee.IsDeleted == false && ur.IsDeleted == false)
        //        .Select(ur => ur.FolloweeId)
        //        .ToList();

        //    // Remove followed users from the list of all users
        //    var nonFollowedUsers = allUsers.Where(u => !followedUserIds.Contains(u.Id) && u.Id != id).ToList();

        //    return nonFollowedUsers;

        //}



        public List<ApplicationUser> GetAppUserFollowees(string id)
        {
            List<ApplicationUser> allUsers = context.Users.Include(u => u.ProfilePicture).ToList();

            // Get the IDs of the users followed by the given user
            List<string> followedUserIds = context.UserRelationship
                .Where(ur => ur.FollowerId == id && ur.Followee.IsDeleted == false && ur.IsDeleted == false)
                .Select(ur => ur.FolloweeId)
                .ToList();

            // Retrieve the followed users from the list of all users
            var followedUsers = allUsers.Where(u => followedUserIds.Contains(u.Id) && u.Id != id).ToList();

            return followedUsers;
        }




        public List<ApplicationUser> GetRandomlyTopFive(string id)
        {
            List<ApplicationUser> nonFollowedUsers = GetNonFollowees(id);

            // Shuffle the list randomly
            var random = new Random();

            var shuffledUsers = nonFollowedUsers.OrderBy(u => random.Next()).ToList();

            // Return up to 5 users, or all users if less than 5
            return shuffledUsers.Take(5).ToList();
        }
    }
}
