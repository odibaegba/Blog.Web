using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using System.Net;

namespace Blog.Web.Repositories
{
    public class CloudinaryImageRepository : IImageRepository
    {
        private readonly IConfiguration _configuration;
        private readonly Account account;

        public CloudinaryImageRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            account = new Account(
                configuration.GetSection("Cloudinary")["CloudName"],
                configuration.GetSection("Cloudinary")["ApiKey"],
                configuration.GetSection("Cloudinary")["ApiSecret"]
                );
        }
        public async Task<string> UploadImageAsync(IFormFile file)
        {
            var client = new Cloudinary(account);

            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(file.FileName, file.OpenReadStream()),
                DisplayName = file.Name,
            };
            var uploadResult = await client.UploadAsync(uploadParams);

            if (uploadResult != null && uploadResult.StatusCode == HttpStatusCode.OK)
            {
                return uploadResult.SecureUri.ToString();
            }

            return null;
        }
    }
}
