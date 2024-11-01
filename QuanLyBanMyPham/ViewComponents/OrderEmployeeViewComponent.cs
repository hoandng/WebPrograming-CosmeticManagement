using Microsoft.AspNetCore.Mvc;
using QuanLyBanMyPham.Data;
using QuanLyBanMyPham.Models;
namespace QuanLyBanMyPham.ViewComponents
{
    public class OrderEmployeeViewComponent : ViewComponent
    {
        QuanLyBanMyPhamContext db;
        List<Order> orders;
        public OrderEmployeeViewComponent(QuanLyBanMyPhamContext _context)
        {
            db = _context;
            orders = db.Orders.ToList();
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {


            return View("RenderOrderEmployee", orders);
        }

    }
}
