using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyBanMyPham.Data;
using QuanLyBanMyPham.Models;

namespace QuanLyBanMyPham.Controllers
{
    public class EmployeeController : Controller
    {
        private QuanLyBanMyPhamContext db;
        public EmployeeController(QuanLyBanMyPhamContext context)
        {
            db = context;
        }
        public ActionResult Index()
        {
            var employees = db.Users.Where(u => u.RoleId == 2) .ToList();

            return View(employees);
        }
        public IActionResult Edit(int id)
        {
            var user = db.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(user);  
                    db.SaveChanges(); 
                    return RedirectToAction("Index");  
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Lỗi khi cập nhật: {ex.Message}");
                }
            }
            return View(user);
        }

        public IActionResult Delete(int id)
        {
            var user = db.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);  
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var user = db.Users.Find(id);
            if (user != null)
            {
                db.Users.Remove(user);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

       
    }
}
    



