using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.IO.Directory;
using System.IO;
namespace Comprehensive_Project.FileInputOutput
{
    class FileIO
    {
        private String fileName;
        String fileNameMp4;
        String fileNameWebm;
        String fileNameFlacmono;
        public FileIO(String fileName)
        {
            this.fileName = fileName;
            this.FileExtensionChange();

        }
        public void AllVoiceLocalFileDelete(String fileName)
        {

            if (Exists(fileNameMp4))
            {
                try
                {
                    Console.WriteLine(fileName + ".mp4 파일 존재");
                    Delete(fileNameMp4);
                    Console.WriteLine(fileName + ".mp4 파일 삭제 성공");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    throw;
                }
            }
            else if (Exists(fileNameWebm))
            {
                try
                {
                    Console.WriteLine(fileName + ".webm 파일 존재");
                    Delete(fileNameWebm);
                    Console.WriteLine(fileName + ".webm 파일 삭제 성공");

                }
                catch (Exception e)
                {

                    throw;
                }
            }


            Delete(fileName + ".webm");
            //mp4이거나 webm인지 확인하고 삭제할것 : 오류방지?
            Delete(fileName + ".flac");
            Delete("mono+" + fileName + ".flac");
        }
        private void FileExtensionChange()
        {
            this.fileNameMp4 = this.fileName.Replace(".flac", ".mp4");//mp4파일명으로 교체
            this.fileNameWebm = this.fileName.Replace(".flac", ".webm");//webm파일으로 교체
            this.fileNameFlacmono = "mono+" + this.fileName;//mono flac파일 명으로 교체
        }


    }
}
