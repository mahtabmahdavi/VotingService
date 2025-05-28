using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using VotingService.Protos;

var builder = WebApplication.CreateBuilder(args);

// --------------------------------------------------
// Services
// --------------------------------------------------
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

// gRPC Client Configuration
builder.Services.AddGrpcClient<VoteService.VoteServiceClient>(options =>
{
    options.Address = new Uri("https://localhost:5163");
});

var app = builder.Build();

// --------------------------------------------------
// Middleware
// --------------------------------------------------
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// --------------------------------------------------
// Endpoints
// --------------------------------------------------
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
