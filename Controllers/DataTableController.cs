using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using test.Models;
using test.ViewModels;

namespace test.Controllers
{
    [Authorize(Roles = "Admin,Manager")]
    public class DataTableController : Controller
    {
        private readonly AppDataDbContext _context;
        

        public DataTableController(AppDataDbContext context)
        {
            _context = context;
        }
        public string IDIncrement(int index)
        {
            if (index < 10)
                return "00" + Math.Abs(index).ToString();
            if (index >= 10 && index < 100)
                return "0" + Math.Abs(index).ToString();
            return Math.Abs(index).ToString();

        }
        public IActionResult Index()
        {
            BankDataVM model = new BankDataVM();
            model.BookTypes = GetBookTypes();
            model.Interests = GetInterests();
            model.DetailInterests = GetDetailInterests();
            model.Terms = GetTerms();
            return View(model);
        }

        public IEnumerable<BookType> GetBookTypes()
        {
            return _context.BookTypes.ToList();
        }
        public IEnumerable<Interest> GetInterests()
        {
            return _context.Interests.ToList();
        }
        public IEnumerable<DetailInterest> GetDetailInterests()
        {
            var appDataDbContext = _context.DetailInterests.Include(d => d.Interest).Include(d => d.Term);
            return appDataDbContext.ToList();
        }
        public IEnumerable<Term> GetTerms()
        {
            return _context.Terms.ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult BookTypeUpdate(string id = "")
        {
            if (id == "")
                return View(new BookType());
            return View(_context.BookTypes.Find(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BookTypeUpdate([Bind("IdBookType,NameBookType")] BookType bookType)
        {
            if (ModelState.IsValid)
            {
                if (bookType.IdBookType == null)
                {
                    bookType.IdBookType = "BT" + (_context.BookTypes.Count() + 1).ToString();
                    _context.Add(bookType);
                }
                else
                    _context.Update(bookType);

                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(bookType);
        }


        
        public async Task<IActionResult> BookTypeDelete(string id)
        {
            var bookType = await _context.BookTypes.FindAsync(id);//gan customer muon xoa tu id ben razor dua vao day
            _context.BookTypes.Remove(bookType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        
        public IActionResult test()
        {
            return Content((_context.Interests.Max(x => x.AppliedTo).ToString()));
        }
        public IActionResult InterestUpdate(string id = "")
        {
            ViewBag.LastestInterstAppliedDate = _context.Interests.Max(x => x.AppliedTo);//.ToString("dd-MMM-yy h:mm:ss tt");
            if (id == null)
                return View(new Interest());
            return View(_context.Interests.Find(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> InterestUpdate(/*[Bind("IdInterest,AppliedFrom,AppliedTo")]*/ Interest interest)
        {
            var date = Convert.ToDateTime(_context.Interests.Max(x => x.AppliedTo));
            if (ModelState.IsValid && ((interest.AppliedTo > interest.AppliedFrom) || (interest.AppliedFrom == date) || (interest.AppliedTo != null) || (interest.AppliedTo == null)))
            {
                if (interest.IdInterest == null)
                {
                    interest.IdInterest = "I" + DateTime.Now.ToString("yyMMdd") +(_context.Interests.Count() +1).ToString() ;
                    _context.Add(interest);
                }
                else
                    _context.Update(interest);

                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(interest);
        }

        public async Task<IActionResult> InterestDelete(string id)
        {
            var interest = await _context.Interests.FindAsync(id);
            _context.Interests.Remove(interest);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        public IActionResult DetailInterestUpdate(string id = "")
        {
            ViewData["IdInterest"] = new SelectList(_context.Interests, "IdInterest", "IdInterest");
            ViewData["IdTerm"] = new SelectList(_context.Terms, "IdTerm", "NameTerm");
            if (id == null)
                return View(new DetailInterest());
            return View(_context.DetailInterests.Find(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DetailInterestUpdate([Bind("IdDI,IdInterest,IdTerm,InterestRateDI")] DetailInterest detailInterest)
        {
            if (ModelState.IsValid)
            {
                if (detailInterest.IdDI == null)
                {
                    detailInterest.IdDI = "DI" + detailInterest.IdInterest + detailInterest.IdTerm + (_context.DetailInterests.Count() + 1).ToString();
                    _context.Add(detailInterest);
                }
                else
                    _context.Update(detailInterest);

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdInterest"] = new SelectList(_context.Interests, "IdInterest", "IdInterest", detailInterest.IdInterest);
            ViewData["IdTerm"] = new SelectList(_context.Terms, "IdTerm", "NameTerm", detailInterest.IdTerm);
            return View(detailInterest);
        }

        public async Task<IActionResult> DetailInterestDelete(string id)
        {
            var detailInterest = await _context.DetailInterests.FindAsync(id);
            _context.DetailInterests.Remove(detailInterest);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult TermUpdate(string id = "")
        {
            if (id == null)
                return View(new Term());
            return View(_context.Terms.Find(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TermUpdate([Bind("IdTerm,NameTerm,InterestRateT")] Term term)
        {
            if (ModelState.IsValid)
            {
                if (term.IdTerm == null)
                {
                    int i = 1;
                    while (true)
                    {
                        if (_context.Terms.Contains(_context.Terms.Find("TRM" + IDIncrement(_context.Terms.Count() + i))))
                            i++;
                        else
                        {
                            term.IdTerm = "TRM" + IDIncrement(_context.Terms.Count() + i);
                            break;
                        }
                    }
                    _context.Add(term);
                }
                else
                    _context.Update(term);

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(term);
        }


        public async Task<IActionResult> TermDelete(string id)
        {
            var term = await _context.Terms.FindAsync(id);
            _context.Terms.Remove(term);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}