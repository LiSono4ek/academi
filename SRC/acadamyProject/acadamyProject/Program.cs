using Microsoft.EntityFrameworkCore;
using acadamyProject.Persistence.Contexts;
using acadamyProject.Persistence.Repositories;
using acadamyProject.Interfaces;
using acadamyProject.Blocks.Commands;
using acadamyProject.Blocks.Handlers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseInMemoryDatabase("BlockchainDb"));

builder.Services.AddScoped<IBlockRepository, BlockRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IRequestHandler<CreateBlockCommand, Guid>, CreateBlockHandler>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.Run();