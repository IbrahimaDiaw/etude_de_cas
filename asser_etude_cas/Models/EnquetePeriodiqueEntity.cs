using asser_etude_cas.Consts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace asser_etude_cas.Models
{
    [Table(AserConsts.SCHEMA_NAME + "_t_enquete_periodique")]
    public class EnquetePeriodiqueEntity
    {
        public Guid Id { get; set; }

        public int NbreMenagesRecenses { get; set; }

        [Range(0, 100)]
        public decimal TauxAccesParMenage { get; set; }
        [Range(0, 100)]
        public decimal TauxCouvertureParVillage { get; set; } 

        [Required]
        public Guid VillageId { get; set; }
        public VillageEntity Village { get; set; }

        [Required]
        public Guid AgentId { get; set; }

        [Required]
        public string Intitule { get; set; }
    }
}
