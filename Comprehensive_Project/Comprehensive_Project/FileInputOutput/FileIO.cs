using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.IO.Directory;
namespace Comprehensive_Project.FileInputOutput
{
    class FileIO
    {
        public FileIO()
        {

        }
        public void allVoiceLocalFileDelete(String fileName)
        {
            Delete(fileName + ".mp4");
            Delete(fileName + ".webm");
            //mp4이거나 webm인지 확인하고 삭제할것 : 오류방지?
            Delete(fileName+".flac");
            Delete("mono+"+fileName + ".flac");
        }

    }
}
