 using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using test.Models;

namespace test.Controllers
{
    [Authorize(Roles ="Admin,Manager")]
    public class BankController : Controller
    {
        private readonly AppDataDbContext _appDataDbContext;

        public BankController(AppDataDbContext appDataDbContext)
        {
            _appDataDbContext = appDataDbContext;
        }
        public string IDIncrement(int index)
        {
            if (index < 10)
                return "00" + Math.Abs(index).ToString();
            if (index >= 10 && index < 100)
                return "0" + Math.Abs(index).ToString();
            return Math.Abs(index).ToString();

        }
        // GET: Bank
        public async Task<IActionResult> Index()
        {           
            return View(await _appDataDbContext.Banks.ToListAsync());
        }
        
        public IActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdBank,NameBank,AddressBank")] Bank bank)
        {
            if (ModelState.IsValid)
            {
                if (bank.IdBank == null)
                {
                    int i = 1;
                    while (true)
                    {
                        if (_appDataDbContext.Banks.Contains(_appDataDbContext.Banks.Find("B" + IDIncrement(_appDataDbContext.Banks.Count() + i))))
                            i++;
                        else
                        {
                            bank.IdBank = "B" + IDIncrement(_appDataDbContext.Banks.Count() + i);
                            break;
                        }
                    }
                    _appDataDbContext.Add(bank);
                }
                else
                    _appDataDbContext.Update(bank);
                await _appDataDbContext.SaveChangesAsync();               
                return RedirectToAction(nameof(Index));
            }
            return View(bank);
        }

        
        public async Task<IActionResult> Delete(string id)
        {
            var bank = await _appDataDbContext.Banks.FindAsync(id);
            _appDataDbContext.Banks.Remove(bank);
            await _appDataDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
