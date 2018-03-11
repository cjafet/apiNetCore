using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class CampaignDb : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=CampaignDb;Trusted_Connection=True;");
            }
        }

        public CampaignDb(DbContextOptions<CampaignDb> options) : base(options)
        {
            Database.Migrate();
        }

        public CampaignDb()
        {
        }

        public DbSet<CampaignDto> Campaign { get; set; }
    }
}
