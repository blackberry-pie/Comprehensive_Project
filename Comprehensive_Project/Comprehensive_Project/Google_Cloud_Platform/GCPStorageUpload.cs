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
            catch (ArgumentException)
            {
                Console.WriteLine("빈 경로 이름은 사용할 수 없습니다.");
                throw;
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
