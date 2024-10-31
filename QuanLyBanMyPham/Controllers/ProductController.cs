using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuanLyBanMyPham.Data;
using QuanLyBanMyPham.Models;

namespace QuanLyBanMyPham.Controllers
{

    public class ProductController : Controller
    {
        private QuanLyBanMyPhamContext db;
        public ProductController(QuanLyBanMyPhamContext context)
        {
            db = context;
        }
        public ActionResult Index(int? categoryId)
        {
            var productsQuery = db.Products.Include(p => p.Category).Include(c => c.Supplier).AsQueryable();

            if (categoryId.HasValue)
            {
                productsQuery = productsQuery.Where(p => p.CategoryId == categoryId.Value);
            }

            var products = productsQuery.ToList(); 
            ViewBag.Categories = db.Categories.ToList(); 
            ViewBag.Suppliers = db.Suppliers.ToList();

            return View(products); 
        }

        public IActionResult ProductByCategoryId(int categoryId)
        {
            var products = db.Products
                .Include(p => p.Category)
                .Include(p => p.Supplier)
                .Where(p => p.CategoryId == categoryId)
                .ToList();

            return PartialView("ProducTable", products); 
        }
        public IActionResult ProductByCategoryIdEmployee(int categoryId)
        {
            var products = db.Products
                .Include(p => p.Category)
                .Include(p => p.Supplier)
                .Where(p => p.CategoryId == categoryId)
                .ToList();

            return PartialView("ProducTableEmployee", products);
        }
        public IActionResult ProductByCategoryIdCustomer(int categoryId)
        {
            var products = db.Products
                .Include(p => p.Category)
                .Include(p => p.Supplier)
                .Where(p => p.CategoryId == categoryId)
                .ToList();

            return PartialView("ProducTableCustomer", products);
        }


        public ActionResult IndexEmployee(int? categoryId)
        {
            var productsQuery = db.Products.Include(p => p.Category).Include(c => c.Supplier).AsQueryable();

            if (categoryId.HasValue)
            {
                productsQuery = productsQuery.Where(p => p.CategoryId == categoryId.Value);
            }

            var products = productsQuery.ToList();
            ViewBag.Categories = db.Categories.ToList();
            ViewBag.Suppliers = db.Suppliers.ToList();

            return View(products);
        }

        public ActionResult IndexCustomer(int? categoryId)
        {
            var productsQuery = db.Products.Include(p => p.Category).Include(c => c.Supplier).AsQueryable();

            if (categoryId.HasValue)
            {
                productsQuery = productsQuery.Where(p => p.CategoryId == categoryId.Value);
            }

            var products = productsQuery.ToList();
            ViewBag.Categories = db.Categories.ToList();
            ViewBag.Suppliers = db.Suppliers.ToList();

            return View(products);
        }
 
        public IActionResult Create()
        {
            var categories = db.Categories.Select(c => new SelectListItem
            {
                Text = c.CategoryName,
                Value = c.CategoryId.ToString()
            }).ToList();


            var suppliers = db.Suppliers.Select(s => new SelectListItem
            {
                Text = s.SupplierName,
                Value = s.SupplierId.ToString()
            }).ToList();


            ViewBag.CategoryId = categories;
            ViewBag.SupplierId = suppliers;

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product, IFormFile? imageFile)
        {
            if (ModelState.IsValid)
            {
               
                int maxProductId = db.Products.Any() ? db.Products.Max(u => u.ProductId) : 0;
                product.ProductId = maxProductId + 1;

                if (imageFile != null && imageFile.Length > 0)
                {
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images", imageFile.FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(stream);
                    }
                    product.ImagePath = imageFile.FileName; 
                }
                else
                {
                    product.ImagePath = "default.png"; 
                }

                db.Products.Add(product);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

           
            ViewBag.CategoryId = db.Categories.Select(c => new SelectListItem
            {
                Text = c.CategoryName,
                Value = c.CategoryId.ToString()
            }).ToList();

            ViewBag.SupplierId = db.Suppliers.Select(s => new SelectListItem
            {
                Text = s.SupplierName,
                Value = s.SupplierId.ToString()
            }).ToList();

            return View(product);
        }

        public IActionResult CreateEmployee()
        {
            var categories = db.Categories.Select(c => new SelectListItem
            {
                Text = c.CategoryName,
                Value = c.CategoryId.ToString()
            }).ToList();


            var suppliers = db.Suppliers.Select(s => new SelectListItem
            {
                Text = s.SupplierName,
                Value = s.SupplierId.ToString()
            }).ToList();


            ViewBag.CategoryId = categories;
            ViewBag.SupplierId = suppliers;

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateEmployee(Product product, IFormFile? imageFile)
        {
            if (ModelState.IsValid)
            {
                
                int maxProductId = db.Products.Any() ? db.Products.Max(u => u.ProductId) : 0;
                product.ProductId = maxProductId + 1;

                if (imageFile != null && imageFile.Length > 0)
                {
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images", imageFile.FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(stream);
                    }
                    product.ImagePath = imageFile.FileName; 
                }
                else
                {
                    product.ImagePath = "default.png"; 
                }

                db.Products.Add(product);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(IndexEmployee));
            }

            
            ViewBag.CategoryId = db.Categories.Select(c => new SelectListItem
            {
                Text = c.CategoryName,
                Value = c.CategoryId.ToString()
            }).ToList();

            ViewBag.SupplierId = db.Suppliers.Select(s => new SelectListItem
            {
                Text = s.SupplierName,
                Value = s.SupplierId.ToString()
            }).ToList();

            return View(product);
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,ProductName,Price,Quantity,CategoryId,SupplierId,ImagePath")] Product product, IFormFile? imageFile)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (imageFile != null && imageFile.Length > 0)
                    {
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images", imageFile.FileName);
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await imageFile.CopyToAsync(stream);
                        }
                        product.ImagePath = imageFile.FileName;
                    }
                    else
                    {
                        product.ImagePath = db.Products.AsNoTracking().FirstOrDefault(p => p.ProductId == id)?.ImagePath;
                    }


                    db.Update(product);
                    await db.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    ModelState.AddModelError("", "Không thể lưu thay đổi. Vui lòng thử lại.");
                }
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", product.CategoryId);
            ViewBag.SupplierId = new SelectList(db.Suppliers, "SupplierId", "SupplierName", product.SupplierId);

            return View(product);
        }

        public IActionResult EditEmployee(int? id)
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditEmployee(int id, [Bind("ProductId,ProductName,Price,Quantity,CategoryId,SupplierId,ImagePath")] Product product, IFormFile? imageFile)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (imageFile != null && imageFile.Length > 0)
                    {
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images", imageFile.FileName);
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await imageFile.CopyToAsync(stream);
                        }
                        product.ImagePath = imageFile.FileName;
                    }
                    else
                    {
                        product.ImagePath = db.Products.AsNoTracking().FirstOrDefault(p => p.ProductId == id)?.ImagePath;
                    }


                    db.Update(product);
                    await db.SaveChangesAsync();
                    return RedirectToAction(nameof(IndexEmployee));
                }
                catch (DbUpdateConcurrencyException)
                {
                    ModelState.AddModelError("", "Không thể lưu thay đổi. Vui lòng thử lại.");
                }
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", product.CategoryId);
            ViewBag.SupplierId = new SelectList(db.Suppliers, "SupplierId", "SupplierName", product.SupplierId);

            return View(product);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = db.Products
                .Include(p => p.Category)
                .Include(p => p.Supplier)
                .FirstOrDefault(p => p.ProductId == id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var product = db.Products.Find(id);
            if (product != null)
            {
              
                var orderDetails = db.OrderDetails.Where(od => od.ProductId == id).ToList();
                foreach (var orderDetail in orderDetails)
                {
                    orderDetail.ProductId = null; 
                    db.OrderDetails.Update(orderDetail);
                }

               
                db.Products.Remove(product);
                db.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult DeleteEmployee(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = db.Products
                .Include(p => p.Category)
                .Include(p => p.Supplier)
                .FirstOrDefault(p => p.ProductId == id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpPost, ActionName("DeleteEmployee")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteEmployeeConfirmed(int id)
        {
            var product = db.Products.Find(id);
            if (product != null)
            {
                db.Products.Remove(product);
                db.SaveChanges();
            }
            return RedirectToAction(nameof(IndexEmployee));
        }
        [HttpPost]
        public IActionResult CreateOrder(int productId, int quantity)
        {
            Console.WriteLine($"ProductId: {productId}, Quantity: {quantity}");

            if (productId <= 0 || quantity <= 0)
            {
                return Json(new { success = false, message = "Dữ liệu không hợp lệ." });
            }
            var currentUsername = HttpContext.Session.GetString("CurrentUsername");

            try
            {
                var user = db.Users.FirstOrDefault(u => u.Username == currentUsername);
          
                var order = new Order
                {
                    OrderId = db.Orders.Max(o => o.OrderId) + 1, 
                    UserId = user.UserId,
                    OrderDate = DateOnly.FromDateTime(DateTime.Now),
                    Status = "Chờ xử lý",
                    TotalAmount = 0,
                    OrderDetails = new List<OrderDetail>() 
                };

                var product = db.Products.Find(productId);
                if (product == null)
                {
                    return Json(new { success = false, message = "Sản phẩm không tồn tại." });
                }

                var orderDetail = new OrderDetail
                {
                    OrderDetailId = db.OrderDetails.Max(od => od.OrderDetailId) + 1, 
                    ProductId = productId,
                    Quantity = quantity,
                    Price = product.Price
                };


         
                order.OrderDetails.Add(orderDetail); 

               
                db.Orders.Add(order);
                db.SaveChanges();

                return Json(new
                {
                    success = true,
                    message = "Đặt hàng thành công!",
                    orderId = order.OrderId
                });
            }
            catch (Exception ex)
            {
                var innerExceptionMessage = ex.InnerException?.Message ?? ex.Message;
                return Json(new { success = false, message = "Có lỗi xảy ra: " + innerExceptionMessage });
            }


        }





    }
}