﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuanLyBanMyPham.Data;
using QuanLyBanMyPham.Models;

namespace QuanLyBanMyPham.Controllers
{
    public class EmployeeController : Controller
    {
        private QuanLyBanMyPhamContext db;
        public EmployeeController(QuanLyBanMyPhamContext context)
        {
            db = context;
        }
        public ActionResult Index()
        {
            var employees = db.Users.Where(u => u.RoleId == 2).ToList();

            return View(employees);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Username, Password, FullName, Email, Phone")] User user)
        {
            if (ModelState.IsValid)
            {
  
                bool usernameExists = db.Users.Any(u => u.Username == user.Username);
                if (usernameExists)
                {
                    ModelState.AddModelError("Username", "Tên người dùng đã tồn tại. Vui lòng chọn tên khác.");
                    return View(user);
                }

          
                int maxUserId = db.Users.Max(u => u.UserId);
                user.UserId = maxUserId + 1;
                user.RoleId = 2;

               
                db.Users.Add(user);
                db.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            return View(user); 
        }


        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = db.Users.Find(id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("UserId,Username,Password,FullName,Email,Phone,RoleId")] User user)
        {
            if (id != user.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(user);
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



        public IActionResult Delete(int? id)
        {
            if (id == null || db.Users == null)
            {
                return NotFound();
            }
            var user = db.Users.Include(u => u.Role).FirstOrDefault(u => u.UserId == id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            if (db.Users == null)
            {
                return Problem("Entity set 'Users' is null.");
            }
            var user = db.Users.Find(id);
            if (user != null)
            {
                db.Users.Remove(user);
                db.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Creaate()
        {

            return View();
        }

    }
}   