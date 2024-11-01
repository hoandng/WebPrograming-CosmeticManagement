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
                    HttpContext.Session.SetString("CurrentUsername", user.Username);

                    if (user.Username.Contains("admin"))
                    {
                        return RedirectToAction("Admin", "Menu");
                    }
                    else if (user.Username.Contains("nhanvien"))
                    {
                        return RedirectToAction("IndexEmployee", "Supplier");
                    }
                    else if (user.Username.Contains("khachhang"))
                    {
                        return RedirectToAction("IndexCustomer", "Product");
                    }
                }
                else
                {
                    ViewBag.ErrorMessage = "Tên người dùng hoặc mật khẩu không chính xác.";
                }
            }
            return View(model);
        }
    }
}
