﻿using Instagram_Clone.Models;

namespace Instagram_Clone.Repositories.PostRepo
{
    public class PostRepository : Repository<Post> , IPostRepository
    {
        Context context;
        public PostRepository(Context context) : base(context)
        {
            // No need to store the context separately if not used elsewhere
        }



    }
}
