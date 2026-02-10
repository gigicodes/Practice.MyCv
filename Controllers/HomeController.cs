using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MyCv.Models;

namespace MyCv.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult MyCv()
        {
            return View();
        }
        public IActionResult Hobbies()
        {
            return View();
        }

        public IActionResult ContactMe()
        {
            return View();
        }
        public IActionResult TaxCalculator()
        {
            return View();
        }

        public decimal CalculateTax(decimal AnnualIncome)
        {
            decimal tax = 0;
            // Band 1: First ?800,000 @ 0% (Tax-Free)
            if (AnnualIncome <= 800000) return 0;

            // Remaining balance after tax-free threshold
            decimal balance = AnnualIncome - 800000;

            // Band 2: Next ?2,200,000 @ 15%
            decimal band2 = Math.Min(balance, 2200000);
            tax += band2 * 0.15m;
            balance -= band2;
            if (balance <= 0) return tax;

            // Band 3: Next ?9,000,000 @ 18%
            decimal band3 = Math.Min(balance, 9000000);
            tax += band3 * 0.18m;
            balance -= band3;
            if (balance <= 0) return tax;

            // Band 4: Next ?13,000,000 @ 21%
            decimal band4 = Math.Min(balance, 13000000);
            tax += band4 * 0.21m;
            balance -= band4;
            if (balance <= 0) return tax;

            // Band 5: Next ?25,000,000 @ 23%
            decimal band5 = Math.Min(balance, 25000000);
            tax += band5 * 0.23m;
            balance -= band5;
            if (balance <= 0) return tax;

            // Band 6: Above ?50,000,000 @ 25%
            tax += balance * 0.25m;

            return tax ;
        }

        [HttpPost]
        public IActionResult TaxCalculator(TaxCalculatorModel model)
        {
            if (ModelState.IsValid)
            {
                model.TaxPayable = CalculateTax(model.AnnualIncome);
                return View("TaxCalculator", model);
            }
            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
