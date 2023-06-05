using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using uwierzytelnianieClaimsIdentity.Data;
using uwierzytelnianieClaimsIdentity.Models;
using uwierzytelnianieClaimsIdentity.Services;
using Microsoft.AspNetCore.Http;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<SearchInterface, SearchService>();
var connectionString = builder.Configuration.GetConnectionString("loggedUsersConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
	options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
	.AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddRazorPages();
builder.Services.AddHttpContextAccessor();
builder.Services.AddDbContext<SearchContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("SearchDbConnection")));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseMigrationsEndPoint();
}
else
{
	app.UseExceptionHandler("/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
