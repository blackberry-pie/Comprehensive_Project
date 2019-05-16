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
            proc.StandardInput.Write(youtubeLink + Environment.NewLine);     //예를 들어 dir명령어를 입력
            proc.StandardInput.Close();
            String error = proc.StandardError.ReadToEnd();//오류내용 읽기

 
            String resultFileName = proc.StandardOutput.ReadToEnd();

            Console.WriteLine("파일이름(다운로드 클래스 ReadToEnd직후) : " + resultFileName);

            if(error != null)
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




