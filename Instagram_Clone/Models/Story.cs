using Instagram_Clone.Authentication;
using Instagram_Clone.Models.photo;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Instagram_Clone.Models
{
    public class Story
    {
        public Story()
        {
            CreatedAt = DateTime.Now;
            LifeTimeTicks = TimeSpan.FromHours(24).Ticks;
            IsDeleted = false;
            Photo = new storyPhoto();
        }

        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }

        public long LifeTimeTicks { get; set; }
        public bool IsDeleted { get; set; } = false;

        [ForeignKey("User")]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public storyPhoto Photo { get; set; }

        [NotMapped]
        public TimeSpan LifeTime
        {
            get => TimeSpan.FromTicks(LifeTimeTicks);
            set => LifeTimeTicks = value.Ticks;
        }

        public string? AudioPath { get; set; }


        public void CheckExpiration()
        {
            TimeSpan timeSinceCreation = DateTime.Now - CreatedAt;

            if (timeSinceCreation.TotalHours >= 24)
            {
                IsDeleted = true;
            }
        }
    }
}
