using Instagram_Clone.Repositories.PhotoRepo.ProfilePhotoContainer;
using Instagram_Clone.Repositories.PhotoRepo;
using Microsoft.EntityFrameworkCore;
using Instagram_Clone.Repositories.UserFollowRepo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Instagram_Clone.Repositories.NotificationRepo.FollowRequestContainer
{
    public class FollowRequestRepository: NotificationRepository<FollowRequest>, IFollowRequestRepository
    {
        private readonly Context context;
        private readonly IUserRelationshipRepository userRelationshipRepository;
        public FollowRequestRepository(Context _context, IUserRelationshipRepository _userRelationshipRepository) : base(_context)
        {
            context = _context;
            userRelationshipRepository = _userRelationshipRepository;
        }


        
        public void Follow(string followeeId, string loginUser)
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

        public void FollowBack(string followeeId, string loginUser)
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

                    var relation2 = new UserRelationship
                    {
                        IsDeleted = false, // Assuming default value
                        FollowerId = followingUser.Id,
                        FolloweeId = followerUser.Id
                    };

                    context.UserRelationship.Add(relation2);
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

       

    }
}
