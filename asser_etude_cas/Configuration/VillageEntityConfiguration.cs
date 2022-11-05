using asser_etude_cas.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace asser_etude_cas.Configuration
{
    public class VillageEntityConfiguration : IEntityTypeConfiguration<VillageEntity>
    {
        public void Configure(EntityTypeBuilder<VillageEntity> builder)
        {
            builder.Property(e => e.Statut).HasDefaultValue(false);
            builder.Property(a => a.Longitude).HasColumnType("decimal(15,2)");
            builder.Property(a => a.Latitude).HasColumnType("decimal(15,2)");
        }
    }
}
