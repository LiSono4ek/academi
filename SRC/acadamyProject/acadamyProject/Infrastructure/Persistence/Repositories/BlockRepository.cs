using Microsoft.EntityFrameworkCore;
using MiniBlockchain.Domain.Entities;
using MiniBlockchain.Infrastructure.Interfaces;
using MiniBlockchain.Infrastructure.Persistence.Contexts;

namespace MiniBlockchain.Infrastructure.Persistence.Repositories;

public class BlockRepository : GenericRepository<Block>, IBlockRepository
{
    public BlockRepository(ApplicationDbContext context) : base(context) { }

    public async Task<Block?> GetLastBlockAsync(CancellationToken ct)
    {
        return await _dbSet.OrderByDescending(b => b.CreatedAt).FirstOrDefaultAsync(ct);
    }
}