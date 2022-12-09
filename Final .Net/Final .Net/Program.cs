using Core.Entities;
using Core.Utilities;
using DataAcces;
using DataAcces.Contexts;
using DataAcces.Repositories.Abstract;
using DataAcces.Repositories.Concrete;
using Final_.Net.Areas.Admin.Services.Abstract;
using Final_.Net.Areas.Admin.Services.Concrete;
using Final_.Net.Services.Abstract;
using Final_.Net.Services.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using AccountService = Final_.Net.Areas.Admin.Services.Concrete.AccountService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
var connectionString = builder.Configuration.GetConnectionString("Default");
builder.Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
builder.Services.AddDbContext<AppDbContext>(x => x.UseSqlServer(connectionString, x => x.MigrationsAssembly("DataAcces")));
builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredUniqueChars = 0;
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 0;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
})
    .AddEntityFrameworkStores<AppDbContext>();

#region Repositories
builder.Services.AddScoped<IOurVisionRepository, OurVisionRepository>();
builder.Services.AddScoped<IAboutUsRepository,AboutUsRepository>();
builder.Services.AddScoped<IMedicalDepartmentsRepository, MedicalDepartmentsRepository>();
builder.Services.AddScoped<IDoctorsRepository,DoctorsRepository>();
builder.Services.AddScoped<IWhyChooseRepository, WhyChooseRepository>();
builder.Services.AddScoped<ILastestNewsRepository, LastestNewsRepository>();
builder.Services.AddScoped<IPricingRepository,PricingRepository>();
//builder.Services.AddScoped<I>
#endregion

#region Services
builder.Services.AddScoped<IOurVisionService, OurVisionService>();
builder.Services.AddScoped<IAboutUsService, AboutUsService>();
builder.Services.AddScoped<IMedicalDepartmentsService, MedicalDepartmentsService>();
builder.Services.AddSingleton<IFileService, FileService>();
builder.Services.AddScoped<IDoctorsService, DoctorsService>();
builder.Services.AddScoped<IWhyChooseService, WhyChooseService>();
builder.Services.AddScoped<ILastestNewsService,LastestNewsService>();
builder.Services.AddScoped<IPricingService, PricingService>();
builder.Services.AddScoped<IFaqService,FaqService>();
builder.Services.AddScoped<IDepartmentsService, DepartmentsService>();
builder.Services.AddScoped<Final_.Net.Services.Abstract.IAccountService, Final_.Net.Services.Concrete.AccountService>();
builder.Services.AddScoped<Final_.Net.Areas.Admin.Services.Abstract.IAccountService, AccountService>();
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseStaticFiles();

app.UseRouting();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

var scopFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
using (var scope = scopFactory.CreateScope())
{
    var userManager = scope.ServiceProvider.GetService<UserManager<User>>();
    var roleManager = scope.ServiceProvider.GetService<RoleManager<IdentityRole>>();
    await DbInitializer.SeedAsync(roleManager, userManager);
}

app.Run();
