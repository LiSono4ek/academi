using Microsoft.AspNetCore.Mvc;
using acadamyProject.Blocks.Commands;
using acadamyProject.Interfaces;

namespace acadamyProject.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BlocksController : ControllerBase
{
    private readonly IRequestHandler<CreateBlockCommand, Guid> _handler;

    public BlocksController(IRequestHandler<CreateBlockCommand, Guid> handler)
    {
        _handler = handler;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateBlockCommand command, CancellationToken ct)
    {
        var result = await _handler.HandleAsync(command, ct);
        return Ok(result);
    }
}