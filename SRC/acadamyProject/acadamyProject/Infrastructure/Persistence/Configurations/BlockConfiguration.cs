using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniBlockchain.Domain.Entities;

namespace MiniBlockchain.Infrastructure.Persistence.Configurations;

public class BlockConfiguration : IEntityTypeConfiguration<Block>
{
    public void Configure(EntityTypeBuilder<Block> builder)
    {
        builder.ToTable("Blocks");

        builder.HasKey(b => b.Id);

        builder.Property(b => b.Data)
            .IsRequired();

        builder.Property(b => b.Hash)
            .HasMaxLength(64)
            .IsRequired();

        builder.Property(b => b.PreviousHash)
            .HasMaxLength(64)
            .IsRequired();

        builder.Property(b => b.CreatedAt)
            .IsRequired();

        // Индекс для ускорения валидации цепочки по времени
        builder.HasIndex(b => b.CreatedAt);
    }
}