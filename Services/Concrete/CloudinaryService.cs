using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Services.Concrete
{
    public class CloudinaryService : ICloudinaryService
    {

        //instead of ICloudinary to make it easier to work with.
        private readonly Cloudinary _cloudinary;

        public CloudinaryService(Cloudinary cloudinary)
        {
            _cloudinary = cloudinary;
        }

        public string FetchUrlAsync(string publicId)
        {
            return _cloudinary.Api.UrlImgUp.BuildUrl(publicId + ".jpg");
        }



        public async Task<ImageUploadResult?> UploadImageAsync(Stream filestream, string folderPath, string fileName)
        {

            if (folderPath == null || folderPath.Length == 0 || filestream == null)
                return null;
       
            try
            {

                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(fileName, filestream),
                    Folder = folderPath  
                };

                // Upload the image to Cloudinary
                return await _cloudinary.UploadAsync(uploadParams);

            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error uploading image to Cloudinary", ex);
            }


        }


    }
}
