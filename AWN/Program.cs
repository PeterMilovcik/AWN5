using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AWN;
using Microsoft.Extensions.DependencyInjection;

var serviceCollection = new ServiceCollection();
serviceCollection.AddServices();
var serviceProvider = serviceCollection.BuildServiceProvider();

var commandInvoker = serviceProvider.GetRequiredService<CommandInvoker>();
var textToSpeechService = serviceProvider.GetRequiredService<TextToSpeechService>();
var speechToTextService = serviceProvider.GetRequiredService<SpeechToTextService>();

while (true)
{
    Console.Write("Choose input method (1: Text, 2: Voice): ");
    var inputMethod = Console.ReadLine();

    if (inputMethod == "1")
    {
        Console.Write("Enter command: ");
        var input = Console.ReadLine() ?? string.Empty;
        var result = await commandInvoker.ExecuteCommandAsync(input);
        Console.WriteLine(result);

        await textToSpeechService.ConvertTextToSpeechAsync(result);
    }
    else if (inputMethod == "2")
    {
        Console.WriteLine("Press Enter to start recording...");
        Console.ReadLine();

        var cts = new CancellationTokenSource();
        var recordingTask = speechToTextService.StartRecordingAsync("input.mp3", cts.Token);

        Console.WriteLine("Recording... Press Enter to stop.");
        Console.ReadLine();
        cts.Cancel();

        await recordingTask;

        var transcribedText = await speechToTextService.TranscribeAudioAsync("input.mp3");
        Console.WriteLine($"Transcribed Text: {transcribedText}");

        var result = await commandInvoker.ExecuteCommandAsync(transcribedText);
        Console.WriteLine(result);

        await textToSpeechService.ConvertTextToSpeechAsync(result);
    }
    else
    {
        Console.WriteLine("Invalid input method. Please choose 1 or 2.");
    }
}
