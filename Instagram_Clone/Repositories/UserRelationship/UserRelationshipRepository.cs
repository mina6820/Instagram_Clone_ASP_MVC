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

            }

        }
     
        public async Task<bool> IsFollowing(string followeeId, string followerId)
        {
            return await context.UserRelationship.AnyAsync(ur => ur.FollowerId == followerId && ur.FolloweeId == followeeId && !ur.IsDeleted);
        }

        //public async Task Follow(string followeeId, string followerId)
        public async Task Accept_FollowRequest(string receiverId, string senderId)

        {
            if (!await IsFollowing(receiverId, senderId))
            {
                var relation = new UserRelationship
                {
                    IsDeleted = false,
                    FolloweeId = receiverId,
                    FollowerId = senderId
                };

                context.UserRelationship.Add(relation);
                await context.SaveChangesAsync();
            }
        }

        //public async Task Accept_FollowRequest(string followeeId, string followerId)



        //followback

        public async Task Followback(string followeeid, string loginuser)
        {
            // assuming context is your dbcontext instance
            ApplicationUser followeruser = context.Users.FirstOrDefault(u => u.Id == loginuser);
            ApplicationUser followinguser = context.Users.FirstOrDefault(u => u.Id == followeeid);

            if (followinguser != null && followeruser != null)
            {
                // check if the relationship already exists
                bool alreadyexists = context.UserRelationship.Any(ur => ur.FollowerId == followeruser.Id && ur.FolloweeId == followinguser.Id);

                if (!alreadyexists)
                {

                    var relation2 = new UserRelationship
                    {
                        IsDeleted = false, // assuming default value
                        FollowerId = followinguser.Id,
                        FolloweeId = followeruser.Id
                    };



                    var relation = new UserRelationship
                    {
                        IsDeleted = false, // assuming default value
                        FolloweeId = followinguser.Id,
                        FollowerId = followeruser.Id
                    };

                    context.UserRelationship.Add(relation);
                    context.UserRelationship.Add(relation2);
                    Save();
                    //context.savechanges();
                }
                else
                {
                    // handle case where the relationship already exists
                    // you can add logging or throw an exception here
                }
            }
            else
            {
                // handle case where either followeruser or followinguser is null
                // you can add logging or throw an exception here
            }
        }




        //public List<ApplicationUser> GetNonFollowees(string id)
        //{
        //List<ApplicationUser> allUsers = context.Users
        //        .Where(u=>u.IsDeleted==false)
        //        .Include(u => u.ProfilePicture)
        //        .ToList();

        //    // Get the IDs of the users followed by the given user
        //    List<string> followedUserIds = context.UserRelationship
        //        .Where(ur => ur.FollowerId == id && ur.Followee.IsDeleted == false 
        //        && ur.IsDeleted == false && ur.Follower.IsDeleted==false)
        //        .Select(ur => ur.FolloweeId)
        //        .ToList();

        //    // Remove followed users from the list of all users
        //    var nonFollowedUsers = allUsers.Where(u => !followedUserIds.Contains(u.Id) && u.Id != id).ToList();

        //    return nonFollowedUsers;

        //}


        public List<ApplicationUser> GetNonFollowees(string id)
        {
            List<ApplicationUser> allUsers = context.Users
                .Where(u => u.IsDeleted == false)
                .Include(u => u.ProfilePicture)
                .ToList();

            // Get the IDs of the users followed by the given user
            List<string> followedUserIds = context.UserRelationship
                .Where(ur => ur.FollowerId == id &&
                             ur.Followee.IsDeleted == false &&
                             ur.IsDeleted == false &&
                             ur.Follower.IsDeleted == false)
                .Select(ur => ur.FolloweeId)
                .ToList();

            // Get the IDs of users who have sent follow requests that are not yet accepted (requested)
            List<string> requestedUserIds = context.FollowRequest_notifications
                .Where(frn => frn.SenderId == id && frn.IsRequested == true )
                .Select(frn => frn.ReceiverId)
                .ToList();

            // Remove followed users and users who have sent requested follow requests from the list of all users
            var nonFollowedUsers = allUsers
                .Where(u => !followedUserIds.Contains(u.Id) &&
                            !requestedUserIds.Contains(u.Id) &&
                            u.Id != id)
                .ToList();

            return nonFollowedUsers;
        }

        public List<ApplicationUser> GetRequestedUsers(string id)
        {
            List<ApplicationUser> requestedUsers = context.FollowRequest_notifications
                    .Where(frn => frn.SenderId == id && frn.IsRequested == true)
                    .Join(context.Users,
                          frn => frn.ReceiverId,
                          user => user.Id,
                          (frn, user) => user)
                    .ToList();
            return requestedUsers;

        }


        public List<ApplicationUser> GetAppUserFollowees(string id)
        {
            List<ApplicationUser> allUsers = context.Users
                 .Where(u => u.IsDeleted == false)
                .Include(u => u.ProfilePicture)
                .ToList();

            // Get the IDs of the users followed by the given user
            List<string> followedUserIds = context.UserRelationship
                .Where(ur => ur.FollowerId == id && ur.Followee.IsDeleted == false
                && ur.IsDeleted == false && ur.Follower.IsDeleted == false)
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
