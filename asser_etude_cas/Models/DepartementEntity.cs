using asser_etude_cas.Consts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace asser_etude_cas.Models
{
    [Table(AserConsts.SCHEMA_NAME + "_t_departement")]
    public class DepartementEntity
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(AserConsts.LABEL_MEDIUM_SIZE)]
        public string Nom { get; set; }

        [Required]
        public Guid RegionId { get; set; }
        public RegionEntity Region { get; set; }

        [InverseProperty("Departement")]
        public List<CommuneEntity> Communes { get; set; }
    }
}
