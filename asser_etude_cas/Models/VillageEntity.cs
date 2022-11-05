using asser_etude_cas.Consts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace asser_etude_cas.Models
{
    public interface CoordonneeGeographique
    {
        decimal? Longitude { get; set; }
        decimal? Latitude { get; set; }
    }
    [Table(AserConsts.SCHEMA_NAME + "_t_village")]
    public class VillageEntity : CoordonneeGeographique
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(AserConsts.LABEL_MEDIUM_SIZE)]
        public string NomVillage { get; set; }

        [Required]
        public int NbreDeMenage { get; set; }

        public bool Statut { get; set; }

        public decimal? Longitude { get; set; }
        public decimal? Latitude { get; set; }

        [Required]
        public Guid RegionId { get; set; }

        [Required]
        public Guid DepartementId { get; set; }

        [Required]
        public Guid CommuneId { get; set; }
        public CommuneEntity Commune { get; set; }

    }
}
