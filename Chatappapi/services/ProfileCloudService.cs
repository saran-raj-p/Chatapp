using Chatappapi.Helpers;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc.Diagnostics;

namespace Chatappapi.services
{
    public class ProfileCloudService
    {
        private readonly Cloudinary cloudinary; 

        public ProfileCloudService(ProfileHelperService profileHelper) 
        { 
            //get cloud instance or cloud connection for perfoem download,upload
            cloudinary = profileHelper.GetCloudinaryInstance(); 

        }

        public async Task<string> uploadImageToCloud(IFormFile image)
        {
            //get image name and image
            var profileName = $"{Guid.NewGuid().ToString()}_{image.FileName}";
            var profileImage = image.OpenReadStream();


            //Combine name and image into param for send it to cloudinary
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(profileName,profileImage),
                Folder = "Alpha_Chatapp_Profile_Images",

                PublicId = $"Alpha_Chatapp_Profile_Images/{profileName}",
            };


            //uplod an image to cloud
            var uploadedImageResult = await cloudinary.UploadAsync(uploadParams);


            //after upload get image url
            var uploadedImageUrl = uploadedImageResult.SecureUrl.AbsoluteUri; 


            return uploadedImageUrl;
        }

    }
}
