using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Campeonato.Models;
using Campeonato.Data;
using Microsoft.EntityFrameworkCore;

namespace Campeonato.Controllers
{
    public class HomeController : Controller
    {
        private readonly CampeonatoContext db;

        public HomeController(CampeonatoContext context)
        {
            db = context;
        }

        // GET: Placares
        public async Task<IActionResult> Index()
        {
            var campeonatoContext = db.Placar.Include(p => p.Jogador);
            return View(await campeonatoContext.ToListAsync());
        }

        public IActionResult Privacy()
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
