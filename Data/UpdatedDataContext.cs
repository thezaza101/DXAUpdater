using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DXAUpdater.Models
{
    public class UpdatedDataContext : DbContext
    {
        public UpdatedDataContext(DbContextOptions<UpdatedDataContext> options)
            : base(options)
            {
                
            }
            public DbSet<UpdatedData> UpdatedData { get; set; }
    }
}