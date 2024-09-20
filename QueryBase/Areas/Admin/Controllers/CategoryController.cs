using Microsoft.AspNetCore.Mvc;
using QueryBase.DataAccess.Repository.IRepository;
using QueryBase.Models;
using QueryBase.Models.ViewModels;

namespace QueryBase.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            CategoryPageVM categoryPageVM = new CategoryPageVM()
            {
                Categories = _unitOfWork.CategoryRepository.GetAll().ToList()
            };
            return View(categoryPageVM);
        }

        [HttpPost]
        public IActionResult Create(CategoryVM categoryVM)
        {
            Category category = new Category()
            {
                Id = Guid.NewGuid(),
                CategoryName = categoryVM.CategoryName
            };
            _unitOfWork.CategoryRepository.Add(category);
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(Guid Id)
        {
            _unitOfWork.CategoryRepository.Remove(_unitOfWork.CategoryRepository.Get(c => c.Id == Id));
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Edit(CategoryVM categoryVM)
        {
            Category category = _unitOfWork.CategoryRepository.Get(c => c.Id == categoryVM.Id);
            category.CategoryName = categoryVM.CategoryName;
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }
    }
}
