using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Campeonato.Data;
using Campeonato.Models;

namespace Campeonato.Controllers
{
    public class JogadoresController : Controller
    {
        private readonly CampeonatoContext db;

        public JogadoresController(CampeonatoContext context)
        {
            db = context;
        }

        // GET: Jogadores
        public async Task<IActionResult> Index()
        {
            return View(await db.Jogador.ToListAsync());
        }

        // GET: Jogadores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jogador = await db.Jogador
                .FirstOrDefaultAsync(m => m.JogadorId == id);
            if (jogador == null)
            {
                return NotFound();
            }

            return View(jogador);
        }

        // GET: Jogadores/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Jogadores/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("JogadorId,Nome,Idade,Nacionalidade")] Jogador jogador)
        {
            if (ModelState.IsValid)
            {
                db.Add(jogador);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(jogador);
        }

        // GET: Jogadores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jogador = await db.Jogador.FindAsync(id);
            if (jogador == null)
            {
                return NotFound();
            }
            return View(jogador);
        }

        // POST: Jogadores/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("JogadorId,Nome,Idade,Nacionalidade")] Jogador jogador)
        {
            if (id != jogador.JogadorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(jogador);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JogadorExists(jogador.JogadorId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(jogador);
        }

        // GET: Jogadores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jogador = await db.Jogador
                .FirstOrDefaultAsync(m => m.JogadorId == id);
            if (jogador == null)
            {
                return NotFound();
            }

            return View(jogador);
        }

        // POST: Jogadores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var jogador = await db.Jogador.FindAsync(id);
            db.Jogador.Remove(jogador);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JogadorExists(int id)
        {
            return db.Jogador.Any(e => e.JogadorId == id);
        }
    }
}
