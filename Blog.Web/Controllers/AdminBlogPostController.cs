using Blog.Web.Models.Domain;
using Blog.Web.Models.ViewModels;
using Blog.Web.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Blog.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminBlogPostController : Controller
    {
        private readonly ITagRepository _tagRepository;
        private readonly IBlogPostRepository _blogPostRepository;
        public AdminBlogPostController(ITagRepository tagRepository, IBlogPostRepository blogPostRepository)
        {
            _tagRepository = tagRepository;
            _blogPostRepository = blogPostRepository;

        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            //get tags from repository
            var tags = await _tagRepository.GetAllAsync();
            var model = new AddBlogPostRequest
            {
                Tags = tags.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() })
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddBlogPostRequest addBlogPostRequest)
        {
            //Map the VM to domain Model
            var blogPost = new BlogPost
            {
                Heading = addBlogPostRequest.Heading,
                PageTitle = addBlogPostRequest.PageTitle,
                Content = addBlogPostRequest.Content,
                ShortDescription = addBlogPostRequest.ShortDescription,
                FeaturedImageUrl = addBlogPostRequest.FeaturedImageUrl,
                UrlHandle = addBlogPostRequest.UrlHandle,
                PublishedDate = addBlogPostRequest.PublishedDate,
                Author = addBlogPostRequest.Author,
                Visible = addBlogPostRequest.Visible,
            };

            //Map Tags from selected tags
            var selectedTags = new List<Tag>();
            foreach (var selectedTagId in addBlogPostRequest.SelectedTags)
            {
                var selectedTagIdAsGuid = Guid.Parse(selectedTagId);
                var existingTag = await _tagRepository.GetAsync(selectedTagIdAsGuid);
                if (existingTag != null)
                {
                    selectedTags.Add(existingTag);
                }
            }
            //Mapping tags back to domain model
            blogPost.Tags = selectedTags;
            await _blogPostRepository.AddAsync(blogPost);
            return RedirectToAction("Add");
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            //call the repository
            var blogPost = await _blogPostRepository.GetAllAsync();
            return View(blogPost);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            //Retrive the result from the repository
            var blogPost = await _blogPostRepository.GetAsync(id);
            var tagsDomainModel = await _tagRepository.GetAllAsync();

            //map the domain model into the view model
            if (blogPost != null)
            {
                var model = new EditBlogPostRequest
                {
                    Id = blogPost.Id,
                    Heading = blogPost.Heading,
                    PageTitle = blogPost.PageTitle,
                    Content = blogPost.Content,
                    Author = blogPost.Author,
                    FeaturedImageUrl = blogPost.FeaturedImageUrl,
                    UrlHandle = blogPost.UrlHandle,
                    ShortDescription = blogPost.ShortDescription,
                    PublishedDate = blogPost.PublishedDate,
                    Visible = blogPost.Visible,
                    Tags = tagsDomainModel.Select(x => new SelectListItem
                    {
                        Text = x.Name,
                        Value = x.Id.ToString()
                    }),
                    SelectedTags = blogPost.Tags.Select(x => x.Id.ToString()).ToArray()
                };
                return View(model);

            }

            //pass data to view
            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditBlogPostRequest editBlogpostRequest)
        {
            //map view model back to domain model
            var blogPostDomainModel = new BlogPost
            {
                Id = editBlogpostRequest.Id,
                Heading = editBlogpostRequest.Heading,
                PageTitle = editBlogpostRequest.PageTitle,
                Content = editBlogpostRequest.Content,
                Author = editBlogpostRequest.Author,
                ShortDescription = editBlogpostRequest.ShortDescription,
                FeaturedImageUrl = editBlogpostRequest.FeaturedImageUrl,
                PublishedDate = editBlogpostRequest.PublishedDate,
                UrlHandle = editBlogpostRequest.UrlHandle,
                Visible = editBlogpostRequest.Visible,
            };

            //Map tags into domain model
            var selectTags = new List<Tag>();
            foreach (var selectedTag in editBlogpostRequest.SelectedTags)
            {
                if (Guid.TryParse(selectedTag, out var tag))
                {
                    var foundTag = await _tagRepository.GetAsync(tag);
                    if (foundTag != null)
                    {
                        selectTags.Add(foundTag);
                    }
                }
            }
            blogPostDomainModel.Tags = selectTags;

            //submit information to repository to update
            var updatedBlog = await _blogPostRepository.UpdateAsync(blogPostDomainModel);
            if (updatedBlog != null)
            {
                //show success notification
                return RedirectToAction("Edit");
            }

            //show error notification
            return RedirectToAction("Edit");

        }

        [HttpPost]
        public async Task<IActionResult> Delete(EditBlogPostRequest editBlogpostRequest)
        {
            //Talk to the repository to delete this blog post and tags
            var deletedBlogPost = await _blogPostRepository.DeleteAsync(editBlogpostRequest.Id);
            if (deletedBlogPost != null)
            {
                //show success notification
                return RedirectToAction("List");
            }
            // show error message 
            return RedirectToAction("Edit", new { id = editBlogpostRequest.Id });
        }
    }
}
