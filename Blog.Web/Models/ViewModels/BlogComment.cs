namespace Blog.Web.Models.ViewModels
{
	public class BlogComment
	{
		public string Description { get; set; } = string.Empty;
		public DateTime DateAdded { get; set; }
		public string Username { get; set; } = string.Empty;
	}
}
