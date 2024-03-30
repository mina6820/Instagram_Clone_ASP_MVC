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



        public List<ApplicationUser> GetFollowers(string id)
        {
            List<ApplicationUser> followers = context.UserRelationship
           .Where(ur => ur.FolloweeId == id)
           .Select(ur => ur.Follower)
           .ToList();
            return followers;   
        }
        public List<ApplicationUser> GetFollowees(string id)
        {
            List<ApplicationUser> Following = context.UserRelationship
                .Where(ur => ur.FollowerId == id)
                .Select(ur => ur.Followee)
                .ToList();
            return Following;
        }

        public List<ApplicationUser> searchFollowers(string name)
        {
            List<ApplicationUser> searchedUsers= 
                context.UserRelationship
                .Where(ur => ur.Follower.FirstName.Contains(name) || ur.Follower.LastName.Contains(name))//AppUser
                .Select(ur=>ur.Follower)//AppUser
                .ToList();
            return searchedUsers;     
        }

        public List<ApplicationUser> searchFollowees( string name)
        {
            List<ApplicationUser> searchedUsers =
                context.UserRelationship
                .Where(ur => ur.Followee.FirstName.Contains(name) || ur.Followee.LastName.Contains(name))//AppUser
                .Select(ur => ur.Followee)//AppUser
                .ToList();
            return searchedUsers;
          
        }
    }
}
