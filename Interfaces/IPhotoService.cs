using CloudinaryDotNet.Actions;

namespace RunWepApp_withIdentity_TeddySmith_Youtube.Interfaces;

public interface IPhotoService
{
    Task<ImageUploadResult> AddPhotoAsync(IFormFile file);
    Task<DeletionResult> DeletePhotoAsync(string photoId);


}
