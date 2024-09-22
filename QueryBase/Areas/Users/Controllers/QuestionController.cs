using Microsoft.AspNetCore.Mvc;
using QueryBase.DataAccess.Repository.IRepository;
using QueryBase.Models.ViewModels;

namespace QueryBase.Areas.Users.Controllers
{
    [Area("Users")]
    public class QuestionController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public QuestionController(IUnitOfWork unitOfWork)
        {
             _unitOfWork = unitOfWork;
        }
        public IActionResult Index(Guid questionId)
        {
            var questionVM = new QuestionVM
            {
                Question = _unitOfWork.QuestionRepository.Get(q => q.Id == questionId, includeProperties: "User,Category"),
                Answers = _unitOfWork.AnswerRepository.GetAll(a => a.QuestionId == questionId, includeProperties: "User").ToList()
            };
            return View(questionVM);
        }
    }
}
