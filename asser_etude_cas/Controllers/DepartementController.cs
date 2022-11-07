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
    public class DepartementController : Controller
    {
        private readonly ASERDbContext _context;

        public DepartementController(ASERDbContext context)
        {
            _context = context;
        }

        // GET: Departement
        public async Task<IActionResult> Index()
        {
            var aSERDbContext = _context.DepartementEntity.Include(d => d.Region);
            return View(await aSERDbContext.ToListAsync());
        }

        // GET: Departement/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var departementEntity = await _context.DepartementEntity
                .Include(d => d.Region)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (departementEntity == null)
            {
                return NotFound();
            }

            return View(departementEntity);
        }

        // GET: Departement/Create
        public IActionResult Create()
        {
            ViewData["RegionId"] = new SelectList(_context.RegionEntity, "Id", "Nom");
            return View();
        }

        // POST: Departement/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nom,RegionId")] DepartementEntity departementEntity)
        {
            if (ModelState.IsValid)
            {
                departementEntity.Id = Guid.NewGuid();
                _context.Add(departementEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RegionId"] = new SelectList(_context.RegionEntity, "Id", "Nom", departementEntity.RegionId);
            return View(departementEntity);
        }

        // GET: Departement/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var departementEntity = await _context.DepartementEntity.FindAsync(id);
            if (departementEntity == null)
            {
                return NotFound();
            }
            ViewData["RegionId"] = new SelectList(_context.RegionEntity, "Id", "Nom", departementEntity.RegionId);
            return View(departementEntity);
        }

        // POST: Departement/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Nom,RegionId")] DepartementEntity departementEntity)
        {
            if (id != departementEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(departementEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DepartementEntityExists(departementEntity.Id))
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
            ViewData["RegionId"] = new SelectList(_context.RegionEntity, "Id", "Nom", departementEntity.RegionId);
            return View(departementEntity);
        }

        // GET: Departement/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var departementEntity = await _context.DepartementEntity
                .Include(d => d.Region)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (departementEntity == null)
            {
                return NotFound();
            }

            return View(departementEntity);
        }

        // POST: Departement/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var departementEntity = await _context.DepartementEntity.FindAsync(id);
            _context.DepartementEntity.Remove(departementEntity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DepartementEntityExists(Guid id)
        {
            return _context.DepartementEntity.Any(e => e.Id == id);
        }
    }
}
