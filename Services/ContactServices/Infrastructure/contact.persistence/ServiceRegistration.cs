using contact.persistence.Context;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using contact.application.Repositories;
using contact.persistence.Repositories;

namespace contact.persistence
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
