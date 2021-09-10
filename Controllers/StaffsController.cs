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
    public class StaffsController : Controller
    {
        private readonly AppDataDbContext _context;

        public StaffsController(AppDataDbContext context)
        {
            _context = context;
        }
        public string IDIncrement(int index)
        {
            if (index < 10)
                return "000" + Math.Abs(index).ToString();
            if (index >= 10 && index < 100)
                return "00" + Math.Abs(index).ToString();
            if (index >= 100 && index < 1000)
                return "0" + Math.Abs(index).ToString();
            return Math.Abs(index).ToString();

        }

        // GET: Staffs
        public async Task<IActionResult> Index()
        {
            var appDataDbContext = _context.Staffs.Include(s => s.Bank);
            return View(await appDataDbContext.ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Index(string text)
        {
            ViewData["GetDetails"] = text;
            var query = from x in _context.Staffs.Include(s => s.Bank)
                        select x;
            if (!String.IsNullOrEmpty(text))
                query = query.Where(x => x.IdBank.Contains(text) || x.IdS.Contains(text) || x.NameS.Contains(text) || x.PositionS.Contains(text) || x.PhoneS.Contains(text) || x.IdS.Contains(text));

            return View(await query.AsNoTracking().ToListAsync());
        }


        public IActionResult Create(string id="")
        {
            ViewData["IdBank"] = new SelectList(_context.Banks, "IdBank", "NameBank");
            if (id == null)               
                return View(new Staff());
            else
                return View(_context.Staffs.Find(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdS,NameS,IdBank,DOBS,IDCardS,IDCardGrantedDayS,PositionS,PhoneS,WorkingStatus,StartWorking,EndWorking")] Staff staff)
        {
            if (ModelState.IsValid)
            {
                if (staff.IdS == null)
                {
                    int i = 1;
                    while(true)
                    {
                        if (_context.Staffs.Contains(_context.Staffs.Find(staff.IdS = "S" + staff.IdBank + IDIncrement(_context.Staffs.Count() + i))))
                            i++;
                        else
                        {
                            _context.Add(staff);
                            break;
                        }
                    }
                }
                else
                    _context.Update(staff);

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdBank"] = new SelectList(_context.Banks, "IdBank", "NameBank", staff.IdBank);
            return View(staff);
        }

        public async Task<IActionResult> Delete(string id)
        {
            var staff = await _context.Staffs.FindAsync(id);
            _context.Staffs.Remove(staff);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View("~/Views/Shared/AccessDenied.cshtml");
        }
    }
}
