using asser_etude_cas.Models.View;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace asser_etude_cas.Models.Create
{
    public class DepartementCreateDto : DepartementModelViewInput
    {
        [JsonIgnore]
        public override Guid Id { get; set; }

        [JsonIgnore]
        [Required]
        public override Guid RegionId { get; set; }
    }
}
