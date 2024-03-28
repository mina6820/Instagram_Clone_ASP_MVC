using Instagram_Clone.Authentication;
using Instagram_Clone.Models.photo;
using Microsoft.VisualBasic;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Instagram_Clone.Models
{
    public class Story
    {

        public Story()
        {
          Date = DateTime.Now;
          Duration = TimeSpan.FromSeconds(30);
          LifeTime  = TimeSpan.FromHours(24);
          IsDeleted = false;
        }

        public int Id { get; set; }

        public DateTime Date { get; set; }
        public TimeSpan Duration { get; set; }
        public TimeSpan LifeTime { get; set; }
        public bool IsDeleted { get; set; }




        [ForeignKey("User")]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        //habeba
        public List<StoryView>? StoryViews { get; set; }


        [ForeignKey("Photo")]
        public int PhotoId { get; set; }
        public storyPhoto Photo { get; set; }


    }
}
