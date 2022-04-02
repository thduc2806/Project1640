using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Project1640.Data.EF;

namespace Project1640.Data
{
    public static class ServiceRegister
    {
        public static void AddDataAccessorLayer(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<Project1640DbContext>(options =>
                options.UseSqlServer(config.GetConnectionString("DbConnection"), b =>
                    b.MigrationsAssembly(typeof(Project1640DbContext).Assembly.FullName)
                ));
        }
    }
}
