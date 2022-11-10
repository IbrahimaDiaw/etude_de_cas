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
    public class AgentController : Controller
    {
        private readonly ASERDbContext _context;

        public AgentController(ASERDbContext context)
        {
            _context = context;
        }

        // GET: Agent
        public async Task<IActionResult> Index()
        {
            return View(await _context.Agences.ToListAsync());
        }

        // GET: Agent/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agentEntity = await _context.Agences
                .FirstOrDefaultAsync(m => m.Id == id);
            if (agentEntity == null)
            {
                return NotFound();
            }

            return View(agentEntity);
        }

        // GET: Agent/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Agent/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Prenom,Nom,CodeAgent,Telephone,Email")] AgentEntity agentEntity)
        {
            if (ModelState.IsValid)
            {
                agentEntity.Id = Guid.NewGuid();
                _context.Add(agentEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(agentEntity);
        }

        // GET: Agent/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agentEntity = await _context.Agences.FindAsync(id);
            if (agentEntity == null)
            {
                return NotFound();
            }
            return View(agentEntity);
        }

        // POST: Agent/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Prenom,Nom,CodeAgent,Telephone,Email")] AgentEntity agentEntity)
        {
            if (id != agentEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(agentEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AgentEntityExists(agentEntity.Id))
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
            return View(agentEntity);
        }

        // GET: Agent/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agentEntity = await _context.Agences
                .FirstOrDefaultAsync(m => m.Id == id);
            if (agentEntity == null)
            {
                return NotFound();
            }

            return View(agentEntity);
        }

        // POST: Agent/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var agentEntity = await _context.Agences.FindAsync(id);
            _context.Agences.Remove(agentEntity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AgentEntityExists(Guid id)
        {
            return _context.Agences.Any(e => e.Id == id);
        }
    }
}
