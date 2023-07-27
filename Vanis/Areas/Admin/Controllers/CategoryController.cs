using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VanisBook.DataAccess.Data;
using VanisBook.DataAccess.Repository.IRepository;
using VanisBook.Models;
using VanisBook.Utility;

namespace VanisBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<Category> objCategoryList = _unitOfWork.Category.GetAll().ToList();
            return View(objCategoryList);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The DisplayOrder cannot match the name");
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index", "Category");
            }
            return View();
        }
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? getCategory = _unitOfWork.Category.Get(c => c.Id == id);
            //Product getProduct2 = _db.Categories.FirstOrDefault(c=>c.Id == id);
            //Product getProduct3 = _db.Categories.Where(c=>c.Id==id).FirstOrDefault();

            if (getCategory == null)
            {
                return NotFound();
            }
            return View(getCategory);
        }
        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Category updated successfully";
                return RedirectToAction("Index", "Category");
            }
            return View();
        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category getCategory = _unitOfWork.Category.Get(c => c.Id == id);

            if (getCategory == null)
            {
                return NotFound();
            }
            return View(getCategory);
        }
        [HttpPost, ActionName("Delete")]

        public IActionResult DeletePOST(int? id)
        {
            Category obj = _unitOfWork.Category.Get(c => c.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            _unitOfWork.Category.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index", "Category");
        }

    }
}
