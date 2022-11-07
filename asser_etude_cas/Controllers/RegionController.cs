using asser_etude_cas.Data;
using asser_etude_cas.Models;
using asser_etude_cas.Models.Create;
using asser_etude_cas.Models.Update;
using asser_etude_cas.Models.View;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace asser_etude_cas.Controllers
{
    public class RegionController : Controller
    {
        private readonly ASERDbContext _aSERDbContext;
        public RegionController(ASERDbContext aSERDbContext)
        {
            _aSERDbContext = aSERDbContext;

        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<RegionEntity> model = await _aSERDbContext.RegionEntity.ToListAsync();
            RegionsViewModel regions = new RegionsViewModel()
            {
                Regions = model,
                SelectedRegion = null,
                isCreate = true,
            };
            return View(regions);
        }
        [HttpPost]
        public async Task<IActionResult> Add(RegionCreateDto input)
        {
            RegionEntity entity = new RegionEntity()
            {
                Id = Guid.NewGuid(),
                Nom = input.Nom,
            };
            await _aSERDbContext.RegionEntity.AddAsync(entity);
            await _aSERDbContext.SaveChangesAsync();
            List<RegionEntity> model = await _aSERDbContext.RegionEntity.ToListAsync();
            RegionsViewModel regions = new RegionsViewModel()
            {
                Regions = model,
                SelectedRegion = null,
                isCreate = true,
            };
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            RegionEntity entity = await _aSERDbContext.RegionEntity.FirstOrDefaultAsync(ent => ent.Id == id);
            RegionUpdateDto regionUpdateDto = new RegionUpdateDto()
            {
                Id = entity.Id,
                Nom = entity.Nom,
            };

            RegionsViewModel model = new RegionsViewModel()
            {
                Regions = await _aSERDbContext.RegionEntity.ToListAsync(),
                SelectedRegion = regionUpdateDto,
                isCreate = true,
            };
            return View("Index", model);

        }

        [HttpPost]
        public async Task<IActionResult> Update(RegionUpdateDto input)
        {
            RegionEntity entity = await _aSERDbContext.RegionEntity.FirstOrDefaultAsync(ent => ent.Id == input.Id);
            entity.Nom = input.Nom;
            await _aSERDbContext.SaveChangesAsync();

            List<RegionEntity> model = await _aSERDbContext.RegionEntity.ToListAsync();
            RegionsViewModel regions = new RegionsViewModel()
            {
                Regions = model,
                SelectedRegion = null,
                isCreate = true,
            };

            return View("Index", regions);
        }


    }
}
