using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Mulakat_Takip.Models;

namespace Mulakat_Takip.Database
{
    public class UsersController : Controller
    {
        private readonly DatabaseContext _context;

        public UsersController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: Users/Create
        public IActionResult Login()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UsersLogin G_users)
        {
            var P_obj = _context.Users.Where(a => a.UserName == G_users.UserName && a.UserPassword == G_users.UserPassword).FirstOrDefault();
            if (P_obj != null)
            {
                if (P_obj.UserAuthorization)
                {
                    return RedirectToAction("Index", "AdminPanel"/*, new { @id = P_obj.UserId }*/);
                }
                return RedirectToAction("Index", "UserPanel", new { @id = P_obj.UserId });
            }
            else
            {
                ModelState.AddModelError("", "Kullanıcı adı veya şifreyi yanlış girdiniz.");
            }
            return View(G_users);
        }

        // GET: Users/Create
        public IActionResult Register()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(Users G_users)
        {
            if (ModelState.IsValid)
            {
                _context.Add(G_users);
                await _context.SaveChangesAsync();
                return RedirectToAction("Login", "Users");
            }
            return View(G_users);
        }
        private bool UsersExists(int id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }
    }
}
