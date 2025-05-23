using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EBIN.Models;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;

namespace EBIN.Data
{
    public class EBINContext : DbContext
    {
        public EBINContext (DbContextOptions<EBINContext> options)
            : base(options)
        {
        }

        public DbSet<EBIN.Models.Posts> Posts { get; set; } = default!;
        public DbSet<EBIN.Models.Profiles> Profiles { get; set; } = default!;
        public DbSet<ProfileFollowers> ProfileFollowers { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProfileFollowers>()
                .HasKey(pk => new { pk.FollowerId, pk.FollowingId });
            
            modelBuilder.Entity<ProfileFollowers>()
                .HasOne(pf => pf.Follower)
                .WithMany(f => f.Following)
                .HasForeignKey(pf => pf.FollowerId)
                .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Entity<ProfileFollowers>()
                .HasOne(pf => pf.Following)
                .WithMany(f => f.Followers)
                .HasForeignKey(pf => pf.FollowingId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
