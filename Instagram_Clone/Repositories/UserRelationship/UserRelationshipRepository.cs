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
            List<UserRelationship> userRelationships = context.UserRelationship
            .Where(ur => ur.FolloweeId == id)
            .Include(ur => ur.Follower)
            .ToList();

            return userRelationships;

        }

        public List<UserRelationship> GetFollowees(string id)
        {
            List<UserRelationship> Following = context.UserRelationship
                 .Where(ur => ur.FollowerId == id)
                .Include(ur => ur.Followee)
                .ToList();
            return Following;
        }
       
        public List<UserRelationship> searchFollowers(string name)
        {
            List<UserRelationship> searchedUsers =
                context.UserRelationship
                .Where(ur => ur.Follower.UserName.Contains(name))
                 .Include(ur => ur.Follower)
                .ToList();
            return searchedUsers;
        }

      




        public List<UserRelationship> searchFollowees( string name)
        {
            List<UserRelationship> searchedUsers =
                context.UserRelationship
                .Where(ur => ur.Followee.UserName.Contains(name))//AppUser
                .Include(ur => ur.Followee)  
                .ToList();
            return searchedUsers;
          
        }
    }
}
