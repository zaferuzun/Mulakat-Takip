using Microsoft.EntityFrameworkCore;
using Mulakat_Takip.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mulakat_Takip.Database
{
    public class DatabaseContext : DbContext
    {

        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Users> Users { get; set; }
        public DbSet<PanelOperations> PanelOperations { get; set; }

    }
}
