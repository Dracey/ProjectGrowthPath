using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;
using ProjectGrowthPath.Application.Interfaces;
using ProjectGrowthPath.Application.Interfaces.IRepository;
using ProjectGrowthPath.Application.Interfaces.IServices;
using ProjectGrowthPath.Application.Service;
using ProjectGrowthPath.Application.State;
using ProjectGrowthPath.Infrastructure.API;
using ProjectGrowthPath.Infrastructure.Identity;
using ProjectGrowthPath.Infrastructure.Persistence;
using ProjectGrowthPath.Infrastructure.Services;
using ProjectGrowthPath.UserInterface.Components;
using ProjectGrowthPath.UserInterface.Components.Account;

namespace ProjectGrowthPath.UserInterface;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);



        // Voeg console logging toe
        builder.Logging.ClearProviders();
        builder.Logging.AddConsole();

        // Optioneel: Log alles van niveau 'Debug' en hoger
        builder.Logging.SetMinimumLevel(LogLevel.Debug);

        // Add services to the container.
        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents();

        builder.Services.AddCascadingAuthenticationState();
        builder.Services.AddScoped<IdentityUserAccessor>();
        builder.Services.AddScoped<IdentityRedirectManager>();
        builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();
        builder.Services.AddScoped<ProtectedLocalStorage>();


        // Voor sessies
        builder.Services.AddDistributedMemoryCache();
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromMinutes(30);
            options.Cookie.HttpOnly = true;
            options.Cookie.IsEssential = true;
        });

        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("Default") ?? throw new InvalidOperationException("Connection string 'Default' not found."
            )));


        // Identity Service
        builder.Services.AddDbContext<ApplicationIdentityDbContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("Identity") ?? throw new InvalidOperationException("Connection string 'Identity' not found."
            )));

        builder.Services.AddDatabaseDeveloperPageExceptionFilter();

        builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddEntityFrameworkStores<ApplicationIdentityDbContext>()
            .AddSignInManager()
            .AddDefaultTokenProviders();

        builder.Services.ConfigureApplicationCookie(options =>
        {
            options.LoginPath = "/Landing";
        });

        builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();

        // Application Services 
        builder.Services.AddScoped<IUserProfileService, UserProfileService>();
        builder.Services.AddScoped<ICompetenceRepository, CompetenceRepository>();
        builder.Services.AddScoped<ISetupStatePersistence, SetupStatePersistenceJsInterop>();
        builder.Services.AddScoped<ILearningToolsRepository, LearningToolsRepository>();
        builder.Services.AddScoped<ILearningtoolCompetenceRepository, LearningToolCompetenceRepository>();
        builder.Services.AddScoped<IAvatarGenerator, DiceBearAvatarGenerator>();
        builder.Services.AddScoped<ICompetenceSelectionService, CompetenceSelectionService>();
        builder.Services.AddScoped<IAvatarService, AvatarService>();
        builder.Services.AddScoped<IFirstTimeSetupService, FirstTimeSetupService>();
        builder.Services.AddScoped<ILearningToolSetupHelper, LearningToolSetupHelper>();
        builder.Services.AddScoped<IUserCompetenceRepository, UserCompetenceRepository>();
        builder.Services.AddScoped<IGoalRepository, GoalRepository>();
        builder.Services.AddScoped<IGoalLearningToolRepository, GoalLearningToolRepository>();
        builder.Services.AddScoped<IUserSessionService, UserSessionService>();
        builder.Services.AddScoped<SetupStateStore>();
        builder.Services.AddScoped<LearningToolService>();
        builder.Services.AddScoped<CompetenceService>();
        builder.Services.AddScoped<LearningToolCompetenceService>();
        builder.Services.AddScoped<SetupNewUserService>();

        // Application Repositories

        builder.Services.AddHttpClient();
        builder.Services.AddMudServices();


        var app = builder.Build();

        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            // Seed roles and admin user
            SeedRoles(roleManager).Wait();
            SeedAdmin(userManager).Wait();
        }

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

        app.UseSession();

        app.UseHttpsRedirection();

        app.UseAntiforgery();

        app.MapStaticAssets();
        app.UseStaticFiles();
        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode();

        app.MapAdditionalIdentityEndpoints();


        // Voor docker moet het app.Run(http://0.0.0.0:80). Oplossing nog aan het zoeken.
        app.Run();
    }


    // Eerste seed data voor development. Weg laten in productie.
    private static async Task SeedRoles(RoleManager<IdentityRole> roleManager)
    {
        var roles = new string[] { "Beheerder", "Medewerker" };
        foreach (var role in roles)
        {
            var roleExist = await roleManager.RoleExistsAsync(role);
            if (!roleExist)
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }
    }

    // Eerste seed data voor development. Weg laten in productie.
    private static async Task SeedAdmin(UserManager<ApplicationUser> userManager)
    {
        var adminEmail = "admin@voorbeeld.nl";
        var adminUser = await userManager.FindByEmailAsync(adminEmail);

        if (adminUser == null)
        {
            var user = new ApplicationUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                EmailConfirmed = true,

            };
            var result = await userManager.CreateAsync(user, "AdminPassword123!");
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, "Beheerder");
            }
        }
    }
}

