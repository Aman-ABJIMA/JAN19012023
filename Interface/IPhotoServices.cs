using CloudinaryDotNet.Actions;

namespace WebApplicationMVC.Interface
{
    public interface IPhotoServices
    {
        Task<ImageUploadResult> AddPhotoAsync(IFormFile formFile);

        Task<DeletionResult> DeletePhotoAsync(string publicId);
    }
}
