using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Zapis.Models;
using Zapis.Models.Data;

namespace Zapis.Controllers
{
    public class appointmentsController : Controller
    {
        private readonly zapisContext _context;

        public appointmentsController(zapisContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var zapisContext = _context.appointment.Include(a => a.category);
            return View(await zapisContext.ToListAsync());
        }
        //public async Task<IActionResult> CanceledIndex()
        //{
        //    var zapisContext = _context.appointment.Include(a => a.category);
        //    return View(await zapisContext.Where(a => a.Status == 2).ToListAsync());
        //}
        //public async Task<IActionResult> CompletedIndex()
        //{
        //    var zapisContext = _context.appointment.Include(a => a.category);
        //    return View(await zapisContext.Where(a=>a.Status==3).ToListAsync());
        //}
        //public async Task<IActionResult> NotComeIndex()
        //{
        //    var zapisContext = _context.appointment.Include(a => a.category);
        //    return View(await zapisContext.Where(a => a.Status == 4).ToListAsync());
        //}

        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.category, "CategoryId", "CategoryName");
            var zapisContext = _context.appointment.ToList();
            ViewData["EmployeeData"] = zapisContext;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AppointmentId,CategoryId,NameSurname,Telefon,DateTime,IpAddress,Status")] appointment appointment)
        {
            string remoteIpAddress = HttpContext.Connection.RemoteIpAddress.ToString();
            if (ModelState.IsValid)
            {
                appointment.IpAddress = remoteIpAddress;
                appointment.Status = 1;
                _context.Add(appointment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Create));
            }
            ViewData["CategoryId"] = new SelectList(_context.category, "CategoryId", "CategoryName", appointment.CategoryId);
            return View(appointment);
        }

        // GET: appointments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.appointment == null)
            {
                return NotFound();
            }

            var appointment = await _context.appointment.FindAsync(id);
            if (appointment == null)
            {
                return NotFound();
            }

            var dictionary = new Dictionary<int, string>
            {
                { 1, "Активный" },
                { 2, "Отменить" },
                { 3, "Завершить" },
                { 4, "Не пришедший" }
            };

            ViewBag.Status = new SelectList(dictionary, "Key", "Value");
            ViewData["Status"] = new List<string> { "Активный", "Отменить","Завершить", "Не пришедший" };
            ViewData["CategoryId"] = new SelectList(_context.category, "CategoryId", "CategoryName", appointment.CategoryId);
            return View(appointment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AppointmentId,CategoryId,NameSurname,Telefon,DateTime,IpAddress,Status")] appointment appointment)
        {
            string remoteIpAddress = HttpContext.Connection.RemoteIpAddress.ToString();
            if (id != appointment.AppointmentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    appointment.IpAddress = remoteIpAddress;
                    _context.Update(appointment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!appointmentExists(appointment.AppointmentId))
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
            ViewData["CategoryId"] = new SelectList(_context.category, "CategoryId", "CategoryName", appointment.CategoryId);
            return View(appointment);
        }

        private bool appointmentExists(int id)
        {
          return (_context.appointment?.Any(e => e.AppointmentId == id)).GetValueOrDefault();
        }
    }
}
