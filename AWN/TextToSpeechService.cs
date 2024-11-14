using System;
using System.IO;
using System.Threading.Tasks;
using NAudio.Wave;
using OpenAI.Audio;

namespace AWN
{
    public class TextToSpeechService
    {
        private readonly string _apiKey;
        private readonly AudioClient _audioClient;

        public TextToSpeechService()
        {
            _apiKey = Environment.GetEnvironmentVariable("OPENAI_API_KEY") ?? throw new InvalidOperationException("API key not found.");
            _audioClient = new AudioClient("tts-1", _apiKey);
        }

        public async Task ConvertTextToSpeechAsync(string text)
        {
            try
            {
                var language = Environment.GetEnvironmentVariable("DEFAULT_LANGUAGE") ?? "en";
                var options = new AudioTranscriptionOptions
                {
                    Language = language
                };

                BinaryData speech = await _audioClient.GenerateSpeechAsync(text, GeneratedSpeechVoice.Onyx, options);

                string outputFilePath = "output.mp3";
                if (File.Exists(outputFilePath))
                {
                    File.Delete(outputFilePath);
                }

                using (FileStream stream = File.OpenWrite(outputFilePath))
                {
                    speech.ToStream().CopyTo(stream);
                }

                PlayAudio(outputFilePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                Console.WriteLine("Text to speech failed.");
            }
        }

        private void PlayAudio(string filePath)
        {
            using (var audioFile = new AudioFileReader(filePath))
            using (var outputDevice = new WaveOutEvent())
            {
                outputDevice.Init(audioFile);
                outputDevice.Play();

                Console.WriteLine("Press Enter to stop playback...");
                while (outputDevice.PlaybackState == PlaybackState.Playing)
                {
                    if (Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Enter)
                    {
                        outputDevice.Stop();
                    }
                }
            }
        }
    }
}
