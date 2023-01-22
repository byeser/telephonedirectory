using telephonedirectory.persistence.Context;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using telephonedirectory.application.Repositories;
using telephonedirectory.persistence.Repositories;

namespace telephonedirectory.persistence
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
