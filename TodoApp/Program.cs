using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TodoApp.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// для бд
 builder.Services.AddDbContext<AppIdentityDbContext>(options => 
     options.UseSqlite(builder.Configuration.GetConnectionString("MyDatabase")));

// builder.Services.AddSingleton(TimeProvider.System);

// сервис для CoreIdentity
builder.Services.AddIdentity<IdentityUser, IdentityRole>().
    AddEntityFrameworkStores<AppIdentityDbContext>().
    AddDefaultTokenProviders();   // для генерации токенов

// добавление ролей
async Task SeedRolesAndAdminUser(IServiceProvider serviceProvider)
{
    var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

    if(!await roleManager.RoleExistsAsync("admin"))
    {
        await roleManager.CreateAsync(new IdentityRole("admin"));
    }

    if(!await roleManager.RoleExistsAsync("customer"))
    {
        await roleManager.CreateAsync(new IdentityRole("customer"));
    }

    // создание админ юзера
    var adminEmail = "admin@todo.net";
    var adminPassword = "Admin@123";
    var adminUser = await userManager.FindByEmailAsync(adminEmail);

    if (adminUser == null)
    {
        var user = new IdentityUser() {UserName = adminEmail, Email = adminEmail};
        var result = await userManager.CreateAsync(user, adminPassword);

        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(user, "admin");
        }
    }
}

var app = builder.Build();

using(var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await SeedRolesAndAdminUser(services);
}

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
    pattern: "{controller=Account}/{action=Index}/{id?}");

app.Run();
