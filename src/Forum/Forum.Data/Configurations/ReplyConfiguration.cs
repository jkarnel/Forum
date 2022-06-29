using Forum.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Forum.Data.Configurations
{
    public class ReplyConfiguration : IEntityTypeConfiguration<Reply>
    {
        public void Configure(EntityTypeBuilder<Reply> reply)
        {
            reply
                .Property(r => r.Description)
                .HasMaxLength(32768)
                .IsRequired();

            reply
                .HasOne(r => r.Post)
                .WithMany(p => p.Replies)
                .HasForeignKey(r => r.PostId)
                .OnDelete(DeleteBehavior.Restrict);

            reply
                .HasOne(r => r.Author)
                .WithMany(a => a.Replies)
                .HasForeignKey(r => r.AuthorId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
