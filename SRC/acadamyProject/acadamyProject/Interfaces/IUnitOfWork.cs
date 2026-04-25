namespace acadamyProject.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IBlockRepository Blocks { get; }
    Task<int> SaveChangesAsync(CancellationToken ct);
}