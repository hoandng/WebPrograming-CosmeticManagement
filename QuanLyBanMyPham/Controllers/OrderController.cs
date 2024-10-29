using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using QuanLyBanMyPham.Data;
using QuanLyBanMyPham.Models;
using System.Collections.Immutable;
using System.Security.Claims;

namespace QuanLyBanMyPham.Controllers
{
    public class OrderController : Controller
    {
        private QuanLyBanMyPhamContext db;
        public OrderController(QuanLyBanMyPhamContext context)
        {
            db = context;
        }
        public ActionResult Index(string mid)
        {

            var orders = db.Orders
                .Include(o => o.User) 
                .Include(o => o.OrderDetails) 
                .ToList();
            if (!string.IsNullOrEmpty(mid))
            {
                orders = orders.Where(l => l.Status == mid).ToList();
            }


            return View(orders);
        }



        public ActionResult OrdersByStatus(string mid)

        {

            var orders = db.Orders.Where(l => l.Status ==mid).ToList();
            return PartialView("OrderTable", orders);
        }
        public ActionResult OrdersByStatusEmployee(string mid)

        {

            var orders = db.Orders.Where(l => l.Status == mid).ToList();
            return PartialView("OrderTableEmployee", orders);
        }


        public ActionResult IndexEmployee()
        {
            var orders = db.Orders
                .Include(o => o.User)
                .Include(o => o.OrderDetails)
                .ToList();

            return View(orders);
        }
        public ActionResult IndexCustomer()
        {
            var currentUsername = HttpContext.Session.GetString("CurrentUsername");
            if (string.IsNullOrEmpty(currentUsername))
            {
                return RedirectToAction("Index", "Login");
            }

            var orders = db.Orders
                .Include(o => o.User)
                .Where(o => o.User.Username == currentUsername)
                .ToList();

            return View(orders);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = db.Orders
                .Include(o => o.User) 
                .Include(o => o.OrderDetails)
                .FirstOrDefault(o => o.OrderId == id);

            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var order = db.Orders
                .Include(o => o.OrderDetails)
                .FirstOrDefault(o => o.OrderId == id);

            if (order != null)
            {

                foreach (var detail in order.OrderDetails.ToList())
                {
                    db.OrderDetails.Remove(detail);
                }


                db.Orders.Remove(order);
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

            var order = db.Orders
                .Include(o => o.User)
                .Include(o => o.OrderDetails)
                .FirstOrDefault(o => o.OrderId == id);

            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }
        [HttpPost, ActionName("DeleteEmployee")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteEmployeeConfirmed(int id)
        {
            var order = db.Orders
                .Include(o => o.OrderDetails)
                .FirstOrDefault(o => o.OrderId == id);

            if (order != null)
            {

                foreach (var detail in order.OrderDetails.ToList())
                {
                    db.OrderDetails.Remove(detail);
                }


                db.Orders.Remove(order);
                db.SaveChanges();
            }

            return RedirectToAction(nameof(IndexEmployee));
        }
        public IActionResult DeleteCustomer(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = db.Orders
                .Include(o => o.User)
                .Include(o => o.OrderDetails)
                .FirstOrDefault(o => o.OrderId == id);

            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }
        [HttpPost, ActionName("DeleteCustomer")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteCustomerConfirmed(int id)
        {
            var order = db.Orders
                .Include(o => o.OrderDetails)
                .FirstOrDefault(o => o.OrderId == id);

            if (order != null)
            {

                foreach (var detail in order.OrderDetails.ToList())
                {
                    db.OrderDetails.Remove(detail);
                }


                db.Orders.Remove(order);
                db.SaveChanges();
            }

            return RedirectToAction(nameof(IndexCustomer));
        }

        public IActionResult Detail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

           
            var order = db.Orders
                .Include(o => o.User)
                .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Product)
                .FirstOrDefault(o => o.OrderId == id);

            if (order == null)
            {
                return NotFound();
            }

            return View(order); 
        }
        public IActionResult DetailEmployee(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var order = db.Orders
                .Include(o => o.User)
                .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Product)
                .FirstOrDefault(o => o.OrderId == id);

            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }
       
        public IActionResult DetailCustomer(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var order = db.Orders
                .Include(o => o.User)
                .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Product)
                .FirstOrDefault(o => o.OrderId == id);

            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ConfirmOrder(int orderId)
        {
            var order = db.Orders
                .Include(o => o.OrderDetails) 
                .ThenInclude(od => od.Product) 
                .FirstOrDefault(o => o.OrderId == orderId);

            if (order != null && order.Status == "Chờ xử lý")
            {
          
                decimal totalAmount = 0;

               
                foreach (var detail in order.OrderDetails)
                {
                    totalAmount += detail.Price * detail.Quantity; 
                }

             
                order.TotalAmount = totalAmount;

             
                order.Status = "Hoàn thành";

           
                foreach (var detail in order.OrderDetails)
                {
               
                    var product = db.Products.FirstOrDefault(p => p.ProductId == detail.ProductId);
                    if (product != null && product.Quantity >= detail.Quantity) 
                    {
                        product.Quantity -= detail.Quantity; 
                    }
                    else
                    {
                        
                        ModelState.AddModelError("", $"Không đủ hàng tồn kho cho sản phẩm {product?.ProductName}");
                        return View("Error"); 
                    }
                }

                db.SaveChanges(); 
            }

            return RedirectToAction(nameof(IndexEmployee)); 
        }

       





    }
}

