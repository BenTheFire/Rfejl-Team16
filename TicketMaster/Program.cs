using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using TicketMaster.Data;
using Microsoft.EntityFrameworkCore;
using TicketMaster.Objects;
using Microsoft.AspNetCore.Identity;
using TicketMaster.Data.Services.Interfaces;
using TicketMaster.Data.Services.Implementations;

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

        //init mysql server context
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

        builder.Services.AddDbContext<TicketmasterContext>(
            o => o.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

        /*builder.Services.AddDefaultIdentity<UnregisteredUser>(
            o => o.SignIn.RequireConfirmedAccount = false).AddEntityFrameworkStores<TicketmasterContext>();*/

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
}
