using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using VanisBook.DataAccess.Data;
using VanisBook.DataAccess.Repository.IRepository;
using VanisBook.Models;
using VanisBook.Models.ViewModels;
using VanisBook.Utility;

namespace VanisBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles = SD.Role_Admin)]
    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CompanyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<Company> objCompanyList = _unitOfWork.Company.GetAll().ToList();
           
       

            return View(objCompanyList);
        }

        // UpdateInsert
        public IActionResult Upsert(int? id)
        {
     

            if(id == null || id == 0)
            {
                // Create
            return View(new Company());
            }else
            {
                // Update
                Company companyObj = _unitOfWork.Company.Get(u => u.Id == id);
                return View(companyObj);
            }


        }
        [HttpPost]
        public IActionResult Upsert(Company CompanyObj)
        {
            if (ModelState.IsValid)
            {
               

                if(CompanyObj.Id == 0)
                {
                    _unitOfWork.Company.Add(CompanyObj);
                }else
                {
                    _unitOfWork.Company.Update(CompanyObj);
                }

                _unitOfWork.Save();
                TempData["success"] = "Company updated successfully";
                return RedirectToAction("Index", "Company");
            }else
            {
                
            return View(CompanyObj);
            }
        }
        //public IActionResult Edit(int? id)
        //{
        //    if (id == null || id == 0)
        //    {
        //        return NotFound();
        //    }
        //    Company? getCompany = _unitOfWork.Company.Get(c => c.Id == id);
        //    //Company getCompany2 = _db.Categories.FirstOrDefault(c=>c.Id == id);
        //    //Company getCompany3 = _db.Categories.Where(c=>c.Id==id).FirstOrDefault();

        //    if (getCompany == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(getCompany);
        //}
        //[HttpPost]
        //public IActionResult Edit(Company obj)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _unitOfWork.Company.Update(obj);
        //        _unitOfWork.Save();
        //        TempData["success"] = "Company updated successfully";
        //        return RedirectToAction("Index", "Company");
        //    }
        //    return View();
        //}
        //public IActionResult Delete(int? id)
        //{
        //    if (id == null || id == 0)
        //    {
        //        return NotFound();
        //    }
        //    Company getCompany = _unitOfWork.Company.Get(c => c.Id == id);

        //    if (getCompany == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(getCompany);
        //}
        //[HttpPost, ActionName("Delete")]

        //public IActionResult DeletePOST(int? id)
        //{
        //    Company obj = _unitOfWork.Company.Get(c => c.Id == id);
        //    if (obj == null)
        //    {
        //        return NotFound();
        //    }
        //    _unitOfWork.Company.Remove(obj);
        //    _unitOfWork.Save();
        //    TempData["success"] = "Company deleted successfully";
        //    return RedirectToAction("Index", "Company");
        //}
        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Company> objCompanyList = _unitOfWork.Company.GetAll().ToList();
            return Json(new { data = objCompanyList });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var CompanyToBeDeleted = _unitOfWork.Company.Get(u => u.Id == id);
            if (CompanyToBeDeleted == null)
            { 
                return Json(new { success = false, message = "Error while deleting"}); 
            }

            _unitOfWork.Company.Remove(CompanyToBeDeleted);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Delete Successful" });
        }
        #endregion
    }
}
