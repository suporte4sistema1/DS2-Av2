﻿using System;
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
    public class PlacaresController : Controller
    {
        private readonly CampeonatoContext db;

        public PlacaresController(CampeonatoContext context)
        {
            db = context;
        }

        // GET: Placares
        public async Task<IActionResult> Index()
        {
            var campeonatoContext = db.Placar.Include(p => p.Jogador);
            return View(await campeonatoContext.ToListAsync());
        }

        public async Task<IActionResult> PlacarDez()
        {
            var dezPlacar = db.Placar.Include(p => p.Jogador).
                                OrderByDescending(p => p.TotalPontos).
                                Take(10);

            return View(await dezPlacar.ToListAsync());
        }
        // GET: Placares/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var placar = await db.Placar
                .Include(p => p.Jogador)
                .FirstOrDefaultAsync(m => m.PlacarId == id);
            if (placar == null)
            {
                return NotFound();
            }

            return View(placar);
        }

        // GET: Placares/Create
        public IActionResult Create()
        {            ViewData["JogadorId"] = new SelectList(db.Set<Jogador>(), "JogadorId", "Nome");
            return View();
        }

        // POST: Placares/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PlacarId,JogadorId,TotalPontos,HorarioId")] Placar placar)
        {
            if (ModelState.IsValid)
            {
                db.Add(placar);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["JogadorId"] = new SelectList(db.Set<Jogador>(), "JogadorId", "Nome", placar.JogadorId);
            return View(placar);
        }

        // GET: Placares/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var placar = await db.Placar.FindAsync(id);
            if (placar == null)
            {
                return NotFound();
            }
            ViewData["JogadorId"] = new SelectList(db.Set<Jogador>(), "JogadorId", "Nome", placar.JogadorId);
            return View(placar);
        }

        // POST: Placares/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PlacarId,JogadorId,TotalPontos,HorarioId")] Placar placar)
        {
            if (id != placar.PlacarId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(placar);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlacarExists(placar.PlacarId))
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
            ViewData["JogadorId"] = new SelectList(db.Set<Jogador>(), "JogadorId", "Nacionalidade", placar.JogadorId);
            return View(placar);
        }

        // GET: Placares/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var placar = await db.Placar
                .Include(p => p.Jogador)
                .FirstOrDefaultAsync(m => m.PlacarId == id);
            if (placar == null)
            {
                return NotFound();
            }

            return View(placar);
        }

        // POST: Placares/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var placar = await db.Placar.FindAsync(id);
            db.Placar.Remove(placar);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlacarExists(int id)
        {
            return db.Placar.Any(e => e.PlacarId == id);
        }
    }
}
