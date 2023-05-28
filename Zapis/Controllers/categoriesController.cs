using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Zapis.Models;
using Zapis.Models.Data;

namespace Zapis.Controllers
{
    public class categoriesController : Controller
    {
        private readonly zapisContext _context;
        public categoriesController(zapisContext context)
        {
            _context = context;
        }

        // GET: categories
        public async Task<IActionResult> Index()
        {
              return _context.category != null ? 
                          View(await _context.category.ToListAsync()) :
                          Problem("Entity set 'zapisContext.category'  is null.");
        }



        // GET: categories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoryId,CategoryName,IpAddress,Status")] category category)
        {
            //string myIP = Dns.GetHostByName(Dns.GetHostName()).AddressList[0].ToString();
            

            string ipAddress = HttpContext.Connection.RemoteIpAddress.ToString();
            if (ModelState.IsValid)
            {
                category.IpAddress = ipAddress;
                _context.Add(category);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            return View(category);
        }

        // GET: categories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.category == null)
            {
                return NotFound();
            }

            var category = await _context.category.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CategoryId,CategoryName,IpAddress,Status")] category category)
        {
            string remoteIpAddress = HttpContext.Connection.RemoteIpAddress.ToString();
            if (id != category.CategoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    category.IpAddress = remoteIpAddress;
                    _context.Update(category);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!categoryExists(category.CategoryId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: categories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.category == null)
            {
                return NotFound();
            }

            var category = await _context.category
                .FirstOrDefaultAsync(m => m.CategoryId == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.category == null)
            {
                return Problem("Entity set 'zapisContext.category'  is null.");
            }
            var category = await _context.category.FindAsync(id);
            if (category != null)
            {
                _context.category.Remove(category);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool categoryExists(int id)
        {
          return (_context.category?.Any(e => e.CategoryId == id)).GetValueOrDefault();
        }
    }
}
