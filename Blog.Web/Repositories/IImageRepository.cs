namespace Blog.Web.Repositories
{
    public interface IImageRepository
    {
        //we are returning the image url
        Task<string> UploadImageAsync(IFormFile file);
    }
}
