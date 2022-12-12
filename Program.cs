using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;

class Program 
{
    // This example requires environment variables named "SPEECH_KEY" and "SPEECH_REGION"
    static string speechKey = Environment.GetEnvironmentVariable("SPEECH_KEY");
    static string speechRegion = Environment.GetEnvironmentVariable("SPEECH_REGION");

    static void OutputSpeechSynthesisResult(SpeechSynthesisResult speechSynthesisResult, string text)
    {
        switch (speechSynthesisResult.Reason)
        {
            case ResultReason.SynthesizingAudioCompleted:
                Console.WriteLine($"Speech synthesized for text: [{text}]");
                break;
            case ResultReason.Canceled:
                var cancellation = SpeechSynthesisCancellationDetails.FromResult(speechSynthesisResult);
                Console.WriteLine($"CANCELED: Reason={cancellation.Reason}");

                if (cancellation.Reason == CancellationReason.Error)
                {
                    Console.WriteLine($"CANCELED: ErrorCode={cancellation.ErrorCode}");
                    Console.WriteLine($"CANCELED: ErrorDetails=[{cancellation.ErrorDetails}]");
                    Console.WriteLine($"CANCELED: Did you set the speech resource key and region values?");
                }
                break;
            default:
                break;
        }
    }

    async static Task Main(string[] args)
    {
        var speechConfig = SpeechConfig.FromSubscription(speechKey, speechRegion); 
        List<string> voicesESP = new List<string>() {"es-ES-AlvaroNeural",  "es-ES-ElviraNeural"};
        List<string> voicesCAT = new List<string>() {"ca-ES-AlbaNeural", "ca-ES-EnricNeural", "ca-ES-JoanaNeural"};

        // Creating the audio files in Spanish
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("Creating audios in Spanish, please wait...");
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        for (int i = 0; i < voicesESP.Count; i++)
        {
            var audioFileESP = voicesESP[i];
            speechConfig.SpeechSynthesisVoiceName = audioFileESP;
            using var audioESPConfig = AudioConfig.FromWavFileOutput("C:\\Users\\mcerverar\\Desktop\\Text to voice\\audios\\" + audioFileESP + ".wav");
            using (var speechSynthesizer = new SpeechSynthesizer(speechConfig, audioESPConfig))
            {
                // Get text from the string variable and synthesize to the default speaker.
                string text = "Audio example";
    
                var speechSynthesisResult = await speechSynthesizer.SpeakTextAsync(text);
                OutputSpeechSynthesisResult(speechSynthesisResult, text);
            }
        }
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Audios in Spanish created and stored!");
        // Creating the audio files in Catalan
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("Creating audios in Catalan, please wait...");
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        for (int i = 0; i < voicesCAT.Count; i++)
        {
            var audioFileCAT = voicesCAT[i];
            speechConfig.SpeechSynthesisVoiceName = audioFileCAT;
            using var audioCATConfig = AudioConfig.FromWavFileOutput("C:\\Users\\mcerverar\\Desktop\\Text to voice\\audios\\" + audioFileCAT + ".wav");
            using (var speechSynthesizer = new SpeechSynthesizer(speechConfig, audioCATConfig))
            {
                // Get text from the string variable and synthesize to the default speaker.
                string text = "Audio example";
    
                var speechSynthesisResult = await speechSynthesizer.SpeakTextAsync(text);
                OutputSpeechSynthesisResult(speechSynthesisResult, text);
            }
        }
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Audios in catalan created and stored");
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("All processes completed successfully!");
        Console.ForegroundColor = ConsoleColor.White;
        Environment.Exit(0);
    }

}