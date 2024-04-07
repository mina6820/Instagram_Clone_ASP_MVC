using Instagram_Clone.Authentication;
using Instagram_Clone.Models;
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


    }
}
