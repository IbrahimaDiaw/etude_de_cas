using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using asser_etude_cas.Data;
using asser_etude_cas.Models;
using Microsoft.AspNetCore.Authorization;

namespace asser_etude_cas.Controllers
{
    [Authorize]
    public class CommuneController : Controller
    {
        private readonly ASERDbContext _context;

        public CommuneController(ASERDbContext context)
        {
            _context = context;
        }

        // GET: Commune
        public async Task<IActionResult> Index()
        {
            var aSERDbContext = _context.CommuneEntity.Include(c => c.Departement);
            return View(await aSERDbContext.ToListAsync());
        }

        // GET: Commune/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var communeEntity = await _context.CommuneEntity
                .Include(c => c.Departement)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (communeEntity == null)
            {
                return NotFound();
            }

            return View(communeEntity);
        }

        // GET: Commune/Create
        public IActionResult Create()
        {
            ViewData["DepartementId"] = new SelectList(_context.DepartementEntity, "Id", "Nom");
            return View();
        }

        // POST: Commune/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nom,DepartementId")] CommuneEntity communeEntity)
        {
            if (ModelState.IsValid)
            {
                communeEntity.Id = Guid.NewGuid();
                _context.Add(communeEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DepartementId"] = new SelectList(_context.DepartementEntity, "Id", "Nom", communeEntity.DepartementId);
            return View(communeEntity);
        }

        // GET: Commune/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var communeEntity = await _context.CommuneEntity.FindAsync(id);
            if (communeEntity == null)
            {
                return NotFound();
            }
            ViewData["DepartementId"] = new SelectList(_context.DepartementEntity, "Id", "Nom", communeEntity.DepartementId);
            return View(communeEntity);
        }

        // POST: Commune/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Nom,DepartementId")] CommuneEntity communeEntity)
        {
            if (id != communeEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(communeEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CommuneEntityExists(communeEntity.Id))
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
            ViewData["DepartementId"] = new SelectList(_context.DepartementEntity, "Id", "Nom", communeEntity.DepartementId);
            return View(communeEntity);
        }

        // GET: Commune/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var communeEntity = await _context.CommuneEntity
                .Include(c => c.Departement)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (communeEntity == null)
            {
                return NotFound();
            }

            return View(communeEntity);
        }

        // POST: Commune/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var communeEntity = await _context.CommuneEntity.FindAsync(id);
            _context.CommuneEntity.Remove(communeEntity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CommuneEntityExists(Guid id)
        {
            return _context.CommuneEntity.Any(e => e.Id == id);
        }
    }
}
