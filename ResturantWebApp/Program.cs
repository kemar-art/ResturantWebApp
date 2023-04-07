using Microsoft.EntityFrameworkCore;
using ResturantWebApp.DataAccess.Data;
using ResturantWebApp.DataAccess.Repository;
using ResturantWebApp.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using ResturantWebApp.Utility;
using Stripe;
using ResturantWebApp.DataAccess.SeedUSerRoles;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
         builder.Configuration.GetConnectionString("DefaultConnection")
    ));

builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("Stripe"));

builder.Services.AddIdentity<IdentityUser, IdentityRole>(/*options => options.SignIn.RequireConfirmedAccount = true*/)
    .AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
builder.Services.AddSingleton<IEmailSender, EmailSender>();


builder.Services.AddScoped<IDbSeedUSerRoles, DbSeedUSerRoles>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Identity/Account/Login";
    options.LogoutPath = "/Identity/Account/Logout";
    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
});

//Creating session for items in cart
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(100);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddAuthentication().AddFacebook(options =>
{
    options.AppId = "1951341088540504";
    options.AppSecret = "48fb2cf74b04710b50b4d0f521d1cd48";
})
.AddTwitter(options =>
{
    options.ConsumerKey = "ESvq1jJoSzqLK5siGU31VS5PU";
    options.ConsumerSecret = "OeP49FPDksXjQA8rfyBYRvcp4fsc4HRJ0j9uHwhI2E0ZelXxmV";

})
.AddLinkedIn(options =>
{
    options.ClientId = "78c44ti1x0agit";
    options.ClientSecret = "7rK8QGf5A6qDICqc";

})
.AddGoogle(options =>
 {
     options.ClientId = "78c44ti1x0agit";
     options.ClientSecret = "7rK8QGf5A6qDICqc";

 });



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
SeedDatabase();
string? key = builder.Configuration.GetSection("Stripe:Secretkey").Get<string>();
StripeConfiguration.ApiKey = key;

app.UseAuthorization();
app.UseSession();
app.MapRazorPages();
app.MapControllers();
app.Run();

void SeedDatabase()
{
    using (var scope = app.Services.CreateScope())
    {
        var dbSeedUSerRoles = scope.ServiceProvider.GetRequiredService<IDbSeedUSerRoles>();
        dbSeedUSerRoles.Initialize();
    }
}