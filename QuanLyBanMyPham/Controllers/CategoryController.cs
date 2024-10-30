using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyBanMyPham.Data;
using QuanLyBanMyPham.Models;

namespace QuanLyBanMyPham.Controllers
{
    public class CategoryController : Controller
    {
        private QuanLyBanMyPhamContext db;
        public CategoryController(QuanLyBanMyPhamContext context)
        {
            db = context;
        }
        public IActionResult Index()
        {
            var category = db.Categories.ToList();

            return View(category);
        }
        public IActionResult IndexEmployee()
        {
            var category = db.Categories.ToList();

            return View(category);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("CategoryName")] Category category)
        {
            if (ModelState.IsValid)
            {
                int maxCategoryId = db.Categories.Max(u => u.CategoryId);
                category.CategoryId = maxCategoryId + 1;
                db.Categories.Add(category);
                db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        public IActionResult CreateEmployee()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateEmployee([Bind("CategoryName")] Category category)
        {
            if (ModelState.IsValid)
            {
                int maxCategoryId = db.Categories.Max(u => u.CategoryId);
                category.CategoryId = maxCategoryId + 1;
                db.Categories.Add(category);
                db.SaveChanges();
                return RedirectToAction(nameof(IndexEmployee));
            }
            return View();
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = db.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("CategoryId,CategoryName")] Category category)
        {
            if (id != category.CategoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(category);
                    db.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again.");
                }
            }

            return View(category);
        }
        public IActionResult EditEmployee(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = db.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditEmployee(int id, [Bind("CategoryId,CategoryName")] Category category)
        {
            if (id != category.CategoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(category);
                    db.SaveChanges();
                    return RedirectToAction(nameof(IndexEmployee));
                }
                catch (DbUpdateConcurrencyException)
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again.");
                }
            }

            return View(category);
        }

        public IActionResult Delete(int id)
        {
            if (db.Categories == null)
            {
                return NotFound();
            }

            var category = db.Categories.Include(c => c.Products).FirstOrDefault(c => c.CategoryId == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var category = db.Categories.Include(c => c.Products).FirstOrDefault(c => c.CategoryId == id);
            if (category != null)
            {
                foreach (var product in category.Products)
                {
                    product.CategoryId = null; 
                }

                db.Categories.Remove(category);
                db.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }
        public IActionResult DeleteEmployee(int id)
        {
            if (db.Categories == null)
            {
                return NotFound();
            }

            var category = db.Categories.Include(c => c.Products).FirstOrDefault(c => c.CategoryId == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost, ActionName("DeleteEmployee")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteEmployeeConfirmed(int id)
        {
            var category = db.Categories.Include(c => c.Products).FirstOrDefault(c => c.CategoryId == id);
            if (category != null)
            {
                foreach (var product in category.Products)
                {
                    product.CategoryId = null;
                }

                db.Categories.Remove(category);
                db.SaveChanges();
            }

            return RedirectToAction(nameof(IndexEmployee));
        }



    }
}
