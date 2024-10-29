using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyBanMyPham.Data;

namespace QuanLyBanMyPham.Controllers
{
    public class MenuController : Controller
    {
        private QuanLyBanMyPhamContext db;
        public MenuController(QuanLyBanMyPhamContext context)
        {
            db = context;
        }
        public IActionResult Admin()
        {

            int totalOrders = db.Orders.Count();

         
            int totalEmployees = db.Users.Count(u => u.RoleId == 2);

          
            int totalCustomers = db.Users.Count(u => u.RoleId == 3);

            int completedOrders = db.Orders.Count(o => o.Status == "Hoàn thành");
            int pendingOrders = db.Orders.Count(o => o.Status == "Chờ xử lý");
            
            int canceledOrders = db.Orders.Count(o => o.Status == "Hủy");


            int totalProducts = db.Products.Count();

          
            int totalSuppliers = db.Suppliers.Count();

            
            var today = DateOnly.FromDateTime(DateTime.Now);
            var totalRevenueToday = db.Orders
                .Where(o => o.OrderDate.HasValue &&
                            o.OrderDate.Value.Year == today.Year &&
                            o.OrderDate.Value.Month == today.Month &&
                            o.OrderDate.Value.Day == today.Day)
                .Sum(o => o.TotalAmount);

            var totalRevenueThisMonth = db.Orders
                .Where(o => o.OrderDate.HasValue &&
                            o.OrderDate.Value.Year == today.Year &&
                            o.OrderDate.Value.Month == today.Month)
                .Sum(o => o.TotalAmount);

            var totalRevenueThisYear = db.Orders
                .Where(o => o.OrderDate.HasValue &&
                            o.OrderDate.Value.Year == today.Year)
                .Sum(o => o.TotalAmount);

            ViewBag.TotalOrders = totalOrders;
            ViewBag.CanceledOrders = canceledOrders;
            ViewBag.TotalProducts = totalProducts;
            ViewBag.TotalSuppliers = totalSuppliers;
            ViewBag.TotalEmployees = totalEmployees;
            ViewBag.TotalCustomers = totalCustomers;
            ViewBag.CompletedOrders = completedOrders;
            ViewBag.PendingOrders = pendingOrders;
            ViewBag.TotalRevenueToday = totalRevenueToday;
            ViewBag.TotalRevenueThisMonth = totalRevenueThisMonth;
            ViewBag.TotalRevenueThisYear = totalRevenueThisYear;

            return View();
        }



    }
}
