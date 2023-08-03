using System.ComponentModel.DataAnnotations;

namespace Blog.Web.Models.ViewModels
{
	public class RegisterViewModel
	{
		[Required]
		public string Username { get; set; } = string.Empty;

		[Required]
		[EmailAddress]
		public string Email { get; set; } = string.Empty;

		[Required]
		[MinLength(6, ErrorMessage = "Password has to be at least 6 characters")]
		public string Password { get; set; } = string.Empty;
	}
}
