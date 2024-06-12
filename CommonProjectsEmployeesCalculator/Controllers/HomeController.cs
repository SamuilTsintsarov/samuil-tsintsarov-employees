using BusinessLogicLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CommonProjectsEmployeesCalculator.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICalculateCommonProjectsService _commonProjectsCalculationService;
        private readonly ICsvLoaderService _csvLoadingService;

        public HomeController(ICalculateCommonProjectsService commonProjectsCalculationService, ICsvLoaderService csvLoadingService)
        {
            _commonProjectsCalculationService = commonProjectsCalculationService;
            _csvLoadingService = csvLoadingService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Upload(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                try
                {
                    var records = _csvLoadingService.LoadCsv(file);
                    var commonProjects = _commonProjectsCalculationService.CalculateCommonProjects(records);

                    return View("Results", commonProjects);
                }
                catch (Exception ex)
                {
                    //todo log ex.
                    return RedirectToAction("Error", "Home");
                }
            }

            return RedirectToAction("Index");
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
