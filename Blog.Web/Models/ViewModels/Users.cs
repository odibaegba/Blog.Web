namespace Blog.Web.Models.ViewModels
{
	public class Users
	{
		public Guid Id { get; set; }
		public string UserName { get; set; } = string.Empty;
		public string EmailAddress { get; set; } = string.Empty;
	}
}
