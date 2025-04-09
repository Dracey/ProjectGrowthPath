using ProjectGrowthPath.UI.Components;
using Microsoft.EntityFrameworkCore;
using ProjectGrowthPath.Infrastructure.Persistence;
using ProjectGrowthPath.Infrastructure.Identity;
using System;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Identity;
using ProjectGrowthPath.Application.Interfaces;
using ProjectGrowthPath.Infrastructure.Repositories;

namespace ProjectGrowthPath.UI;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents();

        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

        
        // Identity Service
        builder.Services.AddDbContext<AppIdentityDbContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

        builder.Services.AddIdentity<IdentityUser, IdentityRole>()
            .AddEntityFrameworkStores<AppIdentityDbContext>()
            .AddDefaultTokenProviders();

        builder.Services.AddScoped<IUserProfileRepository, UserProfileRepository>();
        builder.Services.AddScoped<IIdentityService, IdentityService>();



        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseAntiforgery();
        app.UseAuthentication();
        app.UseAuthorization();


        app.MapStaticAssets();
        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode();

        app.Run();
    }
}
