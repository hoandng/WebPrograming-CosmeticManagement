using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyBanMyPham.Data;
using QuanLyBanMyPham.Models;

namespace QuanLyBanMyPham.Controllers
{
    public class LoginController : Controller
    {
        private QuanLyBanMyPhamContext db;
    

        public LoginController(QuanLyBanMyPhamContext context)
        {
            db = context;
        }
        public ActionResult Index(User model)
        {
            HttpContext.Session.Clear();
            if (ModelState.IsValid)
            { 
                var user = db.Users
                    .FirstOrDefault(u => u.Username == model.Username && u.Password == model.Password);


                if (user != null)
                {
                    if (user != null)
                    {
                        HttpContext.Session.SetString("CurrentUsername", user.Username);

                        if (user.Username.Contains("admin"))
                        {
                            return RedirectToAction("Admin", "Menu");
                        }
                        else if (user.Username.Contains("nhanvien"))
                        {
                            return RedirectToAction("IndexEmployee", "Supplier");
                        }
                        else
                        {
                            return RedirectToAction("IndexCustomer", "Product");
                        }
                    }
                }
                ModelState.AddModelError(string.Empty, "Tên đăng nhập hoặc mật khẩu không đúng.");
            }
            return View(model);
        }
    }
}
