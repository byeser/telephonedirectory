using MediatR;
using Microsoft.Extensions.DependencyInjection; 
using System.Reflection; 

namespace report.application
{
    static public class ServiceRegistration
    {
        public static void AddApplicationServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddMediatR(Assembly.GetExecutingAssembly());
            serviceCollection.AddAutoMapper(Assembly.GetExecutingAssembly());
        }
    }
}
