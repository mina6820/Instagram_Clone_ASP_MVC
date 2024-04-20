namespace Instagram_Clone.ViewModels
{
    public class PostViewModel
    {
        public int ID { get; set; }
        public string? UserName { get; set; }

        public string? UserId { get; set; } 

        //public string? CurrentUserID { get; set; }

        public string? TimeAgo { get; set; }

        public DateTime CreatedAt { get; set; }
        public List<string>? ImagesNames { get; set; } = new List<string>();

        public List<Like>? Likes { get; set; }

        public List<Comment> Comments { get; set; }

        public string? Caption { get; set; }

        public profilePhoto ProfilePhoto { get; set; }

        public List<UserRelationship>? Followers { get; set; }


    }
}
