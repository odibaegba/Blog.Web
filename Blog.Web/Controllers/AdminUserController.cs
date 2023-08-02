using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Controllers
{
	public class AdminUserController : Controller
	{
		public IActionResult List()
		{
			return View();
		}
	}
}
