using acadamyProject.Entities;
namespace acadamyProject.Interfaces;

public interface IBlockRepository
{
    Task<Block?> GetByIdAsync(Guid id, CancellationToken ct);
    Task AddAsync(Block entity, CancellationToken ct);
    Task<Block?> GetLastBlockAsync(CancellationToken ct);
    Task<IEnumerable<Block>> GetAllAsync(CancellationToken ct); 
}