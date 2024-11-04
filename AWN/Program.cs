using System.Collections.Generic;
using System.Threading.Tasks;
using AWN;
using Microsoft.Extensions.DependencyInjection;

var serviceCollection = new ServiceCollection();
serviceCollection.AddServices();
var serviceProvider = serviceCollection.BuildServiceProvider();

var commandInvoker = serviceProvider.GetRequiredService<CommandInvoker>();

while (true)
{
    Console.Write("Enter command: ");
    var input = Console.ReadLine();
    var result = await commandInvoker.ExecuteCommandAsync(input);
    Console.WriteLine(result);
}
