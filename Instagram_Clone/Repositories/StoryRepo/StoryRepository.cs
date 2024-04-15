﻿namespace Instagram_Clone.Repositories.StoryRepo
{
    public class StoryRepository :Repository<Story> , IStoryRepository
    {
        Context context;
        public StoryRepository(Context _context) : base(_context)
        {
            context = _context;
        }

        public List<Story> GetAllStories(string id)
        {
            // Retrieve the IDs of the users iam following
            List<string> followingIds = context.UserRelationship
                .Where(ur => ur.FollowerId == id && ur.Followee.IsDeleted == false && ur.IsDeleted == false)
                .Select(ur => ur.FolloweeId)
                .ToList();

            // Retrieve stories posted by the users you are following
            var stories = context.Stories
                               .Where(s => followingIds.Contains(s.UserId))
                               .ToList();

            return stories;
        }
    }
}
