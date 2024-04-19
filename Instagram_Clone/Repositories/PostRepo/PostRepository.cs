using Instagram_Clone.Models;
using Microsoft.EntityFrameworkCore;

namespace Instagram_Clone.Repositories.PostRepo
{
    public class PostRepository : Repository<Post> , IPostRepository
    {
        Context context;
        public PostRepository(Context context) : base(context)
        {
            // No need to store the context separately if not used elsewhere
            this.context = context;
        }

        public List<Post>? GetAllPostsByUserID(string userID)
        {
            return context.Posts.Where(p => p.UserId == userID && p.IsDeleted == false).ToList();
        }

        public Post? GetPostByIDWithLikes(int id)
        {
            return context.Posts.Include(p => p.Likes.Where(l => l.IsDeleted == false)).FirstOrDefault(p => p.Id == id);
        }

        public Post? GetPostByIDWithUser(int id)
        {
            return context.Posts.Include(p => p.User).FirstOrDefault(p => p.Id == id);
        }

        //public List<Post>? GetAllPostsWithPhotosAndLikes()
        //{
        //    return context.Posts.Where(p => p.IsDeleted == false)
        //        .Include(p => p.Likes.Where(l => l.IsDeleted == false))
        //        .Include(p => p.Comments)
        //        .Include(p => p.User)
        //        .ThenInclude(p => p.ProfilePicture)

        //        .Include(p => p.User)
        //        .ThenInclude(p => p.Following)

        //        .ToList();
        //}
        public List<Post>? GetAllPostsWithPhotosAndLikes(string id)
        {
            //return context.Posts
            //    .Where(p => p.IsDeleted == false && context.Users.FirstOrDefault(u => u.Id == id).Following.Any(f => f.FolloweeId == p.UserId))
            //    .Include(p => p.Likes.Where(l => l.IsDeleted == false))
            //    .Include(p => p.Comments)
            //    .Include(p => p.User)
            //    .ThenInclude(p => p.ProfilePicture)
            //    .ToList();


            List<string> followingIds = context.UserRelationship
               .Where(ur => ur.FollowerId == id && ur.Followee.IsDeleted == false && ur.IsDeleted == false)
               .Select(ur => ur.FolloweeId)
               .ToList();

            // Retrieve stories posted by the users you are following
            var posts = context.Posts
                                 .Include(p => p.Likes.Where(l => l.IsDeleted == false))
                                 .Include(p => p.Comments)
                                .Include(p => p.User)
                                .ThenInclude(p => p.ProfilePicture)
                                .Where(s => s.IsDeleted == false)
                               .Where(s => followingIds.Contains(s.UserId))
                               .ToList();

            return posts;
        }


        public List<Post> GetMyPosts(string id)
        {
            return context
                .Posts
                .Include(p => p.Likes.Where(l => l.IsDeleted == false))
                .Include(p => p.Comments)
                .Include(p => p.User)
                .ThenInclude(p => p.ProfilePicture)
                .Where(s => s.IsDeleted == false)
                .Where(s => s.UserId == id)
                .ToList();
        }


        //public Post? GetPostwithUserAndCommentsAndFollowersById(int id)
        //{
        //    return context.Posts.Include(p => p.Comments)

        //        .Include(p => p.User)
        //        .ThenInclude(u => u.ProfilePicture)
        //        .Include(p => p.User)
        //        .ThenInclude(u => u.Followers)
        //        .FirstOrDefault(p => p.Id == id && p.IsDeleted == false);
        //}

        //public Post? GetPostwithUserAndCommentsAndFollowersById(int id)
        //{
        //    return context.Posts.Include(p => p.Comments)
        //        .ThenInclude(c => c.User) // Include users from comments
        //        .Include(p => p.User)
        //        .ThenInclude(u => u.ProfilePicture)
        //        .Include(p => p.User)
        //        .ThenInclude(u => u.Followers)
        //        .FirstOrDefault(p => p.Id == id && p.IsDeleted == false);
        //}

        public Post? GetPostwithUserAndCommentsAndFollowersById(int id)
        {
            return context.Posts.Include(p => p.Comments.Where(c=> c.IsDeleted== false && c.PostId == id))
                .ThenInclude(c => c.User)
                .ThenInclude(u => u.ProfilePicture)// Include users from comments
                .Include(p => p.User)
                .ThenInclude(u => u.ProfilePicture)
                .Include(p => p.User)
                .ThenInclude(u => u.Followers)
                .FirstOrDefault(p => p.Id == id && p.IsDeleted == false);
        }


        //public List<Post> getyyy(string userid)
        //{

        //    return context.Posts.Where(p => p.UserId == userid).Include(p => p.User).ThenInclude(p => p.Following).ToList();
        //}


        public List<Post>? GetAllPosts()
        {
            return context.Posts.Where(p => p.IsDeleted == false)
                .Include(p => p.Likes.Where(l => l.IsDeleted == false))
                .Include(p => p.Comments)
                .Include(p => p.User)
                .ThenInclude(p => p.ProfilePicture)

                .Include(p => p.User)
                .ThenInclude(p => p.Following)

                .ToList();
        }
    }
}
