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
namespace Comprehensive_Project.Google_Cloud_Platform
{
    //bucketname : speech_limit;

    class GCPStorageUpload
    {
        String FileStorageLink;
        public GCPStorageUpload(string bucketName, string localPath, string objectName = null)
        {
            Console.WriteLine("localPath : "+ localPath);
            this.UploadFile(bucketName, localPath, objectName);
        }

        private void UploadFile(string bucketName, string localPath, string objectName = null)
        {
            var storage = StorageClient.Create();
            using (var f = File.OpenRead(localPath)) // 처리되지 않은 예외: System.ArgumentException: 경로에 잘못된 문자가 있습니다. 오류발생
            {
                objectName = objectName ?? Path.GetFileName(localPath);
                storage.UploadObject(bucketName, objectName, null, f);
                Console.WriteLine($"Uploaded {objectName}.");
            }
            FileStorageLink = "gs://" + bucketName + "/" + objectName; // 파일 링크 생성
        }

        public String getFileStorageLink()
        {
            return FileStorageLink;
        }

    }

}
