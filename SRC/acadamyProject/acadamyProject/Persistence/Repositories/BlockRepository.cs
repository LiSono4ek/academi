using Microsoft.EntityFrameworkCore;
using acadamyProject.Entities;
using acadamyProject.Interfaces;
using acadamyProject.Persistence.Contexts;

namespace acadamyProject.Persistence.Repositories;

public class BlockRepository : IBlockRepository
{
    private readonly ApplicationDbContext _context;

    public BlockRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Block entity, CancellationToken ct) => await _context.Blocks.AddAsync(entity, ct);

    public async Task<Block?> GetByIdAsync(Guid id, CancellationToken ct) => await _context.Blocks.FindAsync(new object[] { id }, ct);

    public async Task<Block?> GetLastBlockAsync(CancellationToken ct) =>
        await _context.Blocks.OrderByDescending(b => b.CreatedAt).FirstOrDefaultAsync(ct);
}