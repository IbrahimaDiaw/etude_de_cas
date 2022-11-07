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
    public class VillageController : Controller
    {
        private readonly ASERDbContext _context;

        public VillageController(ASERDbContext context)
        {
            _context = context;
        }

        // GET: Village
        public async Task<IActionResult> Index()
        {
            var aSERDbContext = _context.VillageEntity.Include(v => v.Commune)
                                                      .ThenInclude(c => c.Departement)
                                                      .ThenInclude(d => d.Region);
            return View(await aSERDbContext.ToListAsync());
        }

        // GET: Village/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var villageEntity = await _context.VillageEntity
                .Include(v => v.Commune)
                    .ThenInclude(c => c.Departement)
                        .ThenInclude(d => d.Region)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (villageEntity == null)
            {
                return NotFound();
            }

            return View(villageEntity);
        }

        // GET: Village/Create
        public IActionResult Create()
        {
            ViewData["CommuneId"] = new SelectList(_context.CommuneEntity, "Id", "Nom");
            ViewData["RegionId"] = new SelectList(_context.RegionEntity, "Id", "Nom");
            ViewData["DepartementId"] = new SelectList(_context.DepartementEntity, "Id", "Nom");
            return View();
        }

        // POST: Village/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NomVillage,NbreDeMenage,Statut,Longitude,Latitude,RegionId,DepartementId,CommuneId")] VillageEntity villageEntity)
        {
            if (ModelState.IsValid)
            {
                villageEntity.Id = Guid.NewGuid();
                _context.Add(villageEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CommuneId"] = new SelectList(_context.CommuneEntity, "Id", "Nom", villageEntity.CommuneId);
            ViewData["RegionId"] = new SelectList(_context.RegionEntity, "Id", "Nom", villageEntity.RegionId);
            ViewData["DepartementId"] = new SelectList(_context.DepartementEntity, "Id", "Nom", villageEntity.DepartementId);
            return View(villageEntity);
        }

        // GET: Village/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var villageEntity = await _context.VillageEntity.FindAsync(id);
            if (villageEntity == null)
            {
                return NotFound();
            }
            ViewData["CommuneId"] = new SelectList(_context.CommuneEntity, "Id", "Nom", villageEntity.CommuneId);
            ViewData["RegionId"] = new SelectList(_context.RegionEntity, "Id", "Nom", villageEntity.RegionId);
            ViewData["DepartementId"] = new SelectList(_context.DepartementEntity, "Id", "Nom", villageEntity.DepartementId);
            return View(villageEntity);
        }

        // POST: Village/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,NomVillage,NbreDeMenage,Statut,Longitude,Latitude,RegionId,DepartementId,CommuneId")] VillageEntity villageEntity)
        {
            if (id != villageEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(villageEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VillageEntityExists(villageEntity.Id))
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
            ViewData["CommuneId"] = new SelectList(_context.CommuneEntity, "Id", "Nom", villageEntity.CommuneId);;
            ViewData["RegionId"] = new SelectList(_context.RegionEntity, "Id", "Nom", villageEntity.RegionId);
            ViewData["DepartementId"] = new SelectList(_context.DepartementEntity, "Id", "Nom", villageEntity.DepartementId);
            return View(villageEntity);
        }

        // GET: Village/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var villageEntity = await _context.VillageEntity
                .Include(v => v.Commune)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (villageEntity == null)
            {
                return NotFound();
            }

            return View(villageEntity);
        }

        // POST: Village/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var villageEntity = await _context.VillageEntity.FindAsync(id);
            _context.VillageEntity.Remove(villageEntity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VillageEntityExists(Guid id)
        {
            return _context.VillageEntity.Any(e => e.Id == id);
        }
    }
}
