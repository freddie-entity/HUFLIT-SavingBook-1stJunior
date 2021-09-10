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
    public class WithdrawalPapersController : Controller
    {
        private readonly AppDataDbContext _context;

        public WithdrawalPapersController(AppDataDbContext context)
        {
            _context = context;
        }

        // GET: WithdrawalPapers
        public async Task<IActionResult> Index()
        {
            var appDataDbContext = _context.WithdrawalPapers.Include(w => w.Bank).Include(w => w.Customer).Include(w => w.SavingBook).Include(w => w.Staff);
            return View(await appDataDbContext.ToListAsync());
        }


        public IActionResult Create(string id = "")
        {
            ViewData["IdBank"] = new SelectList(_context.Banks, "IdBank", "IdBank");
            ViewData["IdCust"] = new SelectList(_context.Customers, "IdCust", "IdCust");
            ViewData["IdSB"] = new SelectList(_context.SavingBooks, "IdSB", "IdSB");
            ViewData["IdS"] = new SelectList(_context.Staffs, "IdS", "IdS");
            if (id == null)
                return View(new WithdrawalPaper());
            return View(_context.WithdrawalPapers.Find(id));
        }
        
        //Ton gan 12 tieng lam cai nay do hichic
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdWP,IdSB,IdCust,IdS,IdBank,WithdrawalsWP")] WithdrawalPaper withdrawalPaper)
        {
            withdrawalPaper.TransactionTimeWP = Convert.ToDateTime(DateTime.Now.ToString("dd-MMM-yy h:mm:ss tt"));
            if (ModelState.IsValid)
            {
                if (withdrawalPaper.IdWP == null)
                {
                    withdrawalPaper.IdWP = "WP" + DateTime.Now.ToString("yyMMdd") + (_context.WithdrawalPapers.Count() + 1).ToString();
                    _context.Add(withdrawalPaper);
                }
                else
                    _context.Update(withdrawalPaper);

                var due_date = (from d in _context.SavingBooks
                           where d.IdSB == withdrawalPaper.IdSB
                           select d.DueDaySB).Single();

                var open_day = (from d in _context.SavingBooks
                                where d.IdSB == withdrawalPaper.IdSB
                                select d.OpenDaySB).Single();

                var current_balance = (from cb in _context.SavingBooks
                                       where cb.IdSB == withdrawalPaper.IdSB
                                       select cb.CurrentBalance).Single();

                var equivalent = (from di in _context.DetailInterests
                                       join i in _context.Interests on di.IdInterest equals i.IdInterest
                                       select new { i.AppliedFrom, i.AppliedTo, di.InterestRateDI }).ToList();

                SavingBook sv = _context.SavingBooks.Where(sv => sv.IdSB == withdrawalPaper.IdSB).SingleOrDefault();

                DateTime new_start = DateTime.Now.AddYears(-2000);
                //DateTime new_end;
                var rate = (from t in _context.Terms
                            where t.IdTerm == sv.IdTerm
                            select t.InterestRateT).SingleOrDefault();

                if (withdrawalPaper.TransactionTimeWP < due_date || rate == null)
                {
                    foreach (var item in equivalent)
                    {
                        if(open_day > item.AppliedFrom && open_day < item.AppliedTo)
                        {
                            TimeSpan gap = (DateTime)item.AppliedTo - open_day;
                            double days = gap.Days;
                            sv.CurrentBalance += (current_balance * item.InterestRateDI * days / 365);
                            new_start = (DateTime)item.AppliedTo;
                            if (new_start < withdrawalPaper.TransactionTimeWP)
                            {
                                foreach (var i in equivalent)
                                {
                                    if (new_start.Date == ((DateTime)i.AppliedFrom).Date)
                                    {
                                        TimeSpan g;
                                        if (i.AppliedTo == null)
                                        {
                                            g = withdrawalPaper.TransactionTimeWP - (DateTime)i.AppliedFrom;
                                            double days_between = g.Days;
                                            sv.CurrentBalance += (current_balance * i.InterestRateDI * days_between / 365);
                                            break;
                                        }
                                        else
                                        {
                                            g = (DateTime)i.AppliedTo - (DateTime)i.AppliedFrom;
                                            double days_between = g.Days;
                                            sv.CurrentBalance += (current_balance * i.InterestRateDI * days_between / 365);
                                            new_start = (DateTime)i.AppliedTo;
                                        }
                                        
                                                                   
                                    }
                                }
                            }
                        }
                    }                  
                }
                else
                {                   
                    var term = (from t in _context.Terms
                                where t.IdTerm == sv.IdTerm
                                select t.NameTerm).SingleOrDefault();
                    sv.CurrentBalance += (current_balance * (double)rate * int.Parse(term) * 30 / 365);
                }

                sv.CurrentBalance -= withdrawalPaper.WithdrawalsWP;


                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "SavingBooks");
            }
            ViewData["IdBank"] = new SelectList(_context.Banks, "IdBank", "IdBank", withdrawalPaper.IdBank);
            ViewData["IdCust"] = new SelectList(_context.Customers, "IdCust", "IdCust", withdrawalPaper.IdCust);
            ViewData["IdSB"] = new SelectList(_context.SavingBooks, "IdSB", "IdSB", withdrawalPaper.IdSB);
            ViewData["IdS"] = new SelectList(_context.Staffs, "IdS", "IdS", withdrawalPaper.IdS);
            return View(withdrawalPaper);
        }

        public async Task<IActionResult> Delete(string id)
        {
            var withdrawalPaper = await _context.WithdrawalPapers.FindAsync(id);
            _context.WithdrawalPapers.Remove(withdrawalPaper);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
