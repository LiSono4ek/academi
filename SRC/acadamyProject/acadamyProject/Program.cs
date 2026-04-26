using Microsoft.EntityFrameworkCore;
using acadamyProject.Persistence.Contexts;
using acadamyProject.Persistence.Repositories;
using acadamyProject.Interfaces;
using acadamyProject.Blocks.Commands;
using acadamyProject.Blocks.Handlers;
using FluentValidation;
using FluentValidation.AspNetCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();
builder.Host.UseSerilog();

builder.Services.AddDbContext<ApplicationDbContext>(opt => opt.UseInMemoryDatabase("Db"));

builder.Services.AddScoped<IBlockRepository, BlockRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IRequestHandler<CreateBlockCommand, Guid>, CreateBlockHandler>();

builder.Services.AddControllers();
builder.Services.AddFluentValidationAutoValidation().AddValidatorsFromAssemblyContaining<CreateBlockHandler>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseMiddleware<acadamyProject.Middleware.ExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment()) { app.UseSwagger(); app.UseSwaggerUI(); }

app.MapControllers();
app.Run();