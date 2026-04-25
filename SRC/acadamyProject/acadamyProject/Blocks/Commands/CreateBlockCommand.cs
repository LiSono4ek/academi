using acadamyProject.Interfaces;

namespace acadamyProject.Blocks.Commands;

public record CreateBlockCommand(string Data) : IRequest<Guid>;