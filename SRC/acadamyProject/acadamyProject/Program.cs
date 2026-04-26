using Microsoft.EntityFrameworkCore;
using acadamyProject.Persistence.Contexts;
using acadamyProject.Persistence.Repositories;
using acadamyProject.Interfaces;
using acadamyProject.Blocks.Commands;
using acadamyProject.Blocks.Handlers;
using Serilog;
using FluentValidation;
using FluentValidation.AspNetCore;
using acadamyProject.Blocks.Validators;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs/blockchain_log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseInMemoryDatabase("BlockchainDb"));

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<CreateBlockCommandValidator>();

builder.Services.AddScoped<IBlockRepository, BlockRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IRequestHandler<CreateBlockCommand, Guid>, CreateBlockHandler>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseMiddleware<acadamyProject.Middleware.ExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

try
{
    Log.Information("Приложение запускается...");
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Приложение упало при запуске");
}
finally
{
    Log.CloseAndFlush();
}