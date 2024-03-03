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

        public DbSet<Publication> Publications { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Friend> Friends { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            #region "Tables"
            modelBuilder.Entity<Publication>().ToTable("Publications");
            modelBuilder.Entity<Comment>().ToTable("Comments");
            modelBuilder.Entity<Friend>().ToTable("Friends");
            #endregion

            #region "PrimaryKey"
            modelBuilder.Entity<Publication>().HasKey(p => p.Id);
            modelBuilder.Entity<Comment>().HasKey(c => c.Id);
            modelBuilder.Entity<Friend>().HasKey(f => f.Id);
            #endregion

            #region "Relantioship"

            modelBuilder.Entity<Publication>()
                .HasMany<Comment>(c => c.Comments)
                .WithOne(p => p.Publication)
                .HasForeignKey(p => p.PublicationId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Comment>()
               .HasOne(c => c.Parent)
               .WithMany(c => c.Children)
               .HasForeignKey(c => c.ParentId)
               .OnDelete(DeleteBehavior.NoAction);

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

            base.OnModelCreating(modelBuilder);
        }
    }
}
