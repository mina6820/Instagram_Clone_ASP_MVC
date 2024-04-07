﻿using Instagram_Clone.Models;
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
            return context.Posts.Where(p => p.UserId == userID).ToList();
        }

        public Post? GetPostByIDWithLikes(int id)
        {
            return context.Posts.Include(p => p.Likes.Where(l => l.IsDeleted == false)).FirstOrDefault(p => p.Id == id);
        }

       
        public List<Post>? GetAllPostsWithPhotosAndLikes()
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

        //public List<Post> getyyy(string userid)
        //{
        //    dfkjvdnnvnd
        //    return context.Posts.Where(p => p.UserId == userid).Include(p=> p.User).ThenInclude(p => p.Following).ToList();
        //}

        

    }
}
