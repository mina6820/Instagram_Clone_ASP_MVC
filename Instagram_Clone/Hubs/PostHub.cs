using Instagram_Clone.Repositories.LikeRepo;
using Instagram_Clone.Repositories.PostRepo;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;
using static System.Net.Mime.MediaTypeNames;

namespace Instagram_Clone.Hubs
{
    public class PostHub : Hub
    {
        private readonly IPostRepository postRepository;
        private readonly ILikeRepository likeRepository;

        public PostHub(IPostRepository postRepository , ILikeRepository likeRepository)
        {
            this.postRepository = postRepository;
            this.likeRepository = likeRepository;
        }

        public async Task AddLike(int postID , string CurrentUserId)
        {
            Post? post = postRepository.GetPostByIDWithLikes(postID);

            for(int i = 0; i < post.Likes.Count; i++)
            {
                if (post.Likes[i].UserId == CurrentUserId)
                    return;
            }

            Like like = new Like();
            like.Post = post;
            like.UserId = CurrentUserId;
            like.PostId = postID;
            like.IsDeleted = false;

            post.Likes.Add(like);
            postRepository.Update(post);
            postRepository.Save();

            await Clients.All.SendAsync("ReceiveLikes", post.Id , post.Likes.Count());

        }

        public async Task DeleteLike(int postID, string CurrentUserId)
        {
            Like? like = likeRepository.GetByUserIdAndPostId(CurrentUserId , postID);
            like.IsDeleted = true;
            likeRepository.Update(like);
            likeRepository.Save();
            Post? post = postRepository.GetPostByIDWithLikes(postID);
            post.Likes.Remove(like);
            postRepository.Update(post);
            postRepository.Save();

            //post.Likes.Remove(like);
            await Clients.All.SendAsync("ReceiveLikes", postID, post.Likes.Count());
        }


    }
}
