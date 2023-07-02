using BulkyBookWeb.Data;
using BulkyBookWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly BulkyDbContext _dbContext;

        public CategoryController(BulkyDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        // Display list of categories
        public IActionResult Index()
        {
            IEnumerable<Category> categories = _dbContext.Categories.ToList();
            return View(categories);
        }
        // Get
        public IActionResult Create()
        {
            return View();
        }
        // Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Categories.Add(category);
                _dbContext.SaveChanges();
                TempData["success"] = "Category added successfully.";
                return RedirectToAction("Index");
            }
            TempData["error"] = "Category is not added.";
            return View(category);
        }
        // Get
        public IActionResult Edit(int? id)
        {
            if(id == null)
            {
                TempData["error"] = "Not a valid Id.";
                return NotFound();
            }
            Category category = _dbContext.Categories.Find(id);
            if(category == null)
            {
                TempData["error"] = "Category is not found.";
                return NotFound();
            }
            return View(category);
        }
        // Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Categories.Update(category);
                _dbContext.SaveChanges();
                TempData["success"] = "Category updated successfully.";
                return RedirectToAction("Index");
            }
            TempData["error"] = "Category is not updated.";
            return View(category);
        }
        public IActionResult Delete(int id)
        {
            var category = _dbContext.Categories.Find(id);
            if(category == null)
            {
                TempData["error"] = "Category is not found.";
                return NotFound();
            }
            _dbContext.Categories.Remove(category);
            _dbContext.SaveChanges();
            TempData["success"] = "Category deleted successfully.";
            return RedirectToAction("Index");
        }
    }
}
