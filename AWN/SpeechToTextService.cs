using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using NAudio.Wave;
using OpenAI.Audio;

namespace AWN
{
    public class SpeechToTextService
    {
        private readonly string _apiKey;
        private readonly AudioClient _audioClient;

        public SpeechToTextService()
        {
            _apiKey = Environment.GetEnvironmentVariable("OPENAI_API_KEY") ?? throw new InvalidOperationException("API key not found.");
            _audioClient = new AudioClient("whisper-1", _apiKey);
        }

        public async Task StartRecordingAsync(string outputFilePath, CancellationToken cancellationToken)
        {
            var waveFormat = new WaveFormat(44100, 16, 1);
            using (var waveIn = new WaveInEvent { WaveFormat = waveFormat })
            using (var writer = new WaveFileWriter(outputFilePath, waveFormat))
            {
                waveIn.DataAvailable += (sender, e) =>
                {
                    writer.Write(e.Buffer, 0, e.BytesRecorded);
                };

                waveIn.StartRecording();

                try
                {
                    await Task.Delay(Timeout.Infinite, cancellationToken);
                }
                catch (TaskCanceledException)
                {
                    waveIn.StopRecording();
                }
            }
        }

        public async Task<string> TranscribeAudioAsync(string audioFilePath)
        {
            AudioTranscription transcription = await _audioClient.TranscribeAudioAsync(audioFilePath);
            return transcription.Text;
        }
    }
}
