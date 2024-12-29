using CloudinaryDotNet;

namespace Chatappapi.Helpers
{
    public class ProfileHelperService
    {
        private readonly Cloudinary _cloudinary;


        // Constructor to initialize the Cloudinary instance
        public ProfileHelperService()
        {
            _cloudinary = new Cloudinary(
                new Account(
                    "dofzts3vo", // Cloud name
                    "283961955427318", // api key
                    "s7h8SdCbeOuNde7T1mxmMl1LNt8" // ai secret
                )
            );
        }


        // Method to get the Cloudinary instance
        public Cloudinary GetCloudinaryInstance()
        {
            return _cloudinary;
        }
    }
}
