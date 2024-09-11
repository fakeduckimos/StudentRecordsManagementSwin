using Microsoft.AspNetCore.Mvc;
using StudentRecordManagement.Models;
using StudentRecordManagement.Models.Entities.Forms;
using StudentRecordManagement.Repositories.FormRecordRepository;
using System.Diagnostics;

namespace StudentRecordManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly FormRecordRepositoryFactory _factory;

        public HomeController(ILogger<HomeController> logger, FormRecordRepositoryFactory factory)
        {
            _logger = logger;
            _factory = factory;
        }

        //public IActionResult Index()
        //{
        //    return View();
        //}

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var formRecordRepository = _factory.CreateRepository<FormRecord>();
            var formRecords = await formRecordRepository.GetAllAsync();
            return View(formRecords);
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
