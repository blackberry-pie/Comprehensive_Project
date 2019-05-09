using Google.Apis.Auth.OAuth2;
using Google.Cloud.Speech.V1;
using Grpc.Auth;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Text;
namespace preprocessor.Google_Cloud_Platform
{
    class SpeechToText
    {
        StringBuilder sttResult;
        public SpeechToText(String storageUri)
        {
            sttResult = new StringBuilder();
            var speech = SpeechClient.Create();
            var longOperation = speech.LongRunningRecognize(new RecognitionConfig()
            {
                Encoding = RecognitionConfig.Types.AudioEncoding.Flac,
                //SampleRateHertz = 16000,
                LanguageCode = "ko",
            }, RecognitionAudio.FromStorageUri(storageUri));
            longOperation = longOperation.PollUntilCompleted();
            var response = longOperation.Result;
            foreach (var result in response.Results)
            {
                foreach (var alternative in result.Alternatives)
                {
                    //Console.WriteLine($"Transcript: { alternative.Transcript}"); //콘솔확인용
                    sttResult.AppendLine(alternative.Transcript);
                }
            }
        }
        public String getResult()
        {
            return sttResult.ToString();
        }

    }
}
