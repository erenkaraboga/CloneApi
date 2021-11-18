using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CarApi.Helpers
{
    public static class FileHelper
    {
        public static async Task<string>  UploadImage(IFormFile file)
        {
            string connectionString = @"DefaultEndpointsProtocol=https;AccountName=carstroage;AccountKey=3OXKHIKtcna3gaXrKKSznDlnJ4y/W/IkBBi6JVrqozg8Sun2u7Yf3B1aHBuXo9UyKiaAfgW9ymxLBPVKn2UKGA==;EndpointSuffix=core.windows.net";
            string containerName = "carscover";
            BlobContainerClient blobContainerClient = new BlobContainerClient(connectionString, containerName);
            BlobClient blobClient = blobContainerClient.GetBlobClient(file.FileName);
            var memoryStream = new MemoryStream();
            
            await file.CopyToAsync(memoryStream);
            memoryStream.Position = 0;
            
            await blobClient.UploadAsync(memoryStream);
            return blobClient.Uri.AbsoluteUri;
        }
        public static async Task<string> UploadAudio(IFormFile file)
        {
            string connectionString = @"DefaultEndpointsProtocol=https;AccountName=carstroage;AccountKey=3OXKHIKtcna3gaXrKKSznDlnJ4y/W/IkBBi6JVrqozg8Sun2u7Yf3B1aHBuXo9UyKiaAfgW9ymxLBPVKn2UKGA==;EndpointSuffix=core.windows.net";
            string containerName = "audiocover";
            BlobContainerClient blobContainerClient = new BlobContainerClient(connectionString, containerName);
            BlobClient blobClient = blobContainerClient.GetBlobClient(file.FileName);
            var memoryStream = new MemoryStream();

            await file.CopyToAsync(memoryStream);
            memoryStream.Position = 0;

            await blobClient.UploadAsync(memoryStream);
            return blobClient.Uri.AbsoluteUri;
        }



    }
}
