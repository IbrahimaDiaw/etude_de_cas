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
    public class RegionEntitiesController : Controller
    {
        private readonly ASERDbContext _context;

        public RegionEntitiesController(ASERDbContext context)
        {
            _context = context;
        }

        // GET: RegionEntities
        public async Task<IActionResult> Index()
        {
            return View(await _context.RegionEntity.ToListAsync());
        }

        // GET: RegionEntities/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var regionEntity = await _context.RegionEntity
                .FirstOrDefaultAsync(m => m.Id == id);
            if (regionEntity == null)
            {
                return NotFound();
            }

            return View(regionEntity);
        }

        // GET: RegionEntities/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: RegionEntities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nom")] RegionEntity regionEntity)
        {
            if (ModelState.IsValid)
            {
                regionEntity.Id = Guid.NewGuid();
                _context.Add(regionEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(regionEntity);
        }

        // GET: RegionEntities/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var regionEntity = await _context.RegionEntity.FindAsync(id);
            if (regionEntity == null)
            {
                return NotFound();
            }
            return View(regionEntity);
        }

        // POST: RegionEntities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Nom")] RegionEntity regionEntity)
        {
            if (id != regionEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(regionEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RegionEntityExists(regionEntity.Id))
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
            return View(regionEntity);
        }

        // GET: RegionEntities/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var regionEntity = await _context.RegionEntity
                .FirstOrDefaultAsync(m => m.Id == id);
            if (regionEntity == null)
            {
                return NotFound();
            }

            return View(regionEntity);
        }

        // POST: RegionEntities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var regionEntity = await _context.RegionEntity.FindAsync(id);
            _context.RegionEntity.Remove(regionEntity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RegionEntityExists(Guid id)
        {
            return _context.RegionEntity.Any(e => e.Id == id);
        }
    }
}
