using asser_etude_cas.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace asser_etude_cas.Configuration
{
    public class EnquetePeriodiqueEntityConfiguration : IEntityTypeConfiguration<EnquetePeriodiqueEntity>
    {
        public void Configure(EntityTypeBuilder<EnquetePeriodiqueEntity> builder)
        {
            builder.Property(a => a.TauxAccesParMenage).HasColumnType("decimal(5,2)");
            builder.Property(a => a.TauxCouvertureParVillage).HasColumnType("decimal(5,2)");
        }
    }
}
