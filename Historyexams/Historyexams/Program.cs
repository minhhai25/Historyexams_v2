using Historyexams.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var connectionStr = builder.Configuration.GetConnectionString("DBConnectString");
builder.Services.AddDbContext<HistoryexamsContext>(x => x.UseSqlServer(connectionStr));
//C?u hình s? d?ng session
builder.Services.AddDistributedMemoryCache();
//??ng ký d?ch v? cho HttpContextAccesor
builder.Services.AddHttpContextAccessor();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.Cookie.Name = ".Devmaster.Session";
});



//builder.Services.AddIdentity <AdminModel,IdentityRole > ()
//.AddEntityFrameworkStores<DbContext>().AddDefaultTokenProviders();


//builder.Services.Configure<IdentityOptions>(options =>
//{
//	//Password settings.
//	options.Password.RequireDigit = true;
//	options.Password.RequireLowercase = true;
//	options.Password.RequireNonAlphanumeric = false;
//	options.Password.RequireUppercase = false;
//	options.Password.RequiredLength = 6;
//	options.User.RequireUniqueEmail = true;

	//options.Password.RequiredUniqueChars = 1;

	// Lockout settings.
	//options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
	//options.Lockout.MaxFailedAccessAttempts = 5;
	//options.Lockout.AllowedForNewUsers = true;

	// User settings.
	//options.User.AllowedUserNameCharacters =
	//"abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";

//});





var app = builder.Build();

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
//S? d?ng session khai báo
app.UseSession();

//app.UseAuthentication();
//app.UseAuthorization();

app.MapControllerRoute(
        name: "areas",
        pattern: "{area:exists}/{controller=Chuongs}/{action=Index}/{id?}"
        );

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
