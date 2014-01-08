using System.Web.Mvc;
using MuonLab.Validation.Example.ViewModels;

namespace MuonLab.Validation.Example.Controllers
{
	public class HomeController : Controller
	{
		[AcceptVerbs(HttpVerbs.Get)]
		public ActionResult Index()
		{
			return View("Index", new TestViewModel());
		}

		[AcceptVerbs(HttpVerbs.Post)]
		public ActionResult Index(TestViewModel viewModel)
		{
			if (!ModelState.IsValid)
				return this.View("Index", viewModel);

			// do something

			return View("Index", viewModel);
		}
	}
}