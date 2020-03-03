using System.Security.Claims;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace IdentityServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var user = new IdentityUser("bob");
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
                userManager.CreateAsync(user, "pass123").GetAwaiter().GetResult();
                //optional
                userManager.AddClaimAsync(user, new Claim("rc.grandma", "big.cookie")).GetAwaiter().GetResult();
                userManager.AddClaimAsync(user, new Claim("rc.apione.grandma", "big.apione.cookie")).GetAwaiter().GetResult();
            }     

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}