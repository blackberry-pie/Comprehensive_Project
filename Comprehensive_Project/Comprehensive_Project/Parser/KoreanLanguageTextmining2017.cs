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
        static readonly private string PROCESS_NAME_CMD = "../../Parser/KLT2010-TestVersion-2017/EXE/indexT.exe";
        static ProcessStartInfo psi = null;
        static Process proc = null;
        String resultValue = null;

        public KoreanLanguageTextmining2017(String input)
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
            proc.StandardInput.Write( "파일이름" + Environment.NewLine);
        }


        public String[] StringSplit(String input)
        {
            String[] splitResult = input.Split(new char[] { ' ' });

            return splitResult;
        }
    }
}
