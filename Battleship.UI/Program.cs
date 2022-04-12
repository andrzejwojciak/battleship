using Battleship.Core;
using Battleship.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();

builder.Services.AddCoreLayer();
builder.Services.AddDataAccessLayer();

var app = builder.Build();

await SeedDatabase(app.Services);

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseCors(builder => builder 
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true)
    .AllowCredentials());

app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");

app.Run();

async Task SeedDatabase(IServiceProvider serviceProvider)
{
    using var scope = serviceProvider.CreateScope();

    var context = scope.ServiceProvider.GetRequiredService<BattleshipDbContext>();

    await context.Database.EnsureCreatedAsync();
}