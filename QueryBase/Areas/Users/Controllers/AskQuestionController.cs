using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.IdentityModel.Tokens;
using QueryBase.DataAccess.Repository.IRepository;
using QueryBase.Models;
using QueryBase.Models.ViewModels;
using System.Security.Claims;
using System.Text.Json.Nodes;

namespace QueryBase.Areas.Users.Controllers
{
    [Area("Users")]
    public class AskQuestionController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IUnitOfWork _unitOfWork;
        public AskQuestionController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
            _unitOfWork = unitOfWork;
        }
        public IActionResult Ask()
        {

            var askQuestionVM = new AskQuestionVM()
            {
                CategoryList = _unitOfWork.CategoryRepository.GetAll().Select( cl => new SelectListItem()
                {
                    Text = cl.CategoryName,
                    Value = cl.Id.ToString(),
                })
            };
            return View(askQuestionVM);
        }

        [HttpPost]
        public IActionResult Ask(AskQuestionVM askQuestionVM)
        {
            if (!ModelState.IsValid)
            {
                askQuestionVM.CategoryList = _unitOfWork.CategoryRepository.GetAll().Select(cl => new SelectListItem()
                {
                    Text= cl.CategoryName,
                    Value = cl.Id.ToString(),
                });
                return View(askQuestionVM);
            }

            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            Question question = new Question()
            {
                Id = Guid.NewGuid(),
                Description = askQuestionVM.Description,
                Title = askQuestionVM.Title,
                CategoryId = askQuestionVM.CategoryId,
                UserId = Guid.Parse(userId),
                AnswerCount = 0,
                ViewsCount = 0,
                VotesCount = 0,
                PostedDateAndTime = DateTime.UtcNow,
                ModifiedDateAndTime = DateTime.UtcNow
            };
            _unitOfWork.QuestionRepository.Add(question);
            _unitOfWork.Save();
            return RedirectToAction(nameof(HomeController.Index), "Home", new { area = "Users" });
        }







        #region API CALLS

        [HttpPost]
        public IActionResult UploadImage(IFormFile? upload)
        {
            string webRootPath = _webHostEnvironment.WebRootPath;
            if (upload != null)
            {
                if(upload.Length > 1 * 1024 * 1024)
                {
                    return Json(new {error = new {message = "The image upload failed because the image was too big (max 1MB)." } });
                }

                string imagePath = Path.Combine(webRootPath, @"images");
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(upload.FileName);

                //checking if the same name file exists
                while (System.IO.File.Exists(Path.Combine(imagePath, fileName)))
                {
                    fileName = Guid.NewGuid().ToString() + Path.GetExtension(upload.FileName);
                }
                string fullPath = Path.Combine(imagePath, fileName);
                //creating the file
                using (var fileStream = new FileStream(fullPath, FileMode.Create))
                {
                    upload.CopyTo(fileStream);
                }

                //checking if file is created 
                if(System.IO.File.Exists(fullPath))
                {
                    return Json(new {url = "/images/" + fileName});
                }
                else
                {
                    return Json(new { error = new { message = "Image upload failed, please try again." } });
                }
            }
            else
            {
                return Json(new { error = new { message = "Image upload failed, please try again." } });
            }
        }

        [HttpPost]
        public IActionResult DeleteImage(string imagePath)
        {
            string webRootPath = _webHostEnvironment.WebRootPath;
            if (!imagePath.IsNullOrEmpty())
            {
                string fullPath = Path.Combine(webRootPath, imagePath.TrimStart('/'));
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }
                return Json(new {success = true});
            }
            else
            {
                return Json(new {success = false});
            }
        }

        [HttpPost]
        public IActionResult DeleteUnsavedImages([FromBody]UnsavedImagesListVM unsavedImagesListVM)
        {
            string webRootPath = _webHostEnvironment.WebRootPath;
            foreach(string imagePath in unsavedImagesListVM.unsavedImages)
            {
                string fullPath = Path.Combine(webRootPath, imagePath.TrimStart('/'));
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }
            }
            return Ok();
        }

        #endregion
    }

}
