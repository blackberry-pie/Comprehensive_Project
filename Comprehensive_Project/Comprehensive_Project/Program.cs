﻿using Moda.Korean.TwitterKoreanProcessorCS;
using Comprehensive_Project.Google_Cloud_Platform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Comprehensive_Project.Video;
using Comprehensive_Project.DataBase;

namespace Comprehensive_Project
{
    class Program
    {
        private static readonly string bucketname = "speech_limit";



        static int Main(string[] args)
        {
            
            var youtubeLink = Console.ReadLine();
            //youtube 다운로더 모듈 시작
            YoutubeDownloder yd = new YoutubeDownloder(youtubeLink); //args[0]로는 안됨. 직접 값을 넣을 경우 작동함
            var fileName =  yd.getResult();
            Console.WriteLine("파일 이름:" + fileName);
            //youtube 다운로더 모듈 종료

            
            String objectName = fileName; //구글 스토리지에 업로드되는 이름
            String filePath = fileName; //업로드 대상 로컬쪽
            Console.WriteLine("filePath : "+filePath);

            //String objectName = "mono+범죄도시中 진선규 조선족 연기ㄷㄷㄷ.flac";//구글 스토리지에 업로드되는 이름
            //String filePath = "mono+범죄도시中 진선규 조선족 연기ㄷㄷㄷ.flac";//업로드 대상 로컬쪽


            //STT 모듈 시작
            //GCP Storage upload 모듈
            var storage = new GCPStorageUpload(bucketname, filePath, objectName);
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

            var splitresult = Program.StringSplit(parserResult);

            /*
              //배열 출력 확인용
            for(int i = 0;i < splitresult.Length;i++)
            {
                Console.WriteLine(splitresult[i]);
            }
            */
            DataBaseAccess dbas = new DataBaseAccess(splitresult);
            Console.ReadKey();
            return 0;


        }

        public static String[] StringSplit(String input)
        {
            String[] splitResult = input.Split(new char[]{' '});

            return  splitResult;
        }
        

    }
}
