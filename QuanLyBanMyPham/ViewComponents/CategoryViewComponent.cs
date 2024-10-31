using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyBanMyPham.Data;
using QuanLyBanMyPham.Models;

namespace QuanLyBanMyPham.ViewComponents
{
    public class CategoryViewComponent : ViewComponent
    {
        private readonly QuanLyBanMyPhamContext _context;

        public CategoryViewComponent(QuanLyBanMyPhamContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            
            var categories = await _context.Categories
                .Include(c => c.Products) 
                .ToListAsync();

            return View("RenderCategory", categories); 
        }
    }
}
