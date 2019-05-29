
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
namespace Comprehensive_Project.Parser
{
    class KoreanLanguageTextmining2017
    {
        //결과파일은 output.txt 고정
        //index.exe  --- 어절 단위, line 단위, 혹은 문장 단위로 색인어를 추출
        //indexT.exe --- 파일 단위로 색인어를 추출(빈도, 위치 계산 등)
        //ham-api.h C C++용만 있고 C#용은 없음

        static private string PROCESS_NAME_CMD = @"../../Parser/KLT2010-TestVersion-2017/EXE/indexT.exe";
        private readonly String inputFilePath = "../../Parser/KLT2010-TestVersion-2017/EXE/";
        private readonly String outputFilePath = "../../Parser/KLT2010-TestVersion-2017/EXE/output.txt";
        private readonly String outputFileName = "output.txt";
        static ProcessStartInfo psi = null;
        static Process proc = null;
        String resultValue = null;
        private static String[] resultArray;
        public KoreanLanguageTextmining2017(String InputFileName)
        {



            this.inputFilePath = inputFilePath + InputFileName;
            psi = new ProcessStartInfo();
            proc = new Process();
            psi.FileName = PROCESS_NAME_CMD;
            psi.CreateNoWindow = false;  // cmd 창 띄우기 -- true(띄우지 않기.) false(띄우기)
            psi.UseShellExecute = false;
            psi.Arguments = inputFilePath + " " + outputFilePath;
            psi.RedirectStandardOutput = true; // cmd 데이터 받기
            psi.RedirectStandardInput = true; // cmd 데이터 보내기
            psi.RedirectStandardError = true; // cmd 오류내용 받기

            Console.WriteLine("InputFileName : " + InputFileName);
            Console.WriteLine("inputFilePath : " + inputFilePath);

            proc.EnableRaisingEvents = false;
            proc.StartInfo = psi;
            proc.Start();    //프로세스 시작

            //proc.StandardInput.Write(InputFileName + " " + outputFileName + Environment.NewLine);
            //proc.StandardInput.Close();


            String resultFileName = proc.StandardOutput.ReadToEnd();
            String error = proc.StandardError.ReadToEnd();//오류내용 읽기

            Console.WriteLine(resultFileName);


            if (error != null)
            {
                Console.WriteLine("error : " + error);
            }

            proc.WaitForExit();

            proc.Close();


        }

        public String[] StringSplit(String input)
        {
            String[] splitResult = input.Split(new char[] { ' ' });

            return splitResult;
        }
        public static String[] GetResult()
        {
            return resultArray;
        }
    }
}
