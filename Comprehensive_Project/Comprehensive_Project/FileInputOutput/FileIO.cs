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
        private String currentPath;// = @"C:\Users\Public\TestFolder";
        private String fileName; //filename.flac 형식
        private String onlyFileName; // 파일명만 추출
        String fileNameMp4;//filename.mp4
        String fileNameWebm;//filename.webm
        String fileNameFlacmono; //mono+filename.flac
        public FileIO(String fileName)
        {
            this.FileName = fileName;
            this.FileExtensionChange();
            this.currentPath = GetCurrentDirectory();
            //http://bbs.nicklib.com/application/4051

        }
        public void FileRead(String OpenFileName)
        {

        }
        public string FileName { get => fileName; set => fileName = value; }

        public void AllVoiceLocalFileDelete()
        {

            if (Exists(fileNameMp4))
            {
                try
                {
                    Console.WriteLine(FileName + ".mp4 파일 존재");
                    Delete(currentPath + fileNameMp4);
                    Console.WriteLine(FileName + ".mp4 파일 삭제 성공");
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
                    Console.WriteLine(FileName + ".webm 파일 존재");
                    Delete(fileNameWebm);
                    Console.WriteLine(FileName + ".webm 파일 삭제 성공");

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    throw;
                }
            }
            try
            {
                Delete(FileName);
                Delete(fileNameFlacmono);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }

        }
        private void FileExtensionChange()
        {
            onlyFileName = Path.GetFileNameWithoutExtension(FileName);

            this.fileNameMp4 = this.onlyFileName + ".mp4";//mp4파일명으로 교체
            this.fileNameWebm = this.onlyFileName + ".webm";//webm파일으로 교체
            this.fileNameFlacmono = "mono+" + this.FileName;//mono flac파일 명으로 교체
        }

    }
}
