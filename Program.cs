var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
});

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.UseAuthorization();
app.MapRazorPages();
app.Run();
