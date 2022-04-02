using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Project1640.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project1640.Data.EF.Seeds;

namespace Project1640.Data.EF
{
    public class Project1640DbContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public Project1640DbContext(DbContextOptions<Project1640DbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>(entity =>
            {
                entity.ToTable(name: "Users");
            });

            builder.Entity<IdentityRole<int>>(entity =>
            {
                entity.ToTable(name: "Roles");
            });

            builder.Entity<IdentityUserRole<int>>(entity =>
            {
                entity.ToTable("UserRoles");
            });

            builder.Entity<IdentityUserClaim<int>>(entity =>
            {
                entity.ToTable("UserClaims");
            });

            builder.Entity<IdentityUserLogin<int>>(entity =>
            {
                entity.ToTable("UserLogins");
            });

            builder.Entity<IdentityRoleClaim<int>>(entity =>
            {
                entity.ToTable("RoleClaims");
            });

            builder.Entity<IdentityUserToken<int>>(entity =>
            {
                entity.ToTable("UserTokens");
            });

            builder.Entity<Idea>(entity =>
            {
                entity.ToTable("Ideas");

                entity.Property(c => c.Title).IsRequired(true);
                entity.HasKey(a => a.IdeaId);
                entity.Property(a => a.IdeaId).ValueGeneratedOnAdd();
                entity.HasOne<Submission>(am => am.Submission).WithMany(u => u.Ideas).HasForeignKey(am => am.SubmissionId).OnDelete(DeleteBehavior.NoAction);
                entity.HasOne<Category>(am => am.Category).WithMany(u => u.Ideas).HasForeignKey(am => am.CategoryId).OnDelete(DeleteBehavior.NoAction);
                entity.HasOne<User>(am => am.User).WithMany(u => u.Ideas).HasForeignKey(am => am.UserId).OnDelete(DeleteBehavior.NoAction);
                entity.HasMany<File>(am => am.Files).WithOne(u => u.Idea).HasForeignKey(am => am.IdeaId);
                entity.HasMany<Cmt>(am => am.Cmts).WithOne(u => u.Idea).HasForeignKey(am => am.IdeaId);
                entity.HasMany<View>(am => am.Views).WithOne(u => u.Idea).HasForeignKey(am => am.IdeaId);
                entity.HasMany<Reaction>(am => am.Reactions).WithOne(u => u.Idea).HasForeignKey(am => am.IdeaId);
            });

            builder.Entity<File>(entity =>
            {
                entity.ToTable("Files");
                entity.Property(c => c.FilePath).IsRequired(true);
                entity.HasKey(a => a.IdeaId);
                entity.Property(a => a.IdeaId).ValueGeneratedOnAdd();
                entity.HasOne<Idea>(am => am.Idea).WithMany(u => u.Files).HasForeignKey(am => am.IdeaId).OnDelete(DeleteBehavior.NoAction);
            });

            builder.Entity<Category>(entity =>
            {
                entity.ToTable("Categories");
                entity.Property(c => c.Name).IsRequired(true);
                entity.HasKey(a => a.CategoryId);
                entity.Property(a => a.CategoryId).ValueGeneratedOnAdd();
                entity.HasMany<Idea>(am => am.Ideas).WithOne(u => u.Category).HasForeignKey(am => am.CategoryId).OnDelete(DeleteBehavior.NoAction);
            });

            builder.Entity<Submission>(entity =>
            {
                entity.ToTable("Submissions");
                entity.Property(c => c.Name).IsRequired(true);
                entity.HasKey(a => a.SubmissionId);
                entity.Property(a => a.SubmissionId).ValueGeneratedOnAdd();
                entity.HasMany<Idea>(am => am.Ideas).WithOne(u => u.Submission).HasForeignKey(am => am.SubmissionId).OnDelete(DeleteBehavior.NoAction);
            });

            builder.Entity<View>(entity =>
            {
                entity.ToTable("Views");
                entity.HasKey(a => a.ViewId);
                entity.Property(a => a.ViewId).ValueGeneratedOnAdd();
                entity.HasOne<User>(am => am.User).WithMany(u => u.Views).HasForeignKey(am => am.UserId).OnDelete(DeleteBehavior.NoAction);
                entity.HasOne<Idea>(am => am.Idea).WithMany(u => u.Views).HasForeignKey(am => am.IdeaId).OnDelete(DeleteBehavior.NoAction);
            });

            builder.Entity<Cmt>(entity =>
            {
                entity.ToTable("Cmts");
                entity.HasKey(a => a.CmtId);
                entity.Property(a => a.CmtId).ValueGeneratedOnAdd();
                entity.HasOne<User>(am => am.User).WithMany(u => u.Cmts).HasForeignKey(am => am.UserId).OnDelete(DeleteBehavior.NoAction);
                entity.HasOne<Idea>(am => am.Idea).WithMany(u => u.Cmts).HasForeignKey(am => am.IdeaId).OnDelete(DeleteBehavior.NoAction);
            });

            builder.Entity<Reaction>(entity =>
            {
                entity.ToTable("Reactions");
                entity.HasKey(a => a.ReactionId);
                entity.Property(a => a.ReactionId).ValueGeneratedOnAdd();
                entity.HasOne<User>(am => am.User).WithMany(u => u.Reactions).HasForeignKey(am => am.UserId).OnDelete(DeleteBehavior.NoAction);
                entity.HasOne<Idea>(am => am.Idea).WithMany(u => u.Reactions).HasForeignKey(am => am.IdeaId).OnDelete(DeleteBehavior.NoAction);
            });

            builder.SeedAsync();
        }

        public DbSet<Idea> Ideas { get; set; }
        public DbSet<File> Files { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Submission> Submissions { get; set; }
        public DbSet<View> Views { get; set; }
        public DbSet<Cmt> Cmts { get; set; }
        public DbSet<Reaction> Reactions { get; set; }
    }
}
