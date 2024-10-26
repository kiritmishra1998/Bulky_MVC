using Bulky_Web_Razor_Pages.Data;
using Bulky_Web_Razor_Pages.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bulky_Web_Razor_Pages.Pages.Categories
{
    [BindProperties]
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public Category Category { get; set; }
        public EditModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public void OnGet(int? id)
        {
            if (id != null || id != 0)
            {
                Category = _db.Categories.Find(id);
            }
            if (id == null || id == 0)
                RedirectToPage("NotFound");
        }
        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                _db.Categories.Update(Category);
                _db.SaveChanges();
                TempData["success"] = "Category Updated Successfuly";
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}
