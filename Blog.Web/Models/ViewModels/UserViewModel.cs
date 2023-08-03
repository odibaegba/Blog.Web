namespace Blog.Web.Models.ViewModels
{
    public class UserViewModel
    {
        public List<Users> Users { get; set; }

        public string UserName { get; set; } = string.Empty;
        public string EmailAddress { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public bool AdminCheckbox { get; set; }
    }
}
