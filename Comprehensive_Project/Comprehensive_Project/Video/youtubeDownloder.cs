using IronPython.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Scripting.Hosting;
using System.Diagnostics;

namespace Comprehensive_Project.Video
{
    class YoutubeDownloder
    {
        static private string PROCESS_NAME_CMD = "youtubedownloader.exe";
        static ProcessStartInfo psi = null;
        static Process proc = null;
        String resultValue = null;
        public YoutubeDownloder(String youtubeLink)
        {
            psi = new ProcessStartInfo();
            proc = new Process();
            psi.FileName = PROCESS_NAME_CMD;
            psi.CreateNoWindow = false;  // cmd 창 띄우기 -- true(띄우지 않기.) false(띄우기)
            psi.UseShellExecute = false;
            psi.RedirectStandardOutput = true; // cmd 데이터 받기
            psi.RedirectStandardInput = true; // cmd 데이터 보내기
            psi.RedirectStandardError = true; // cmd 오류내용 받기

            proc.EnableRaisingEvents = false;
            proc.StartInfo = psi;
            proc.Start();    //프로세스 시작
            proc.StandardInput.Write(youtubeLink + Environment.NewLine);     //명령어를 입력+엔터키
            proc.StandardInput.Close();
            String error = proc.StandardError.ReadToEnd();//오류내용 읽기

 
            String resultFileName = proc.StandardOutput.ReadToEnd();

            resultFileName = resultFileName.Replace("", "");//개행문자 제거 파이썬에서 system('cls') 사용 시 ''가 추가로 나옴. 해당 문자열 제거용
            //Console.WriteLine("파일이름(다운로드 클래스 ReadToEnd직후) : " + resultFileName);

            if (error != null)
            {
                Console.WriteLine("error : " + error);
            }

            resultValue = resultFileName;

            proc.WaitForExit();
            proc.Close();

        }


        public String getResult()
        {
            return resultValue;
        }

    }

}




