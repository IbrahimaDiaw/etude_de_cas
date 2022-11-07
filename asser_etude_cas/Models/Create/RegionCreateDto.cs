using asser_etude_cas.Models.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace asser_etude_cas.Models.Create
{
    public class RegionCreateDto : RegionModelViewInput
    {
        [JsonIgnore]
        public override Guid Id { get; set; }
    }
}
