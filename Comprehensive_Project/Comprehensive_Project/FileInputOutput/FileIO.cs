using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace Comprehensive_Project.FileInputOutput
{
    class FileIO
    {
        private String currentPath;// = @"C:\Users\Public\TestFolder";
        private String fileName; //filename.flac 형식으로 받음
        private String filePath; // 디렉토리 경로 + 파일명+확장자
        private String onlyFileName; // 파일명만 추출
        private String fileNameMp4;//filename.mp4
        private String filePathMp4;//디렉토리 경로 + 파일명+ mp4확장자
        private String fileNameWebm;//filename.webm
        private String filePathWebm;
        private String fileNameFlacmono; //mono+filename.flac
        private String filePathFlacmono;
        public FileIO(String fileName)
        {
            this.currentPath = System.IO.Directory.GetCurrentDirectory();//파일 실행 위치 디렉토리로 받기
            this.FileName = fileName;
            this.FileExtensionChange();// flac mp4 webm 확장자로 설정
            this.FilePathSet(); //각각의 확장자의 경로 설정
        }
        public void FileWrite(String WriteFileName, String contents)
        {
            String savePath = "../../Parser/KLT2010-TestVersion-2017/EXE/" + WriteFileName;
            System.IO.File.WriteAllText(savePath, contents, Encoding.Default);//운영체제의 현재 ANSI 코드 페이지에 대한 인코딩을 가

            using (Stream fs = new FileStream(savePath, FileMode.Create))//새파일 생성, 파일이 있을 경우 해당 파일 덮어씀
            {

            }
            

        }
        public void FileRead(String ReadFileName)
        {

        }
        public string FileName { get => fileName; set => fileName = value; }

        public void AlㅣLocalFileDelete()
        {
            if (System.IO.File.Exists(filePathMp4))//mp4파일 확인 후 삭제
            {
                try
                {
                    Console.WriteLine(FileName + ".mp4 파일 존재");
                    System.IO.File.Delete(filePathMp4);
                    Console.WriteLine(FileName + ".mp4 파일 삭제 성공");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    throw;
                }
            }
            else if (System.IO.File.Exists(filePathWebm))//webm파일 확인 후 삭제
            {
                try
                {
                    Console.WriteLine(FileName + ".webm 파일 존재");
                    System.IO.File.Delete(filePathWebm);
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
                System.IO.File.Delete(filePath);
                Console.WriteLine(fileName + "파일 삭제 성공");
                System.IO.File.Delete(filePathFlacmono);
                Console.WriteLine("mono+"+fileName + "파일 삭제 성공");
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
        private void FilePathSet()
        {
            this.filePath = this.currentPath + @"\" + this.fileName;
            this.filePathMp4= this.currentPath + @"\" + this.fileNameMp4;
            this.filePathWebm = this.currentPath + @"\" + this.fileNameWebm;
            this.filePathFlacmono = this.currentPath + @"\" + this.fileNameFlacmono;
        }

    }
}
