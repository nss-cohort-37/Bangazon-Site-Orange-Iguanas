using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bangazon.Data;
using Bangazon.Models;
using Bangazon.Models.OrderViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Bangazon.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public OrdersController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Orders
        public async Task<ActionResult> Index()
        {
            var user = await GetUserAsync();
            var order = await _context.Order
                .Where(o => o.UserId == user.Id)
                .Include(o => o.OrderProducts)
                .ThenInclude(op => op.Product)
                .ToListAsync();

            return View(order);
        }

        public async Task<ActionResult> Cart()
        {
            var user = await GetUserAsync();

            var viewModel = new CompleteOrderChoosePaymentTypeViewModel();

            var order = await _context.Order
                .Include(o => o.OrderProducts)
                .ThenInclude(op => op.Product)
                .FirstOrDefaultAsync(o => o.UserId == user.Id && o.PaymentTypeId == null);

            var paymentTypeOptions = await _context.PaymentType
                  .Where(pt => pt.UserId == user.Id)
                .Select(p => new SelectListItem()
                {
                    Text = p.Description,
                    Value = p.PaymentTypeId.ToString()
                }).ToListAsync();

            viewModel.Order = order;
            viewModel.ListOfPaymentTypes = paymentTypeOptions;
            return View(viewModel);
        }

        public async Task<ActionResult> ViewOrder(int id)
        {
            var user = await GetUserAsync();

            var viewModel = new ViewOrderViewModel();

            var order = await _context.Order
                .Include(o => o.OrderProducts)
                .ThenInclude(op => op.Product)
                 .FirstOrDefaultAsync(o => o.OrderId == id);

            viewModel.Order = order;


            return View(viewModel);
        }

        // GET: Orders/Create
        public async Task<ActionResult> Create()
        {
            return View();
        }

        // POST: Orders/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> CompleteOrder(CompleteOrderChoosePaymentTypeViewModel viewModel)
        {
            var user = await GetUserAsync();

            var order = await _context.Order.FirstOrDefaultAsync(o => o.OrderId == viewModel.Order.OrderId);

            order.PaymentTypeId = viewModel.Order.PaymentTypeId;
            order.DateCompleted = DateTime.Now;

            _context.Order.Update(order);

            var newOrder = new Order()
            {
                UserId = user.Id
            };

            _context.Order.Add(newOrder);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        // GET: Orders/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Orders/Edit/5
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

        // GET: Orders/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Orders/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        private async Task<ApplicationUser> GetUserAsync() => await _userManager.GetUserAsync(HttpContext.User);
    }
}