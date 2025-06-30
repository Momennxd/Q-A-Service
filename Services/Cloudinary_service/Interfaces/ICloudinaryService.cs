using CloudinaryDotNet.Actions;

namespace Services.Cloudinary_service.Interfaces
{
    public interface ICloudinaryService
    {

       
        Task<ImageUploadResult?> UploadImageAsync(Stream filestream, string folderPath, string fileName);


        string? FetchUrl(string publicId);

    }
}
