using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using TicketMaster.Data;
using Microsoft.EntityFrameworkCore;
using TicketMaster.Objects;
using Microsoft.AspNetCore.Identity;
using TicketMaster.Data.Services.Interfaces;
using TicketMaster.Data.Services.Implementations;
using TicketMaster.Authentication;
using Microsoft.AspNetCore.Components.Authorization;

namespace TicketMaster;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddRazorPages();
        builder.Services.AddServerSideBlazor();
        builder.Services.AddScoped<ITicketMasterService, TicketMasterService>();
        builder.Services.AddScoped<IMovieService, MovieService>();
        builder.Services.AddScoped<ILoginService, LoginService>();
        builder.Services.AddControllers();

        builder.Services.AddScoped<PasswordService>();
        builder.Services.AddScoped<AuthenticationService>();
        builder.Services.AddScoped<AuthenticationStateProvider, TicketmasterAuthenticationStateProvider>();
        builder.Services.AddAuthenticationCore();

        //init mysql server context
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

        builder.Services.AddDbContext<TicketmasterContext>(
            o => o.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

        /*builder.Services.AddDefaultIdentity<UnregisteredUser>(
            o => o.SignIn.RequireConfirmedAccount = false).AddEntityFrameworkStores<TicketmasterContext>();*/

        // ALL PASSWORDS IN DATABASE ARE NOW HASHED!
        // HashAllPasswordsInDatabase(connectionString);

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

        app.MapControllers();

        app.MapBlazorHub();
        app.MapFallbackToPage("/_Host");

        app.Run();
    }

    /// <summary>
    /// In case passwords need to be rehashed in the database
    /// </summary>
    /// <param name="connectionString"></param>
    static void HashAllPasswordsInDatabase(string? connectionString)
    {
        TicketmasterContext context = new(new DbContextOptionsBuilder<TicketmasterContext>().UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)).Options);
        PasswordService pws = new();

        foreach (var user in context.Users)
        {
            try
            {
                if (!pws.VerifyPassword(user, user.PasswordHash))
                {
                    var oldpwh = user.PasswordHash;
                    user.PasswordHash = pws.HashPassword(user, user.PasswordHash);
                    Console.WriteLine(oldpwh + "\n" + user.PasswordHash);
                }
            }
            catch (FormatException)
            {
                var oldpwh = user.PasswordHash;
                user.PasswordHash = pws.HashPassword(user, user.PasswordHash);
                Console.WriteLine(oldpwh + "\n" + user.PasswordHash);
            }
        }

        context.SaveChanges();
    }
}
