using System.Security.Cryptography;
using System.Text;
using acadamyProject.Blocks.Commands;
using acadamyProject.Interfaces;
using acadamyProject.Entities;

namespace acadamyProject.Blocks.Handlers;

public class CreateBlockHandler : IRequestHandler<CreateBlockCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateBlockHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> HandleAsync(CreateBlockCommand request, CancellationToken ct)
    {
        var lastBlock = await _unitOfWork.Blocks.GetLastBlockAsync(ct);
        string previousHash = lastBlock?.Hash ?? new string('0', 64);

        var newBlock = new Block
        {
            Id = Guid.NewGuid(),
            Data = request.Data,
            PreviousHash = previousHash,
            CreatedAt = DateTime.UtcNow
        };

        newBlock.Hash = CalculateHash($"{newBlock.Data}{newBlock.PreviousHash}{newBlock.CreatedAt.Ticks}");

        await _unitOfWork.Blocks.AddAsync(newBlock, ct);
        await _unitOfWork.SaveChangesAsync(ct);

        return newBlock.Id;
    }

    private static string CalculateHash(string input)
    {
        var bytes = Encoding.UTF8.GetBytes(input);
        var hash = SHA256.HashData(bytes);
        return Convert.ToHexString(hash);
    }
}