using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using test.Models;
using static test.Helper;

namespace test.Controllers
{
    [Authorize(Roles = "Admin,Manager")]
    public class ReportsController : Controller
    {
        private readonly AppDataDbContext _context;

        public ReportsController(AppDataDbContext context)
        {
            _context = context;
        }
        public IActionResult GetReports()
        {
            var serialized = JsonConvert.SerializeObject(_context.Reports.ToList());
            return Content(serialized);
            //var json = JsonConvert.SerializeObject(_context.Reports.ToList(), new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            //return Content(json);
        }
        // GET: Reports
        public IActionResult Index()
        {
            return View(_context.Reports.ToList());
        }



        // GET: Reports/Create
        [NoDirectAccess]
        public IActionResult Update(string id = "")
        {
            if (id == "")
                return View(new Report());
            return View(_context.Reports.Find(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update([Bind("IdReport,From,To")] Report report)
        {
            if (ModelState.IsValid)
            {
                if (report.IdReport == null)
                {
                    report.IdReport = "R" + report.From.ToString("yyMMdd") + report.To.ToString("yyMMdd");
                    _context.Add(report);
                }
                else
                    _context.Update(report);

                await _context.SaveChangesAsync();
                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ReportList", _context.Reports.ToList()) });
            }
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "Update", report) });
        }

        public async Task<IActionResult> Delete(string id)
        {
            var report = await _context.Reports.FindAsync(id);//gan customer muon xoa tu id ben razor dua vao day
            _context.Reports.Remove(report);
            await _context.SaveChangesAsync();
            return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ReportList", _context.Reports.ToList()) });
        }
    }
}
