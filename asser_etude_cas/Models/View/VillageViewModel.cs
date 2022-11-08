using asser_etude_cas.Models.Create;
using asser_etude_cas.Models.Output;
using asser_etude_cas.Models.Update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace asser_etude_cas.Models.View
{
    public class VillageViewModel 
    {
        public Guid Id { get; set; }
        public string NomVillage { get; set; }
        public int NbreDeMenage { get; set; }
        public bool Statut { get; set; }
        public decimal? Longitude { get; set; }
        public decimal? Latitude { get; set; }
        public Guid RegionId { get; set; }
        public RegionOutput Region { get; set; } 
        public Guid DepartementId { get; set; }
        public DepartementOutput Departement { get; set; }
        public Guid CommuneId { get; set; }
        public CommuneOutput Commune { get; set; }
    }
}
