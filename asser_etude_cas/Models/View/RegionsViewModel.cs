using asser_etude_cas.Models.Create;
using asser_etude_cas.Models.Update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace asser_etude_cas.Models.View
{
    public class RegionsViewModel 
    {
        public List<RegionEntity> Regions { get; set; }
        public RegionUpdateDto SelectedRegion { get; set; }
        public bool isCreate { get; set; }
    }
}
