using Instagram_Clone.Authentication;
using Instagram_Clone.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.ComponentModel;
namespace Instagram_Clone.Repositories.UserFollowRepo
{
    public class UserRelationshipRepository:Repository<UserRelationship> , IUserRelationshipRepository
    {
        Context context;
        public UserRelationshipRepository(Context context) : base(context)
        {
            // No need to store the context separately if not used elsewhere
        }


      
        //public List<UserRelationship> GetAll(string include="")
        //{
        //     List < UserRelationship > Lists = context.UserRelationship.ToList();
        //    return Lists;

        //}
        //public ApplicationUser GetById(string id)
        //{
        //    ApplicationUser user = context.Users.FirstOrDefault(i => i.Id == id);
        //    return user;
        //}
        //public List<ApplicationUser> GetFollowers(string id)
        //{
        //    UserRelationship userRelationship = context.UserRelationship.FirstOrDefault(x => x.FollowerId == id);

        //    if (userRelationship != null)
        //    {
        //        //Get the followers based on the user relationship
        //        List<ApplicationUser> followers = context.UserRelationship
        //        .Where(r => r.FolloweeId == id)
        //        .Select(r => r.Followee)
        //        .ToList();

        //        return followers;
        //    }
        //    else
        //    {
        //        return new List<ApplicationUser>(); // Return an empty list if the user relationship is not found
        //    }




            //UserRelationship userRelationship = new UserRelationship();

            //ApplicationUser user = GetById(id);
            //List<ApplicationUser> Followers = userRelationship.FollowerId;
            //return Followers;



            //var user = context.Users
            //.Include(u => u.ProfilePicture)
            //.FirstOrDefault(u => u.Id == id);

            //if (user != null && user != null)
            //{
            //    return context.Users
            //        .Include(u => u.ProfilePicture)
            //        .Where(u => user..Contains(u.Id))
            //        .ToList();
            //}

            //return new List<ApplicationUser>();
        //}

    }
}
