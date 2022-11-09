using asser_etude_cas.Configuration;
using asser_etude_cas.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace asser_etude_cas.Data
{
    public class ASERDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>
    {
        public ASERDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<RegionEntity> RegionEntity { get; set; }
        public DbSet<DepartementEntity> DepartementEntity { get; set; }
        public DbSet<CommuneEntity> CommuneEntity { get; set; }
        public DbSet<VillageEntity> VillageEntity { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new VillageEntityConfiguration());
        }
    }
}
