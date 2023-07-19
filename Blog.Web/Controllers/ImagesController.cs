using Blog.Web.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Blog.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository _imageRepository;
        public ImagesController(IImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }
        [HttpPost]
        public async Task<IActionResult> UploadAsync(IFormFile file)
        {
            //call the repository
            var imageURL = await _imageRepository.UploadImageAsync(file);
            if (imageURL == null)
            {
                return Problem("Something went wrong!", null, (int)HttpStatusCode.InternalServerError);
            }
            return new JsonResult(new { link = imageURL });
        }
    }
}
