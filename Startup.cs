using FantasyAppData;
using FantasyAppData.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContext<FantasyAppContext>(options =>
            options.UseMySQL(Configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<AuthService>();

        services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = null; // Keep property names as is
            });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthorization();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
