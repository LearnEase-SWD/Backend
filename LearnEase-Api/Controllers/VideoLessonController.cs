using Microsoft.AspNetCore.Mvc;

namespace LearnEase_Api.Controllers
{
	public class VideoLessonController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
