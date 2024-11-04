using System.Collections.Generic;
using System.Threading.Tasks;
using AWN;
using Microsoft.Extensions.DependencyInjection;

var serviceCollection = new ServiceCollection();
serviceCollection.AddServices();
var serviceProvider = serviceCollection.BuildServiceProvider();

var commandInvoker = serviceProvider.GetRequiredService<CommandInvoker>();
var textToSpeechService = serviceProvider.GetRequiredService<TextToSpeechService>();

while (true)
{
    Console.Write("Enter command: ");
    var input = Console.ReadLine() ?? string.Empty;
    var result = await commandInvoker.ExecuteCommandAsync(input);
    Console.WriteLine(result);

    await textToSpeechService.ConvertTextToSpeechAsync(result);
}
