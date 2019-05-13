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
        static String link = null;
        public YoutubeDownloder(String link)
        {
            YoutubeDownloder.link = link;
            psi = new ProcessStartInfo();
            proc = new Process();
            psi.FileName = PROCESS_NAME_CMD;
            psi.CreateNoWindow = false;  // cmd 창 띄우기 -- true(띄우지 않기.) false(띄우기)
            psi.UseShellExecute = false;
            psi.RedirectStandardOutput = true; // cmd 데이터 받기
            psi.RedirectStandardInput = true; // cmd 데이터 보내기
            psi.RedirectStandardError = true; // cmd 오류내용 받기

            proc.EnableRaisingEvents = true;


            proc.ErrorDataReceived += (object sending_process, DataReceivedEventArgs e) =>
            {
                if (e.Data != null)
                {
                    Console.WriteLine(e.Data.ToString());
                }
            };
            proc.OutputDataReceived += (object sending_process, DataReceivedEventArgs e) =>
            {
                if(e.Data != null)
                {
                    Console.WriteLine(e.Data.ToString());
                }
            };

            DoAction();//cmd 시작



            /*
            String result = proc.StandardOutput.ReadToEnd();

            Console.WriteLine("result : " + result);
            resultValue = result;
            */
        }
        static async void DoAction()
        {
            proc.StartInfo = psi;
            proc.Start();

            proc.BeginOutputReadLine();
            proc.BeginErrorReadLine();

            await DoSignAsync();

            proc.CancelOutputRead();
            proc.CancelErrorRead();

            proc.WaitForExit();
            proc.Close();




        }
        static async Task<String> DoSignAsync()
        {
            await Task.Delay(20);

            proc.StandardInput.Write(link + Environment.NewLine);
            proc.StandardInput.Close();

            for(int j = 0;j<10 ;j++)
            {
                await Task.Delay(1000);
                Console.WriteLine(j);

            }



            return null;
        }


        public String getResult()
        {
            return resultValue;
        }

    }



}




