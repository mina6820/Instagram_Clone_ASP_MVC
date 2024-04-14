namespace Instagram_Clone.Repositories.NotificationRepo.FollowRequestContainer
{
    public interface IFollowRequestRepository :INotificationRepository<FollowRequest>
    {
        public void FollowBack(string followeeId, string loginUser);
        public void Follow(string followeeId, string loginUser);
    }
}
