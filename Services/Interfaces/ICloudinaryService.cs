using System.Xml;
using Services.Enums;
using CloudinaryDotNet.Actions;
using CloudinaryDotNet;

namespace Services.Interfaces
{
    public interface ICloudinaryService
    {

       
        Task<ImageUploadResult?> UploadImageAsync(Stream filestream, string folderPath, string fileName);


        string? FetchUrl(string publicId);

    }
}
