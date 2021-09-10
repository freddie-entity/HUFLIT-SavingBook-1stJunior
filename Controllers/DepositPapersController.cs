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
    [Authorize(Roles = "Admin,Manager,Staff")]
    public class DepositPapersController : Controller
    {
        private readonly AppDataDbContext _context;

        public DepositPapersController(AppDataDbContext context)
        {
            _context = context;
        }

        // GET: DepositPapers

        public async Task<IActionResult> Index()
        {
            var appDataDbContext = _context.DepositPapers.Include(d => d.Bank).Include(d => d.BookType).Include(d => d.Customer).Include(d => d.SavingBook).Include(d => d.Staff);
            return View(await appDataDbContext.ToListAsync());
        }        
        public IActionResult Create(string id = "")
        {            
            ViewData["IdBank"] = new SelectList(_context.Banks, "IdBank", "NameBank");
            ViewData["IdBookType"] = new SelectList(_context.BookTypes, "IdBookType", "NameBookType", "BT2");
            ViewData["IdCust"] = new SelectList(_context.Customers, "IdCust", "IdCust");
            ViewData["IdSB"] = new SelectList(_context.SavingBooks, "IdSB", "IdSB");
            ViewData["IdS"] = new SelectList(_context.Staffs, "IdS", "IdS");
            if (id == null)
                return View(new DepositPaper());
            return View(_context.DepositPapers.Find(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdDP,IdSB,IdCust,IdS,IdBank,DepositsDP")] DepositPaper depositPaper)
        {
            depositPaper.IdBookType = (from t in _context.SavingBooks
                                       where t.IdSB == depositPaper.IdSB
                                       select t.IdBookType).SingleOrDefault();
            
            depositPaper.TransactionTimeDP = Convert.ToDateTime(DateTime.Now.ToString("dd-MMM-yy h:mm:ss tt"));
            if (ModelState.IsValid && depositPaper.IdBookType == "BT2") //BT2: khong ky han
            {
                if (depositPaper.IdDP == null)
                {
                    depositPaper.IdDP = "DP" + DateTime.Now.ToString("yyMMdd") + (_context.DepositPapers.Count() + 1).ToString();                    
                    _context.Add(depositPaper);
                }
                else
                    _context.Update(depositPaper);

                SavingBook sv = _context.SavingBooks.Where(sv => sv.IdSB == depositPaper.IdSB).SingleOrDefault();
                sv.CurrentBalance += depositPaper.DepositsDP;

                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "SavingBooks");
            }
            ViewData["IdBank"] = new SelectList(_context.Banks, "IdBank", "NameBank", depositPaper.IdBank);
            ViewData["IdBookType"] = new SelectList(_context.BookTypes, "IdBookType", "NameBookType", "BT2");
            ViewData["IdCust"] = new SelectList(_context.Customers, "IdCust", "IdCust", depositPaper.IdCust);
            ViewData["IdSB"] = new SelectList(_context.SavingBooks, "IdSB", "IdSB", depositPaper.IdSB);
            ViewData["IdS"] = new SelectList(_context.Staffs, "IdS", "IdS", depositPaper.IdS);
            return View(depositPaper);
        }
        public async Task<IActionResult> Delete(string id)
        {
            var depositPaper = await _context.DepositPapers.FindAsync(id);
            _context.DepositPapers.Remove(depositPaper);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
