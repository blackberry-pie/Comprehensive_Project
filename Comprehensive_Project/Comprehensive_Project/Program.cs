using Moda.Korean.TwitterKoreanProcessorCS;
using Comprehensive_Project.Google_Cloud_Platform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Comprehensive_Project
{
    class Program
    {
        private static readonly string bucketname = "speech_limit";



        static int Main(string[] args)
        {

            //youtube 다운로더 모듈 시작


            //youtube 다운로더 모듈 종료

            //flac 변환 모듈 시작



            //flac 변환 모듈 시작


            //String storageUri = "gs://speech_limit/즉결처형 권한을 가진 나치 장교를 만난 유대인 피아니스트 1ch (online-audio-converter.com).flac"; //google cloud storage link
            String objectName = "범죄도시中 진선규 조선족 연기ㄷㄷㄷ channel 1(online-audio-converter.com).flac";//구글 스토리지에 업로드되는 이름
            String filePath = "범죄도시中 진선규 조선족 연기ㄷㄷㄷ channel 1(online-audio-converter.com).flac";//업로드 대상 로컬쪽



            //STT 모듈 시작
            //GCP Storage upload 모듈
            GCPStorageUpload storage = new GCPStorageUpload(bucketname, filePath, objectName);
            String storageUri = storage.getFileStorageLink();


            ///GCP Speech-to-Text 모듈
            SpeechToText stt = new SpeechToText(storageUri);
            var sttResult = stt.getResult();
            //STT 모듈 종료

            Console.WriteLine("stt 결과 : " + sttResult + "\n\n\n\n\n\n\n");


            //Parser 모듈 시작
            Parser parser = new Parser(sttResult);
            String parserResult = parser.getResult();
            //Parser 모듈 종료

            Console.WriteLine("메인 본문 파서 결과 : " + parserResult + "\n\n\n\n\n\n\n");
            Console.ReadKey();
            return 0;


        }
    }
}
