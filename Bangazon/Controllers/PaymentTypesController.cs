using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bangazon.Data;
using Bangazon.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bangazon.Controllers
{
    public class PaymentTypesController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public PaymentTypesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        // GET: PaymentTypes
        public async Task<ActionResult> Index()
        {
            var user = await GetUserAsync();
            var paymentTypes = await _context.PaymentType
                .Where(pt => pt.UserId == user.Id)
                .ToListAsync();
            return View(paymentTypes);
        }

        // GET: PaymentTypes/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PaymentTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PaymentTypes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(PaymentType paymentTypeModel)
        {
            try
            {
                var user = await GetUserAsync();
                var paymentType = new PaymentType()
                {
                    UserId = user.Id,
                    Description = paymentTypeModel.Description,
                    AccountNumber = paymentTypeModel.AccountNumber
                };
                _context.PaymentType.Add(paymentType);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        // GET: PaymentTypes/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PaymentTypes/Edit/5
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

        // GET: PaymentTypes/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var paymentType = await _context.PaymentType.FirstOrDefaultAsync(pt => pt.PaymentTypeId == id);

            var user = await GetUserAsync();

            if (paymentType.UserId != user.Id)
            {
                return NotFound();
            }

            return View(paymentType);
        }

        // POST: PaymentTypes/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, PaymentType paymentType)
        {
            try
            {
                _context.PaymentType.Remove(paymentType);
                await _context.SaveChangesAsync();

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