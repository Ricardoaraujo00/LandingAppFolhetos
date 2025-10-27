using LandingAppFolhetos.Client.Pages;
using LandingAppFolhetos.Components;
using LandingAppFolhetos.Components.Account;
using LandingAppFolhetos.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveWebAssemblyComponents()
    .AddAuthenticationStateSerialization();

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityUserAccessor>();
builder.Services.AddScoped<IdentityRedirectManager>();

builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = IdentityConstants.ApplicationScheme;
        options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
    })
    .AddGoogle(googleOptions =>
    {
        googleOptions.ClientId = builder.Configuration["Authentication:Google:ClientId"] ?? throw new InvalidOperationException("Configuração 'Authentication:Google:ClientId' não encontrada.");
        googleOptions.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"] ?? throw new InvalidOperationException("Configuração 'Authentication:Google:ClientSecret' não encontrada.");
        googleOptions.CallbackPath = "/signin-google";
    })
    .AddIdentityCookies();
builder.Services.AddAuthorization();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));
builder.Services.AddDbContext<DatabaseDbContext>(options =>
    options.UseSqlite());
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(LandingAppFolhetos.Client._Imports).Assembly);

// Add additional endpoints required by the Identity /Account Razor components.
app.MapAdditionalIdentityEndpoints();



//using (var scope = app.Services.CreateScope())
//{
//    var origemDb = scope.ServiceProvider.GetRequiredService<DatabaseDbContext>();
//    var destinoDb = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

//    // Exemplo: importar todas as zonas administrativas
//    var niveis = origemDb.Nivel.ToList();

//    // Só insere se não existirem ainda (ajuste conforme sua lógica)
//    if (!destinoDb.Niveis.Any())
//    {
//        destinoDb.Niveis.AddRange(niveis);
//        destinoDb.SaveChanges();
//    }

//    // Exemplo: importar todas as zonas administrativas
//    var locais = origemDb.Local.ToList();

//    // Só insere se não existirem ainda (ajuste conforme sua lógica)
//    if (!destinoDb.Locais.Any())
//    {
//        destinoDb.Locais.AddRange(locais);
//        destinoDb.SaveChanges();
//    }


//}

app.Run();
