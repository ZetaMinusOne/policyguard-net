using ZetaMinusOne.PolicyGuard.ASPNETCore;

namespace WebAppWithPolicyGuardMiddleware
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var configuration = builder.Configuration;

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // Register PolicyGuard as Singleton
            builder.Services.AddSingleton(s =>
            {
                var apiKey = configuration.GetValue<string>("policyGuard:apiKey");
                var httpClient = new HttpClient();
                return new PolicyGuard(httpClient, apiKey);
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            // Register PolicyGuardMiddleware
            app.UsePolicyGuardMiddleware();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}