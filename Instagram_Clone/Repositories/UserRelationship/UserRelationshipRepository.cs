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
            ApplicationUser user = context.Users.FirstOrDefault(i => i.Id == id);
            return user;
        }



        public List<ApplicationUser> GetFollowers(string id)
        {
            //// Find the user's followers based on the FollowerId
            //List<string> followerIds = context.UserRelationship
            //    .Where(ur => ur.FolloweeId == id)
            //    .Select(ur => ur.FollowerId)
            //    .ToList();

            //// Get the ApplicationUser objects corresponding to the followerIds
            //List<ApplicationUser> followers =
            //     context.Users
            //    .Where(u => followerIds.Contains(u.Id))
            //    .ToList();

            //return followers;
            // Find the user by id
            ApplicationUser user = context.Users.FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                // Access the Followers property directly
                //List<ApplicationUser> followers = context.Users
                //    .Select(x=>x.Followers)
                //    .FirstOrDefault(u => u.Id == id)
                //    .ToList();
                //    //user.Followers;
                List<ApplicationUser> followers =
                    user.Followers
                    .Select(ur => ur.Follower)
                    .ToList();

                return followers;
            }
            else
            {
                return new List<ApplicationUser>(); // Return an empty list if the user is null
            }

        }
        public List<ApplicationUser> GetFollowees(string id)
        {
            ApplicationUser user = context.Users.FirstOrDefault(ur => ur.Id == id);
            if (user != null)
            {
                List<ApplicationUser> followees= user.Following.Select(ur => ur.Followee).ToList();
                return followees;
            }
            return new List<ApplicationUser>();
        }

        public List<ApplicationUser> searchFollowers(string id , string name)
        {
            ApplicationUser user = context.Users.FirstOrDefault(x => x.Id==id);
            if (user != null)
            {
                List<ApplicationUser> searched_Users= 
                    user.Followers//userRelation
                    .Where(x => x.Follower.FirstName.Contains( name) || x.Follower.LastName.Contains( name))//AppUser
                    .Select(x=>x.Follower)//AppUser
                    .ToList();
                return searched_Users;
                
            }
            else
            {  return new List<ApplicationUser>(); }
        }

        public List<ApplicationUser> searchFollowees(string id, string name)
        {
            ApplicationUser user = context.Users.FirstOrDefault(x => x.Id == id);
            if (user != null)
            {
                List<ApplicationUser> searched_Users =
                    user.Following//userRelation
                    .Where(x => x.Followee.FirstName .Contains( name) || x.Followee.LastName.Contains( name))//AppUser
                    .Select(x => x.Followee)//AppUser
                    .ToList();
                return searched_Users;

            }
            else
            { return new List<ApplicationUser>(); }
        }
    }
}
