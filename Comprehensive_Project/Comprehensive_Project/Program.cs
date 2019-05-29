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
            var youtubeLink = Console.ReadLine();
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


            ///GCP Speech-to-Text 모듈
            Comprehensive_Project.Google_Cloud_Platform.SpeechToText stt = new Comprehensive_Project.Google_Cloud_Platform.SpeechToText(storageUri);
            var sttResult = stt.GetResult();
            //STT 모듈 종료

            Console.WriteLine("stt 결과 : " + sttResult + "\n\n\n\n\n\n\n");


            //Parser 모듈 시작
            Comprehensive_Project.Parser.KoreanParser parser = new Comprehensive_Project.Parser.KoreanParser(sttResult);
            var parserResult = Comprehensive_Project.Parser.KoreanParser.GetResult();
            //Parser 모듈 종료

            Console.WriteLine("메인 본문 파서 결과 : " + parserResult + "\n\n\n\n\n\n\n");
            for (int i = 0; i < parserResult.Length; i++)
            {
                Console.WriteLine(parserResult[i]);
            }
            //var splitresult = Program.StringSplit(parserResult);

            Comprehensive_Project.DataBase.DataBaseAccess dbas = new Comprehensive_Project.DataBase.DataBaseAccess(parserResult);

            //var fileIO = new Comprehensive_Project.FileInputOutput.FileIO(fileName);
            //fileIO.AllVoiceLocalFileDelete();//Debug폴더의 파일 삭제

            Console.ReadKey();
            return 0;

        }
    }
}
