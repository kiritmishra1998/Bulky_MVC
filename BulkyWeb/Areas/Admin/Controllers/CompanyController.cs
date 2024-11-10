using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Models.ViewModels;
using BulkyBook.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
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
        public IActionResult Upsert(int? id)
        {
            CompanyViewModels companyVM = new()
            {
               
                Company = new Company()
            };
            if (id == null || id == 0)
            {
                //create
                return View(companyVM);
            }
            else
            {
                //update
                companyVM.Company = _unitOfWork.Company.Get(u => u.Id == id);
                return View(companyVM);
            }
        }
        [HttpPost]
        public IActionResult Upsert(CompanyViewModels companyVM)
        {
            if (ModelState.IsValid)
            {
                
                if (companyVM.Company.Id == 0)
                {
                    _unitOfWork.Company.Add(companyVM.Company);
                }
                else
                {
                    _unitOfWork.Company.Update(companyVM.Company);
                }

                _unitOfWork.Save();
                TempData["success"] = "Company Created Successfuly";
                return RedirectToAction("Index", "Company");
            }
            else
            {
                
                return View(companyVM);
            }
        }
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
            var companyToBeDeleted = _unitOfWork.Company.Get(u => u.Id == id);
            if (companyToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
          

            _unitOfWork.Company.Remove(companyToBeDeleted);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Delete Successful" });
              #endregion
        }
    }

}