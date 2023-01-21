using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;
using WebApplicationMVC.Helpers;
using WebApplicationMVC.Interface;

namespace WebApplicationMVC.Services
{
    public class PhotoService : IPhotoServices
    {
        private readonly Cloudinary _config;
        public PhotoService(IOptions<CloudinarySettings> config)
        {
            var acc = new Account(
                config.Value.CloudName,
                config.Value.ApiKey,
                config.Value.ApiSecret
                );
            
            _config = new Cloudinary( acc );


        }

   

        public Task<ImageUploadResult> AddPhotoAsync(IFormFile formFile)
        {
            throw new NotImplementedException();
        }

        public Task<DeletionResult> DeletePhotoAsync(string publicId)
        {
            throw new NotImplementedException();
        }
    }
}
