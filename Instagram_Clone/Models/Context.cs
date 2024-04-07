﻿using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.General;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Instagram_Clone.Authentication;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Instagram_Clone.Models.photo;

namespace Instagram_Clone
{
    public class Context : IdentityDbContext<ApplicationUser>
    {
        public Context(DbContextOptions<Context> options) : base(options) { }

        public Context() : base() { }

        public DbSet<Comment> Comments { get; set; }
        public DbSet<UserRelationship> UserRelationship { get; set; }
        public DbSet<Like> Likes { get; set; } 
        public DbSet<Message> Messages { get; set; }

        public DbSet<messagePhoto> MessagePhoto { get; set; }
        public DbSet<postPhoto> PostPhoto { get; set; }
        public DbSet<profilePhoto> ProfilePhoto { get; set; }
        public DbSet<storyPhoto> StoryPhoto { get; set; }


        public DbSet<Post> Posts { get; set; }
        public DbSet<Story> Stories { get; set; }
        public DbSet<StoryViewer> StoryViewers { get; set; }

        public DbSet<ApplicationUser> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<Connection>()
            //.HasKey(c => new { c.followingId, c.followerId });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //base.OnConfiguring(optionsBuilder);
        }

    }


}