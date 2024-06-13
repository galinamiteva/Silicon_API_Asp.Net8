using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Silicon_ASP_NET_API.Configurations;

public static class DbContextConfiguration
{
    public static void RegisterDbContexts(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<DataContext>(x=>x.UseSqlServer(configuration.GetConnectionString("SqlServer")));
    }   
}
