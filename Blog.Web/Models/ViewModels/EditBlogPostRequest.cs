namespace Blog.Web.Models.ViewModels
{
    public class EditBlogPostRequest : AddBlogPostRequest
    {
        public Guid Id { get; set; }
    }
}
