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
    class youtubeDownloder
    {
        static private string PROCESS_NAME_CMD = "youtubedownloader.exe";
        static ProcessStartInfo psi = null;
        static Process proc = null;
        String resultValue = null;
        public youtubeDownloder(String link)
        {
            psi = new ProcessStartInfo();
            psi.FileName = PROCESS_NAME_CMD;
            psi.CreateNoWindow = false;  // cmd 창 띄우기 -- true(띄우지 않기.) false(띄우기)
            psi.UseShellExecute = false;
            psi.RedirectStandardOutput = true; // cmd 데이터 받기
            psi.RedirectStandardInput = true; // cmd 데이터 보내기
            psi.RedirectStandardError = true; // cmd 오류내용 받기

            proc.StartInfo = psi;
            proc.Start(); //cmd 시작


            proc.StandardInput.Write(link + Environment.NewLine);
            proc.StandardInput.Close();

            String result = proc.StandardOutput.ReadToEnd();

            proc.WaitForExit();
            proc.Close();

            resultValue = result;

        }

        public String getResult()
        {
            return resultValue;
        }

    }



}




