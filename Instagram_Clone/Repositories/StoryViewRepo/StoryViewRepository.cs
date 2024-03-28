﻿namespace Instagram_Clone.Repositories.StoryViewRepo
{
    public class StoryViewRepository:Repository<StoryView>, IStoryViewRepository
    {
        Context context;
        public StoryViewRepository(Context context) : base(context)
        {
            // No need to store the context separately if not used elsewhere
        }
    }
}
