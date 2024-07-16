using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using Repositories.EFCore;
using System.Diagnostics;
using WebProject.Models;


namespace WebProject.Controllers
{
    
    public class HomeController : Controller
    {
        
        public HomeController()
        {
           
        }
        public IActionResult Admin()
        {
            return View();
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Driver()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }
        
        public IActionResult Result()
        {
            return View();  
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
