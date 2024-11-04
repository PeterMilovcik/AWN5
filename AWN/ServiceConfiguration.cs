using Microsoft.Extensions.DependencyInjection;

namespace AWN
{
    public static class ServiceConfiguration
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddSingleton<CommandInvoker>();
            services.AddSingleton<ICommand, ExitCommand>();
        }
    }
}
