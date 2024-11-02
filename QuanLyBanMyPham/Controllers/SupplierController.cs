using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyBanMyPham.Data;
using QuanLyBanMyPham.Models;

namespace QuanLyBanMyPham.Controllers
{
    public class SupplierController : Controller
    {
        private QuanLyBanMyPhamContext db;
        public SupplierController(QuanLyBanMyPhamContext context)
        {
            db = context;
        }
        public ActionResult Index()
        {
            var suppliers = db.Suppliers.ToList();

            return View(suppliers);
        }
        public ActionResult IndexEmployee()
        {
            var suppliers = db.Suppliers.ToList();

            return View(suppliers);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("SupplierName, ContactName, Phone, Address, Email")] Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                int maxSupplierId = db.Suppliers.Max(u => u.SupplierId);
                supplier.SupplierId = maxSupplierId + 1;
                db.Suppliers.Add(supplier);
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
        public IActionResult CreateEmployee([Bind("SupplierName, ContactName, Phone, Address, Email")] Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                int maxSupplierId = db.Suppliers.Max(u => u.SupplierId);
                supplier.SupplierId = maxSupplierId + 1;
                db.Suppliers.Add(supplier);
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

            var supplier = db.Suppliers.Find(id);
            if (supplier == null)
            {
                return NotFound();
            }

            return View(supplier);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("SupplierId,SupplierName, ContactName, Phone, Address, Email")] Supplier supplier)
        {
            if (id != supplier.SupplierId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(supplier);
                    db.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again.");
                }
            }

            return View(supplier);
        }
        public IActionResult EditEmployee(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supplier = db.Suppliers.Find(id);
            if (supplier == null)
            {
                return NotFound();
            }

            return View(supplier);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditEmployee(int id, [Bind("SupplierId,SupplierName, ContactName, Phone, Address, Email")] Supplier supplier)
        {
            if (id != supplier.SupplierId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(supplier);
                    db.SaveChanges();
                    return RedirectToAction(nameof(IndexEmployee));
                }
                catch (DbUpdateConcurrencyException)
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again.");
                }
            }

            return View(supplier);
        }



        public IActionResult Delete(int id)
        {
            if (id == null || db.Suppliers == null)
            {
                return NotFound();
            }
            var supplier = db.Suppliers.Include(u => u.Products).FirstOrDefault(u => u.SupplierId == id);
            if (supplier == null)
            {
                return NotFound();
            }
            return View(supplier);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            if (db.Suppliers == null)
            {
                return Problem("Entity set 'Supplier' is null.");
            }

            var supplier = db.Suppliers.Include(s => s.Products).FirstOrDefault(s => s.SupplierId == id);

            if (supplier != null)
            {
            
                foreach (var product in supplier.Products)
                {
                    product.SupplierId = null; 
                }

                db.Suppliers.Remove(supplier);
                db.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult DeleteEmployee(int id)
        {
            if (id == null || db.Suppliers == null)
            {
                return NotFound();
            }
            var supplier = db.Suppliers.Include(u => u.Products).FirstOrDefault(u => u.SupplierId == id);
            if (supplier == null)
            {
                return NotFound();
            }
            return View(supplier);
        }

        [HttpPost, ActionName("DeleteEmployee")]
        [ValidateAntiForgeryToken]
  
        public IActionResult DeleteEmployeeConfirmed(int id)
        {
            if (db.Suppliers == null)
            {
                return Problem("Entity set 'Supplier' is null.");
            }

            var supplier = db.Suppliers.Include(s => s.Products).FirstOrDefault(s => s.SupplierId == id);

            if (supplier != null)
            {

                foreach (var product in supplier.Products)
                {
                    product.SupplierId = null;
                }

                db.Suppliers.Remove(supplier);
                db.SaveChanges();
            }

            return RedirectToAction(nameof(IndexEmployee));
        }

    }
}
