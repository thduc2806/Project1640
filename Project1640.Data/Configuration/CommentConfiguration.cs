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
    class CommentConfiguration : IEntityTypeConfiguration<Cmt>
    {
        public void Configure(EntityTypeBuilder<Cmt> builder)
        {
            builder.ToTable("Cmts");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.HasOne(x => x.User).WithMany(x => x.Cmts).HasForeignKey(x => x.UserId);
            builder.HasOne(x => x.Idea).WithMany(x => x.Cmts).HasForeignKey(x => x.IdeaId);
        }
    }
}
