using Instagram_Clone.Authentication;
using Instagram_Clone.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

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
                .Where(ur => ur.FolloweeId == id)
                .Include(ur => ur.Follower)
                .ToList();
            return followers;
        }
        public List<UserRelationship> GetFollowees(string id)
        {
            List<UserRelationship> Following = context.UserRelationship
                .Where(ur => ur.FollowerId == id)
                .Include(ur => ur.Followee)
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

        public List<ApplicationUser> searchFollowers(string Name)
        {
            string lowerName = Name.ToLower();
            List<ApplicationUser> searchedUsers =
                 context.Users
                 .Include(user => user.Followers)
                 .Where(ur => ur.UserName.ToLower().Contains(lowerName)).ToList();

            return searchedUsers;
        }

        public List<ApplicationUser> searchFollowees(string Name)
        {
            string lowerName = Name.ToLower();
            List<ApplicationUser> searchedUsers =
                context.Users
                .Include(u => u.Following)
                .Where(u =>u.UserName.ToLower().Contains(lowerName)).ToList();
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






    }
}
