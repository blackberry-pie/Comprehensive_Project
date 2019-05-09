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
            this.UploadFile(bucketName, localPath, objectName);
        }

        private void UploadFile(string bucketName, string localPath, string objectName = null)
        {
            var storage = StorageClient.Create();
            using (var f = File.OpenRead(localPath))
            {
                objectName = objectName ?? Path.GetFileName(localPath);
                storage.UploadObject(bucketName, objectName, null, f);
                Console.WriteLine($"Uploaded {objectName}.");
            }
            FileStorageLink = "gs://" + bucketName + "/" + objectName;
        }

        public String getFileStorageLink()
        {
            return FileStorageLink;
        }

    }

}
