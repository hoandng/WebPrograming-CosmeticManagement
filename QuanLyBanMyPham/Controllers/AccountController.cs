using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuanLyBanMyPham.Data;
using QuanLyBanMyPham.Models;
using System.Linq;

namespace QuanLyBanMyPham.Controllers
{
    public class AccountController : Controller
    {
        private readonly QuanLyBanMyPhamContext db;

        public AccountController(QuanLyBanMyPhamContext context)
        {
            db = context;
        }

        public IActionResult Index()
        {
            var currentUsername = HttpContext.Session.GetString("CurrentUsername");
            if (string.IsNullOrEmpty(currentUsername))
            {
                return RedirectToAction("Index", "Login"); 
            }

            var user = db.Users.FirstOrDefault(u => u.Username == currentUsername);
            if (user == null)
            {
                return NotFound("User not found");
            }

            return View(user);
        }

        public IActionResult IndexEmployee()
        {
            var currentUsername = HttpContext.Session.GetString("CurrentUsername");
            if (string.IsNullOrEmpty(currentUsername))
            {
                return RedirectToAction("Index", "Login");
            }

            var user = db.Users.FirstOrDefault(u => u.Username == currentUsername);
            if (user == null)
            {
                return NotFound("User not found");
            }

            return View(user);
        }

        public IActionResult IndexCustomer()
        {
            var currentUsername = HttpContext.Session.GetString("CurrentUsername");
            if (string.IsNullOrEmpty(currentUsername))
            {
                return RedirectToAction("Index", "Login");
            }

            var user = db.Users.FirstOrDefault(u => u.Username == currentUsername);
            if (user == null)
            {
                return NotFound("User not found");
            }

            return View(user);
        }
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = db.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }


            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", product.CategoryId);
            ViewBag.SupplierId = new SelectList(db.Suppliers, "SupplierId", "SupplierName", product.SupplierId);

            return View(product);
        }

        public IActionResult EditUser()
        {
            var currentUsername = HttpContext.Session.GetString("CurrentUsername");
            if (string.IsNullOrEmpty(currentUsername))
            {
                return RedirectToAction("Index", "Login");
            }

            var user = db.Users.FirstOrDefault(u => u.Username == currentUsername);
            if (user == null)
            {
                return NotFound("User not found");
            }

            return View(user);
        }

        [HttpPost,ActionName("EditUser")]
        [ValidateAntiForgeryToken]
        public IActionResult EditUser([Bind("UserId,Username,Password,FullName,Email,Phone")] User user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var currentUser = db.Users.Find(user.UserId);
                    if (currentUser == null)
                    {
                        return NotFound("User not found");
                    }
                    currentUser.FullName = user.FullName;
                    currentUser.Email = user.Email;
                    currentUser.Phone = user.Phone;
                    if (!string.IsNullOrEmpty(user.Password))
                    {
                        currentUser.Password = user.Password;
                    }

                    db.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again.");
                }
            }

            return View(user);
        }
        public IActionResult EditUserEmployee()
        {
            var currentUsername = HttpContext.Session.GetString("CurrentUsername");
            if (string.IsNullOrEmpty(currentUsername))
            {
                return RedirectToAction("Index", "Login");
            }

            var user = db.Users.FirstOrDefault(u => u.Username == currentUsername);
            if (user == null)
            {
                return NotFound("User not found");
            }

            return View(user);
        }

        [HttpPost, ActionName("EditUserEmployee")]
        [ValidateAntiForgeryToken]
        public IActionResult EditUserEmployee([Bind("UserId,Username,Password,FullName,Email,Phone")] User user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var currentUser = db.Users.Find(user.UserId);
                    if (currentUser == null)
                    {
                        return NotFound("User not found");
                    }
                    currentUser.FullName = user.FullName;
                    currentUser.Email = user.Email;
                    currentUser.Phone = user.Phone;
                    if (!string.IsNullOrEmpty(user.Password))
                    {
                        currentUser.Password = user.Password;
                    }
                  

                    db.SaveChanges();
                    return RedirectToAction(nameof(IndexEmployee));
                }
                catch (DbUpdateConcurrencyException)
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again.");
                }
            }

            return View(user);
        }
        public IActionResult EditUserCustomer()
        {
            var currentUsername = HttpContext.Session.GetString("CurrentUsername");
            if (string.IsNullOrEmpty(currentUsername))
            {
                return RedirectToAction("Index", "Login");
            }

            var user = db.Users.FirstOrDefault(u => u.Username == currentUsername);
            if (user == null)
            {
                return NotFound("User not found");
            }

            return View(user);
        }

        [HttpPost, ActionName("EditUserCustomer")]
        [ValidateAntiForgeryToken]
        public IActionResult EditUserCustomer([Bind("UserId,Username,Password,FullName,Email,Phone")] User user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var currentUser = db.Users.Find(user.UserId);
                    if (currentUser == null)
                    {
                        return NotFound("User not found");
                    }
                    currentUser.FullName = user.FullName;
                    currentUser.Email = user.Email;
                    currentUser.Phone = user.Phone;
                    if (!string.IsNullOrEmpty(user.Password))
                    {
                        currentUser.Password = user.Password;
                    }

                    db.SaveChanges();
                    return RedirectToAction(nameof(IndexCustomer));
                }
                catch (DbUpdateConcurrencyException)
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again.");
                }
            }

            return View(user);
        }
    }
}
