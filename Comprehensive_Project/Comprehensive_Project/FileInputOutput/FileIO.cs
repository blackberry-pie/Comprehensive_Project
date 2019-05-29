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
        private String fileName; //filename.flac 형식
        private String onlyFileName; // 파일명만 추출
        String fileNameMp4;//filename.mp4
        String fileNameWebm;//filename.webm
        String fileNameFlacmono; //mono+filename.flac
        public FileIO(String fileName)
        {
            this.fileName = fileName;
            this.FileExtensionChange();

        }
        public void AllVoiceLocalFileDelete()
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
                    Console.WriteLine(e.Message);
                    throw;
                }
            }
            try
            {
                Delete(fileName);
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

            onlyFileName = Path.GetFileNameWithoutExtension(fileName);

            this.fileNameMp4 = this.onlyFileName + ".mp4";//mp4파일명으로 교체
            this.fileNameWebm = this.onlyFileName + ".webm";//webm파일으로 교체
            this.fileNameFlacmono = "mono+" + this.fileName;//mono flac파일 명으로 교체
        }


    }
}
