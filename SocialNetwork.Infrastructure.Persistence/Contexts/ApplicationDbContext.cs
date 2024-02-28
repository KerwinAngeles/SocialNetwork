using Microsoft.EntityFrameworkCore;
using SocialNetwork.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Infrastructure.Persistence.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Publication> Publications { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Reply> Replies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            #region "Tables"
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Publication>().ToTable("Publications");
            modelBuilder.Entity<Comment>().ToTable("Comments");
            modelBuilder.Entity<Reply>().ToTable("Replies");
            #endregion

            #region "PrimaryKey"
            modelBuilder.Entity<User>().HasKey(u => u.Id);
            modelBuilder.Entity<Publication>().HasKey(p => p.Id);
            modelBuilder.Entity<Comment>().HasKey(c => c.Id);
            modelBuilder.Entity<Reply>().HasKey(r => r.Id);
            #endregion

            #region "Relantioship"
            modelBuilder.Entity<User>()
              .HasMany<Publication>(p => p.Publications)
              .WithOne(u => u.User)
              .HasForeignKey(u => u.UserId)
              .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasMany<Comment>(c => c.Comments)
                .WithOne(u => u.User)
                .HasForeignKey(u => u.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasMany<Reply>(r => r.replies)
                .WithOne(u => u.User)
                .HasForeignKey(u => u.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Publication>()
                .HasMany<Comment>(c => c.Comments)
                .WithOne(p => p.Publication)
                .HasForeignKey(p => p.publicationId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Comment>()
                .HasMany<Reply>(r => r.replies)
                .WithOne(c => c.Comment)
                .HasForeignKey(c => c.commentId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion

            #region "User"
            modelBuilder.Entity<User>()
             .Property(u => u.Name)
             .IsRequired()
             .HasMaxLength(100);

            modelBuilder.Entity<User>()
              .Property(u => u.LastName)
              .IsRequired()
              .HasMaxLength(100);

            modelBuilder.Entity<User>()
              .Property(u => u.UserName)
              .IsRequired()
              .HasMaxLength(150);

            modelBuilder.Entity<User>()
              .Property(u => u.Password)
              .IsRequired();

            modelBuilder.Entity<User>()
              .Property(u => u.Email)
              .IsRequired();

            modelBuilder.Entity<User>()
              .Property(u => u.Phone)
              .IsRequired();

            #endregion

            #region "Publication"
            modelBuilder.Entity<Publication>()
              .Property(p => p.Title)
              .IsRequired();
            #endregion

            #region "Comment"

            modelBuilder.Entity<Comment>()
              .Property(c => c.Message)
              .IsRequired();

            #endregion

            #region "Reply"

            modelBuilder.Entity<Reply>()
              .Property(r => r.Message)
              .IsRequired();

            #endregion

            base.OnModelCreating(modelBuilder);
        }
    }
}
