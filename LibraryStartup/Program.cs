using LibraryManagement.Models.Contexts;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

string connectionString = "Data Source=localhost; Host=localhost; Database=postgres; Initial Dialog=postgres; UserName=postgres; Password=147852";

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));



var app = builder.Build();


var options = new DbContextOptionsBuilder<ApplicationDbContext>().
                    UseNpgsql(connectionString).
                    LogTo(Console.WriteLine, LogLevel.Information).
                    Options;


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
