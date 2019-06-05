using Google.Apis.Storage.v1;
using Google.Apis.Storage.v1.Data;
using Google.Cloud.Storage.V1;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using static System.Net.Mime.MediaTypeNames;

namespace Comprehensive_Project.Google_Cloud_Platform
{
    //bucketname : speech_limit;

    class GCPStorageUpload
    {
        String fileStorageLink;
        public GCPStorageUpload(string bucketName, string localPath, string objectName = null)
        {
            Console.WriteLine("localPath : "+ localPath);
            this.UploadFile(bucketName, localPath, objectName);
        }

        private void UploadFile(string bucketName, string localPath, string objectName = null)
        {
            var storage = StorageClient.Create();
            try
            {
                using (var f = File.OpenRead(localPath))
                {
                    objectName = objectName ?? Path.GetFileName(localPath);
                    storage.UploadObject(bucketName, objectName, null, f);
                    Console.WriteLine($"Uploaded {objectName}.");
                }
            }
            catch (System.ArgumentException)
            {
                Console.WriteLine("존재하지 않는 파일을 업로드 할 수 없습니다.\n프로그램을 5초 후에 종료합니다.");
                Thread.Sleep(5000);
                Environment.Exit(0);

            }
           
            fileStorageLink = FileStorageLinkSet(bucketName, objectName);
             // 파일 링크 생성
        }
        private String FileStorageLinkSet(String bucketName, String objectName)
        {
            var gcloudLink = "gs://" + bucketName + "/" + objectName;
            return gcloudLink;
        }
        public String GetFileStorageLink()
        {
            return fileStorageLink;
        }

    }

}
