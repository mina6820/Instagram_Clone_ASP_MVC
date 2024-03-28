﻿using Instagram_Clone.Authentication;
using Instagram_Clone.Models.photo;
using System.ComponentModel.DataAnnotations.Schema;

namespace Instagram_Clone.Models
{
    public class Post
    {

        public Post()
        {
            DateTime Date = DateTime.Now;
        }
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string? Caption { get; set; }
        public bool IsDeleted { get; set; } = false;


        public List<postPhoto> Photos { get; set; }

        public List<Like>? Likes { get; set; }

        public List<Comment>? Comments { get; set; }

        //messi add this
        [ForeignKey("User")]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }


    }
}
