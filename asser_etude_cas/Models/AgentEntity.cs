using asser_etude_cas.Consts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace asser_etude_cas.Models
{
    [Table(AserConsts.SCHEMA_NAME + "_t_agence")]
    public class AgentEntity
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(AserConsts.LABEL_MEDIUM_SIZE)]
        public string Prenom { get; set; }

        [Required]
        [StringLength(AserConsts.LABEL_MEDIUM_SIZE)]
        public string Nom { get; set; }

        [StringLength(AserConsts.LABEL_SHORT_SIZE)]
        public string CodeAgent { get; set; }

        [Required]
        public string Telephone { get; set; }

        [StringLength(AserConsts.LABEL_MEDIUM_SIZE)]
        public string Email { get; set; }

        public string NomComplet
        {
            get
            {
                return $"{Prenom} {Nom}";
            }
        }
    }
}
