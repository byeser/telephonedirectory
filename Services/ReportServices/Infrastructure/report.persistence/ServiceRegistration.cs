using report.persistence.Context;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using report.application.Repositories;
using report.persistence.Repositories;

namespace report.persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("PostgreSql")));
            serviceCollection.AddTransient<IContactInfoRepository, ContactInfoRepository>(); 
            serviceCollection.AddTransient<IPersonsRepository, PersonsRepository>(); 
        }
    }
}
