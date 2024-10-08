using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QueryBase.DataAccess.Repository.IRepository;
using QueryBase.Models;
using System.Diagnostics;

namespace QueryBase.Areas.Users.Controllers
{
    [Area("Users")]
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            List<Question> questions = _unitOfWork.QuestionRepository.GetAll(includeProperties: "Category,User").ToList();
            return View(questions);
        }

        //public IActionResult Index(string? search,)
        //{

        //    return View();
        //}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
