using MiniBlockchain.Domain.Entities;

namespace MiniBlockchain.Infrastructure.Interfaces;

public interface IBlockRepository : IGenericRepository<Block>
{
    Task<Block?> GetLastBlockAsync(CancellationToken ct);
}