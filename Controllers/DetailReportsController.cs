using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using test.Models;

namespace test.Controllers
{
    [Authorize(Roles = "Admin,Manager")]
    public class DetailReportsController : Controller
    {
        private readonly AppDataDbContext _context;

        public DetailReportsController(AppDataDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var appDataDbContext = _context.DetailReports.Include(d => d.BookType).Include(d => d.Report);
            ViewBag.ReportData = (from dr in _context.DetailReports
                                  join r in _context.Reports on dr.IdReport equals r.IdReport
                                 select new { Time = r.To, Data = dr.TotalRevenue}).ToList();
            
            return View(await appDataDbContext.ToListAsync());
        }

        public IActionResult Create(string id="")
        {
            ViewData["IdBookType"] = new SelectList(_context.BookTypes, "IdBookType", "IdBookType");
            ViewData["IdReport"] = new SelectList(_context.Reports, "IdReport", "IdReport");
            if (id == null)
                return View(new DetailReport());
            return View(_context.DetailReports.Find(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdDR,IdReport,IdBookType,OpenedBooks,ClosedBooks,TotalBooks,TotalRevenue,TotalExpense")] DetailReport detailReport)
        {
            if (ModelState.IsValid)
            {
                if (detailReport.IdDR == null)
                {
                    detailReport.IdDR = "DR" + DateTime.Now.ToString("yyMMdd") + detailReport.IdReport;
                    _context.Add(detailReport);
                }
                else
                    _context.Update(detailReport);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdBookType"] = new SelectList(_context.BookTypes, "IdBookType", "IdBookType", detailReport.IdBookType);
            ViewData["IdReport"] = new SelectList(_context.Reports, "IdReport", "IdReport", detailReport.IdReport);
            return View(detailReport);
        }

        public async Task<IActionResult> Delete(string id)
        {
            var detailReport = await _context.DetailReports.FindAsync(id);
            _context.DetailReports.Remove(detailReport);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
