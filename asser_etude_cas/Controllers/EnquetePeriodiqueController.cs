using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using asser_etude_cas.Data;
using asser_etude_cas.Models;

namespace asser_etude_cas.Controllers
{
    public class EnquetePeriodiqueController : Controller
    {
        private readonly ASERDbContext _context;

        public EnquetePeriodiqueController(ASERDbContext context)
        {
            _context = context;
        }

        // GET: EnquetePeriodique
        public async Task<IActionResult> Index()
        {
            var aSERDbContext = _context.EnquetePeriodiques.Include(e => e.Village);
            return View(await aSERDbContext.ToListAsync());
        }

        // GET: EnquetePeriodique/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enquetePeriodiqueEntity = await _context.EnquetePeriodiques
                .Include(e => e.Village)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (enquetePeriodiqueEntity == null)
            {
                return NotFound();
            }

            return View(enquetePeriodiqueEntity);
        }

        // GET: EnquetePeriodique/Create
        public IActionResult Create()
        {
            ViewData["VillageId"] = new SelectList(_context.VillageEntity, "Id", "NomVillage");
            ViewData["AgentId"] = new SelectList(_context.Agences, "Id", "NomComplet");
            return View();
        }

        // POST: EnquetePeriodique/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Intitule,NbreMenagesRecenses,TauxAccesParMenage,TauxCouvertureParVillage,VillageId,AgentId")] EnquetePeriodiqueEntity enquetePeriodiqueEntity)
        {
            if (ModelState.IsValid)
            {
                enquetePeriodiqueEntity.Id = Guid.NewGuid();
                List<VillageEntity> villages = await _context.VillageEntity.ToListAsync();
                VillageEntity villageEntity = villages.Where(v => v.Id == enquetePeriodiqueEntity.VillageId).FirstOrDefault();
                enquetePeriodiqueEntity.TauxAccesParMenage = (enquetePeriodiqueEntity.NbreMenagesRecenses / villageEntity.NbreDeMenage) * 100;
                enquetePeriodiqueEntity.TauxCouvertureParVillage = (villages.Count(v => v.Statut == true) / villages.Count()) * 100;
                _context.Add(enquetePeriodiqueEntity);

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["VillageId"] = new SelectList(_context.VillageEntity, "Id", "NomVillage", enquetePeriodiqueEntity.VillageId);
            ViewData["AgentId"] = new SelectList(_context.Agences, "Id", "NomComplet", enquetePeriodiqueEntity.AgentId);
            return View(enquetePeriodiqueEntity);
        }

        // GET: EnquetePeriodique/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enquetePeriodiqueEntity = await _context.EnquetePeriodiques.FindAsync(id);
            if (enquetePeriodiqueEntity == null)
            {
                return NotFound();
            }
            ViewData["VillageId"] = new SelectList(_context.VillageEntity, "Id", "NomVillage", enquetePeriodiqueEntity.VillageId);
            ViewData["AgentId"] = new SelectList(_context.Agences, "Id", "NomComplet", enquetePeriodiqueEntity.AgentId);
            return View(enquetePeriodiqueEntity);
        }

        // POST: EnquetePeriodique/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Intitule, NbreMenagesRecenses,TauxAccesParMenage,TauxCouvertureParVillage,VillageId,AgentId")] EnquetePeriodiqueEntity enquetePeriodiqueEntity)
        {
            if (id != enquetePeriodiqueEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    List<VillageEntity> villages = await _context.VillageEntity.ToListAsync();
                    VillageEntity villageEntity = villages.Where(v => v.Id == enquetePeriodiqueEntity.VillageId).FirstOrDefault();
                    decimal resultAcces = enquetePeriodiqueEntity.NbreMenagesRecenses / villageEntity.NbreDeMenage;
                    enquetePeriodiqueEntity.TauxAccesParMenage = decimal.Round((enquetePeriodiqueEntity.NbreMenagesRecenses / villageEntity.NbreDeMenage) * 100, 2, MidpointRounding.AwayFromZero);
                    enquetePeriodiqueEntity.TauxCouvertureParVillage = (villages.Count(v => v.Statut == true) / villages.Count()) * 100;
                    _context.Update(enquetePeriodiqueEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EnquetePeriodiqueEntityExists(enquetePeriodiqueEntity.Id))
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
            ViewData["VillageId"] = new SelectList(_context.VillageEntity, "Id", "NomVillage", enquetePeriodiqueEntity.VillageId);
            ViewData["AgentId"] = new SelectList(_context.Agences, "Id", "NomComplet", enquetePeriodiqueEntity.AgentId);
            return View(enquetePeriodiqueEntity);
        }

        // GET: EnquetePeriodique/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enquetePeriodiqueEntity = await _context.EnquetePeriodiques
                .Include(e => e.Village)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (enquetePeriodiqueEntity == null)
            {
                return NotFound();
            }

            return View(enquetePeriodiqueEntity);
        }

        // POST: EnquetePeriodique/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var enquetePeriodiqueEntity = await _context.EnquetePeriodiques.FindAsync(id);
            _context.EnquetePeriodiques.Remove(enquetePeriodiqueEntity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EnquetePeriodiqueEntityExists(Guid id)
        {
            return _context.EnquetePeriodiques.Any(e => e.Id == id);
        }
    }
}
