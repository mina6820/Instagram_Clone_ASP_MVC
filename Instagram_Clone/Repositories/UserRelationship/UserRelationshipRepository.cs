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
                .Include(Following => Following.Followee)
                .ToList();
            return Following;
        }



        public List<UserRelationship> searchFollowers(string Name)
        {
           List< UserRelationship> searchedUsers = context.UserRelationship
                .Include(ur => ur.Follower)
                .Where(ur => ur.Follower.UserName.Contains(Name))
                .ToList();
            return searchedUsers;
        }

        public List<ApplicationUser> searchFollowees( string Name)
        {
            List<ApplicationUser> users = context.Users.Where(u => u.UserName.Contains(Name)).Include(u => u.Following).ToList();
            return users; 
        }

        

        
    }
}
