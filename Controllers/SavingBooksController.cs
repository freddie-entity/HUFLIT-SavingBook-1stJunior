using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using test.Models;

namespace test.Controllers
{
    [Authorize(Roles = "Admin,Manager,Staff")]
    public class SavingBooksController : Controller
    {
        private readonly AppDataDbContext _context;

        public SavingBooksController(AppDataDbContext context)
        {
            _context = context;
        }

        // GET: SavingBooks
        public async Task<IActionResult> Index()
        {
            var appDataDbContext = _context.SavingBooks.Include(s => s.BookType).Include(s => s.Customer).Include(s => s.Staff).Include(s => s.Term);
            return View(await appDataDbContext.ToListAsync());
        }
        [HttpGet]
        public async Task<IActionResult> Index(string text)
        {
            ViewData["GetDetails"] = text;
            var query = from x in _context.SavingBooks.Include(s => s.BookType).Include(s => s.Customer).Include(s => s.Staff).Include(s => s.Term)
                        select x;
            if (!String.IsNullOrEmpty(text))
                query = query.Where(x => x.IdSB.Contains(text) || x.CurrentBalance.ToString().Contains(text) || x.IdBookType.Contains(text) || x.IdTerm.Contains(text));

            return View(await query.AsNoTracking().ToListAsync());
        }

        public IActionResult Create(string id = "")
        {
            ViewData["IdBookType"] = new SelectList(_context.BookTypes, "IdBookType", "NameBookType");
            ViewData["IdCust"] = new SelectList(_context.Customers, "IdCust", "IdCust");
            ViewData["IdS"] = new SelectList(_context.Staffs, "IdS", "IdS");
            ViewData["IdTerm"] = new SelectList(_context.Terms, "IdTerm", "NameTerm");
            if (id == null)
                return View(new SavingBook());
            return View(_context.SavingBooks.Find(id));
        }
        public IActionResult Test()
        {

            return Content("");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //"IdSB,IdCust,IdS,IdBookType,IdTerm,DepositsSB,InterestPaymentMethodSB,InterestReceivingAccount,CurrentBalance"
        public async Task<IActionResult> Create([Bind("IdSB,IdCust,IdS,IdBookType,IdTerm,DepositsSB,InterestReceivingAccount")] SavingBook savingBook)
        {

            if (ModelState.IsValid && ((savingBook.IdBookType == "BT1" && savingBook.IdTerm != "TRM001") || (savingBook.IdBookType == "BT2" && savingBook.IdTerm == "TRM001")))
            {
                if (savingBook.IdSB == null)
                {
                    savingBook.IdSB = "SB" + savingBook.IdCust + DateTime.Now.ToString("yyMMdd") + (_context.SavingBooks.Count() + 1).ToString();
                    savingBook.CurrentBalance = savingBook.DepositsSB;
                    savingBook.OpenDaySB = Convert.ToDateTime(DateTime.Now.ToString("dd-MMM-yy h:mm:ss tt"));
                    savingBook.InterestPaymentMethodSB = true;
                    if (savingBook.IdBookType == "BT1" && savingBook.IdTerm != "TRM001")
                    {
                        Term t = _context.Terms.Where(t => t.IdTerm == savingBook.IdTerm).SingleOrDefault();
                        savingBook.DueDaySB = savingBook.OpenDaySB.AddMonths(int.Parse(t.NameTerm));
                    }
                    if (savingBook.IdBookType == "BT2" && savingBook.IdTerm == "TRM001")
                    {
                        savingBook.DueDaySB = null;
                    }
                    _context.Add(savingBook);

                }
                else
                {
                    savingBook.CurrentBalance = savingBook.DepositsSB;
                    _context.Update(savingBook);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdBookType"] = new SelectList(_context.BookTypes, "IdBookType", "NameBookType", savingBook.IdBookType);
            ViewData["IdCust"] = new SelectList(_context.Customers, "IdCust", "IdCust", savingBook.IdCust);
            ViewData["IdS"] = new SelectList(_context.Staffs, "IdS", "IdS", savingBook.IdS);
            ViewData["IdTerm"] = new SelectList(_context.Terms, "IdTerm", "NameTerm", savingBook.IdTerm);
            return View(savingBook);
        }

        public async Task<IActionResult> Delete(string id)
        {
            var savingBook = await _context.SavingBooks.FindAsync(id);
            _context.SavingBooks.Remove(savingBook);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
