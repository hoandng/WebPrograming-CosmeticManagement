using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyBanMyPham.Data;
using QuanLyBanMyPham.Models;

namespace QuanLyBanMyPham.ViewComponents
{
    public class CategoryCustomerViewComponent : ViewComponent
    {
        private readonly QuanLyBanMyPhamContext _context;

        public CategoryCustomerViewComponent(QuanLyBanMyPhamContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            // Lấy danh sách các loại sản phẩm từ cơ sở dữ liệu
            var categories = await _context.Categories
                .Include(c => c.Products) // Nếu bạn muốn bao gồm sản phẩm liên quan
                .ToListAsync();

            return View("RenderCategoryCustomer", categories); // Trả về view với danh sách categories
        }
    }
}
