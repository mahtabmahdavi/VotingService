using Microsoft.EntityFrameworkCore;
using VotingService.Application.Intefaces.Repositories;
using VotingService.Grpc.Services;
using VotingService.Infrastructure.Data;
using VotingService.Infrastructure.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<VotingDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddGrpc();

// Register Repositories
builder.Services.AddScoped<IPollRepository, PollRepository>();

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenLocalhost(5163, listenOptions =>
    {
        listenOptions.UseHttps();
        listenOptions.Protocols = Microsoft.AspNetCore.Server.Kestrel.Core.HttpProtocols.Http2;
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<VoteGrpcService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
