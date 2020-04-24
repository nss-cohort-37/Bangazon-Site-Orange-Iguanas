using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bangazon.Data;
using Bangazon.Models;
using Bangazon.Models.ProductViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Bangazon.Controllers
{

   
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ProductsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        // GET: Products
        public async Task<ActionResult> Index()
        {
            var user = await GetUserAsync();
            var products = await _context.Product
                 .Where(p => p.UserId == user.Id)
                 .Include(p => p.ProductType)
                  .ToListAsync();
            return View(products);
        }

        // GET: Products/Details/5
        public ActionResult Details(int id)
        {
          
            return View();
        }

        // GET: Products/Create
        public async Task<ActionResult> Create()
        {
            var viewModel = new ProductFormViewModel();

            var productTypeOptions = await _context.ProductType.Select(g => new SelectListItem()
            {
                Text = g.Label,
                Value = g.ProductTypeId.ToString()
            }).ToListAsync();

            viewModel.ProductTypeOptions = productTypeOptions;
            return View(viewModel);
        }

        // POST: Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ProductFormViewModel productViewModel)
        {
            try
            {
                var user = await GetUserAsync();
                var product = new Product()
                {
                    DateCreated = DateTime.Now,
                    ProductId = productViewModel.ProductId,
                    Description = productViewModel.Description,
                    Title = productViewModel.Title,
                    Price = productViewModel.Price,
                    Quantity = productViewModel.Quantity,
                    UserId = user.Id,
                    City = productViewModel.City,
                    ImagePath = productViewModel.ImagePath,
                    Active = productViewModel.Active,
                    ProductTypeId = productViewModel.ProductTypeId
                };

                _context.Product.Add(product);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Products/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var product = await _context.Product.Include(p => p.ProductType).FirstOrDefaultAsync(p => p.ProductId == id);
            var user = await GetUserAsync();

            if (product.UserId != user.Id)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, Product product)
        {
            try
            {
                _context.Product.Remove(product);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return View();
            }
        }
        private async Task<ApplicationUser> GetUserAsync() => await _userManager.GetUserAsync(HttpContext.User);
    }
}