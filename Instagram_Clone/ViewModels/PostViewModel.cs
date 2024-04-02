namespace Instagram_Clone.ViewModels
{
    public class PostViewModel
    {
        public string? UserName { get; set; }

        public string? TimeAgo { get; set; }

        public List<string>? ImagesNames { get; set; } = new List<string>();

        public int? Likes { get; set; }

        public string? Caption { get; set; }

        public profilePhoto ProfilePhoto { get; set; }



    }
}
