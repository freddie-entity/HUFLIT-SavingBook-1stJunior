using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
//using test.Migrations;
using test.Models;
using static test.Helper;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace test.Controllers
{
    [Authorize(Roles = "Admin,Manager,Staff")]
    public class CustomerController : Controller
    {
        private readonly AppDataDbContext _appDataDbContext;
        
        public CustomerController(AppDataDbContext appDataDbContext)
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
        public IActionResult Index()
        {
            return View(_appDataDbContext.Customers.ToList());
        }
        [HttpGet]
        public async Task<IActionResult> Index(string text)
        {
            ViewData["GetDetails"] = text;
            var query = from x in _appDataDbContext.Customers
                        select x;
            if (!String.IsNullOrEmpty(text))
                query = query.Where(x => x.IdCust.Contains(text) || x.NameCust.Contains(text));

            return View(await query.AsNoTracking().ToListAsync());
        }
        //GET: Customer/Create
        [NoDirectAccess] // tránh việc người dùng muốn truy xuất create thông qua url => redirect lại trang cũ
        public IActionResult Create(string id="")//id chỉ rỗng khi mà tạo mới customer còn nếu edit thì đã có id do đã gán bằng item.CustomerId
        {
            if(id=="")// binh thuong thi cu tao mot customer moi
                return View(new Customer());
            return View(_appDataDbContext.Customers.Find(id));// neu ma bam vo edit thi o day phai tim id tuong uong (id da gan o ben asp-route-id roi)
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCust,NameCust,DOBCust,AddressCust,PhoneCust,IDCardCust,IDCardGrantedDayCust")] Customer customer)
        {
            if(ModelState.IsValid)//+check if the data is valid
            {                           
                if (customer.IdCust == null)
                {
                    int i = 1;
                    while(true)
                    {
                        if (_appDataDbContext.Customers.Contains(_appDataDbContext.Customers.Find("CUS" + IDIncrement(_appDataDbContext.Customers.Count() + i))))
                            i++;
                        else
                        {
                            customer.IdCust = "CUS" + IDIncrement(_appDataDbContext.Customers.Count() + i);
                            break;
                        }
                    }
                    _appDataDbContext.Add(customer);
                }      
                else
                    _appDataDbContext.Update(customer);
                    
                await _appDataDbContext.SaveChangesAsync();
                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this,"_CustomerList", _appDataDbContext.Customers.ToList()) });
            }
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "Create", customer) });
        }


        public async Task<IActionResult> Delete(string id)
        {
            var customer = await _appDataDbContext.Customers.FindAsync(id);//gan customer muon xoa tu id ben razor dua vao day
            _appDataDbContext.Customers.Remove(customer);
            await _appDataDbContext.SaveChangesAsync();
            return Json(new { html = Helper.RenderRazorViewToString(this, "_CustomerList", _appDataDbContext.Customers.ToList()) });
        }
    }
}
