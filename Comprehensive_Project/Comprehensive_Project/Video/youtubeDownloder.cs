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
        static readonly private string PROCESS_NAME_CMD = @"../../Video/youtubedownloader.exe";
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
            proc.StandardInput.Write(youtubeLink + Environment.NewLine);     //명령어를 입력
            proc.StandardInput.Close();
            String error = proc.StandardError.ReadToEnd();//오류내용 읽기


            String resultFileName = proc.StandardOutput.ReadToEnd();

            this.InvalidPathCharsRemove(ref resultFileName);

            if (error != null)
            {
                Console.WriteLine("error : " + error);
            }
            resultValue = resultFileName;

            proc.WaitForExit();
            proc.Close();

        }


        public String GetResult()
        {
            return resultValue;
        }

        public void InvalidPathCharsRemove(ref String checkFileName)
        {
            checkFileName = checkFileName.Replace("", "");// system('cls')로 화면 지울 시 '' 문자가 추가됨 해당 문자 제거
            checkFileName = checkFileName.Replace("\r", "");// https://stackoverflow.com/questions/21389137/why-illegal-characters-in-path
            checkFileName = checkFileName.Replace("\n", "");
            checkFileName = checkFileName.Replace("\t", "");
        }

    }

}




