using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project1640.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1640.Data.Configuration
{
    public class IdeaConfiguration : IEntityTypeConfiguration<Idea>
    {
        public void Configure(EntityTypeBuilder<Idea> builder)
        {
            builder.ToTable("Ideas");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.HasOne(x => x.Category).WithMany(x => x.Ideas).HasForeignKey(x => x.CategoryId);
            builder.HasOne(x => x.Submission).WithMany(x => x.Ideas).HasForeignKey(x => x.SubmissionId);
            builder.HasOne(x => x.User).WithMany(x => x.Ideas).HasForeignKey(x => x.UserId);
        }
    }
}
