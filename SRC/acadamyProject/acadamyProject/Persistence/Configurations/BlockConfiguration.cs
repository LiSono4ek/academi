using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using acadamyProject.Entities;

namespace acadamyProject.Persistence.Configurations;

public class BlockConfiguration : IEntityTypeConfiguration<Block>
{
    public void Configure(EntityTypeBuilder<Block> builder)
    {
        builder.HasKey(b => b.Id);
        builder.Property(b => b.Data).IsRequired();
        builder.Property(b => b.Hash).HasMaxLength(64).IsRequired();
        builder.Property(b => b.PreviousHash).HasMaxLength(64).IsRequired();
        builder.HasIndex(b => b.CreatedAt);
    }
}