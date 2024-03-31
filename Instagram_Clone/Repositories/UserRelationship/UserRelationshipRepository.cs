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
                .Include(follower => follower.Follower)
                .Select(ur => ur.Follower)
                .ToList();

            return followers;
        }
        public List<ApplicationUser> GetFollowees(string id)
        {
            List<ApplicationUser> Following = context.UserRelationship
                .Where(ur => ur.FollowerId == id)
                .Include(Following => Following.Followee)
                .Select(ur => ur.Followee)
                .ToList();
            return Following;
        }
        //انا هديك ال فلويي و انت تبعتلى الفلور
        public List<ApplicationUser> searchFollowers(string name)
        {
            ApplicationUser user = context.Users.FirstOrDefault(u=>u.UserName.Contains(name));
            List<ApplicationUser> users = GetFollowers(user.Id);
            //List<ApplicationUser> searchedUsers= 
            //    context.UserRelationship
            //    .Where(ur => ur.Follower.UserName.Contains(name))//AppUser
            //    .Include(follower => follower.Follower)
            //    .Select(ur=>ur.Follower)//AppUser
            //    .ToList();
            return users;     
        }

        public List<ApplicationUser> searchFollowees( string name)
        {
            List<ApplicationUser> searchedUsers =
                context.UserRelationship
                .Where(ur => ur.Followee.UserName.Contains(name))//AppUser
                .Include(following => following.Followee)
                .Select(ur => ur.Followee)//AppUser
                .ToList();
            return searchedUsers;
          
        }
    }
}
