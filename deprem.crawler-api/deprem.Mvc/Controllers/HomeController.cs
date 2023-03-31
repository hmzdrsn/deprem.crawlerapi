using deprem.Mvc.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using deprem.Model.Models;
using deprem.Database.Data;
using Microsoft.EntityFrameworkCore;

namespace deprem.Mvc.Controllers
{
    public class HomeController : Controller
    {
        
        public IActionResult DepremListesi()
        {
             
            ApplicationDbContext context = new ApplicationDbContext();
            var deprem = context.Depremler.AsNoTracking().ToList();
            return View(deprem);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}