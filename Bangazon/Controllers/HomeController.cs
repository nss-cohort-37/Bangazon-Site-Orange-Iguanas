using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Bangazon.Models;
using Bangazon.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Bangazon.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<ActionResult> Index(string searchString)
        {
            if(searchString != null) { 
            var products = await _context.Product
                  .Where(p => p.Title.Contains(searchString) && p.Active == true || p.City.Contains(searchString) && p.Active == true)
                  .Include(p => p.ProductType)
                   .ToListAsync();
            return View(products);
            }
            else
            {
                var products = await _context.Product
                    .Where(p => p.Active == true)
                    .Include(p => p.ProductType)
                   .ToListAsync();
                return View(products);
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
