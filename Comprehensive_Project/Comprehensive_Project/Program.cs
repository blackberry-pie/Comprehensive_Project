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
        //private static readonly String ParserInputFileName = "input.txt";
        //https://www.youtube.com/watch?v=vpnY2CBLOiY 범죄도시 테스트용
        static int Main(string[] args)
        {
            
            String youtubeLink = args[0];
            Console.WriteLine("Link: " + youtubeLink);
            //youtube 다운로더 모듈 시작
            Comprehensive_Project.Video.YoutubeDownloder yd = new Comprehensive_Project.Video.YoutubeDownloder(youtubeLink); //args[0]로는 안됨. 직접 값을 넣을 경우 작동함
            var fileName = yd.GetResult();
            Console.WriteLine("파일 이름:" + fileName);
            //youtube 다운로더 모듈 종료

            String objectName = fileName; //구글 스토리지에 업로드되는 이름
            String filePath = fileName; //업로드 대상 로컬쪽
            Console.WriteLine("filePath : " + filePath);

            

            //STT 모듈 시작
            //GCP Storage upload 모듈
            var storage = new Comprehensive_Project.Google_Cloud_Platform.GCPStorageUpload(bucketname, filePath, objectName);
            String storageUri = storage.GetFileStorageLink();

            Console.WriteLine("Speech-to-Text를 시작합니다.");
            ///GCP Speech-to-Text 모듈
            Comprehensive_Project.Google_Cloud_Platform.SpeechToText stt = new Comprehensive_Project.Google_Cloud_Platform.SpeechToText(storageUri);
            var sttResult = stt.GetResult();
            //STT 모듈 종료

            Console.WriteLine("stt 결과 : " + sttResult + "\n\n\n\n\n\n\n");

 

            //Parser 모듈 시작
            Comprehensive_Project.Parser.KoreanParser parser = new Comprehensive_Project.Parser.KoreanParser(sttResult);
            var parserResult = Comprehensive_Project.Parser.KoreanParser.GetResult();
            //Parser 모듈 종료
            
            Console.WriteLine("메인 본문 파서 결과 : ");
            for (int i = 0; i < parserResult.Length; i++)
            {
                Console.WriteLine(parserResult[i]);
            }
            Console.WriteLine("끝");
            



            var fileIO = new Comprehensive_Project.FileInputOutput.FileIO(fileName);//파일 입출력 관련 함수
            //fileIO.FileWrite(ParserInputFileName, sttResult);

             //var newParserKLT = new Comprehensive_Project.Parser.KoreanLanguageTextmining2017(ParserInputFileName);

            //var parserResultKLT = Comprehensive_Project.Parser.KoreanLanguageTextmining2017.getResult(); //결과물은 String배열 형태로 할것


            Comprehensive_Project.DataBase.DataBaseAccess dbas = new Comprehensive_Project.DataBase.DataBaseAccess(parserResult);

            Console.Clear();
            Comprehensive_Project.basic_Algo.algo algo = new Comprehensive_Project.basic_Algo.algo();

            //처리 후 삭제 테스트

            fileIO.AlㅣLocalFileDelete();
            //Console.ReadKey();
            return 0;

        }
    }
}
