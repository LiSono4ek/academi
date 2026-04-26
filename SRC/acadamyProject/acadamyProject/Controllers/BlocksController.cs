using Microsoft.AspNetCore.Mvc;
using acadamyProject.Blocks.Commands;
using acadamyProject.Interfaces;
using acadamyProject.Entities;

namespace acadamyProject.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BlocksController : ControllerBase
{
    private readonly IRequestHandler<CreateBlockCommand, Guid> _createHandler;
    private readonly IUnitOfWork _unitOfWork;

    public BlocksController(
        IRequestHandler<CreateBlockCommand, Guid> createHandler,
        IUnitOfWork unitOfWork)
    {
        _createHandler = createHandler;
        _unitOfWork = unitOfWork;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateBlockCommand command, CancellationToken ct)
    {
        var result = await _createHandler.HandleAsync(command, ct);
        return Ok(new { Id = result, Message = "Block created and added to chain." });
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Block>>> GetAll(CancellationToken ct)
    {
        var blocks = await _unitOfWork.Blocks.GetAllAsync(ct);
        return Ok(blocks);
    }

    [HttpGet("validate")]
    public async Task<IActionResult> Validate(CancellationToken ct)
    {
        var blocks = (await _unitOfWork.Blocks.GetAllAsync(ct)).OrderBy(b => b.CreatedAt).ToList();

        for (int i = 1; i < blocks.Count; i++)
        {
            if (blocks[i].PreviousHash != blocks[i - 1].Hash)
            {
                return BadRequest(new { IsValid = false, Message = $"Chain broken at block {i}" });
            }
        }

        return Ok(new { IsValid = true, Message = "Blockchain integrity verified." });
    }
}