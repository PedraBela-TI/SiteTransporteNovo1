using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SiteTransporteNovo.Data;

var builder = WebApplication.CreateBuilder(args);

// Adiciona controllers com views
builder.Services.AddControllersWithViews();

// Configura conexão com MySQL (via Pomelo)
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// Adiciona Identity com opções de senha simples
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 4;
})
.AddEntityFrameworkStores<AppDbContext>()
.AddDefaultTokenProviders();


// Configura sessão
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Criação automática dos usuários
using (var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

    var users = new List<(string Username, string Password)>
    {
        ("Renato","1234"),
        ("Monica", "1234"),
        ("Marilia","1234"),
        ("Grazi", "1234"),
        ("Marcia", "1234")
    };

    foreach (var (username, password) in users)
    {
        var existingUser = await userManager.FindByNameAsync(username);
        if (existingUser == null)
        {
            var user = new IdentityUser
            {
                UserName = username,
                Email = $"{username.ToLower()}@fake.com",
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(user, password);
            if (!result.Succeeded)
            {
                Console.WriteLine($"Erro ao criar {username}: {string.Join(", ", result.Errors.Select(e => e.Description))}");
            }
        }
    }
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();
app.UseAuthentication(); // necessário para Identity
app.UseAuthorization();

app.MapControllerRoute(
    name: "logout",
    pattern: "Usuario/Logout",
    defaults: new { controller = "Usuario", action = "Logout" });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Usuario}/{action=Login}/{id?}");

await app.RunAsync();
