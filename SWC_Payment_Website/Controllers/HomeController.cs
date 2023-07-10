using Microsoft.AspNetCore.Mvc;
using SWC_Payment_Website.Models;
using System.Diagnostics;

namespace SWC_Payment_Website.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return RedirectToAction("Index", "Contact");
        }

        [HttpPost]
        public ActionResult Index(object model)
        {
            if(!ModelState.IsValid) return View(model);

            //Send email to me with message

            return Redirect("https://swcdynamics.com/");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}