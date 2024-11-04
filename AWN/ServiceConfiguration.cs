using Microsoft.Extensions.DependencyInjection;
using Microsoft.Playwright;

namespace AWN
{
    public static class ServiceConfiguration
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddSingleton<CommandInvoker>();
            services.AddSingleton<ICommand, ExitCommand>();
            services.AddSingleton<ICommand, NavigateCommand>(); // Register NavigateCommand

            services.AddSingleton(serviceProvider =>
            {
                var playwright = Playwright.CreateAsync().GetAwaiter().GetResult();
                var browser = playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false }).GetAwaiter().GetResult();
                var page = browser.NewPageAsync().GetAwaiter().GetResult();
                return page;
            });
        }
    }
}
