using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace asser_etude_cas.Data
{
    public class ASERDbContext : DbContext
    {
        public ASERDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
