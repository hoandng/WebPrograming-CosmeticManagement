using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyBanMyPham.Data;
using QuanLyBanMyPham.Models;

namespace QuanLyBanMyPham.ViewComponents
{
    public class ProductViewComponent : ViewComponent
    {
        private readonly QuanLyBanMyPhamContext _context;

        public ProductViewComponent(QuanLyBanMyPhamContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(int? categoryId)
        {
            var products = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Supplier)
                .ToListAsync();

            if (categoryId.HasValue)
            {
                products = products.Where(p => p.CategoryId == categoryId.Value).ToList();
            }

            return View("RenderProduct",products);
        }
    }
}
