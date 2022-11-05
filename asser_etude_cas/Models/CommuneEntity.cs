using asser_etude_cas.Consts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace asser_etude_cas.Models
{
    public class CommuneEntity
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(AserConsts.LABEL_MEDIUM_SIZE)]
        public string Nom { get; set; }

        [Required]
        public Guid DepartementId { get; set; }
        public DepartementEntity Departement { get; set; }
    }
}
