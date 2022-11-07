using asser_etude_cas.Consts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace asser_etude_cas.Models.View
{
    public class DepartementModelViewInput
    {
        public virtual Guid Id { get; set; }

        [Required]
        [StringLength(AserConsts.LABEL_MEDIUM_SIZE)]
        public string Nom { get; set; }

        [Required]
        public virtual Guid RegionId { get; set; }

        public List<CommuneModelViewInput> Communes { get; set; }
    }
}
