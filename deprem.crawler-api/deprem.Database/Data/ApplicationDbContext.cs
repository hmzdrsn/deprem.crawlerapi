using deprem.Model.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace deprem.Database.Data
{
    public class ApplicationDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-228OGM4;Database= newModelTESTDB;User Id=sa;Password=123456;TrustServerCertificate=True"
                ,opt=>opt.UseNetTopologySuite());//***
        }

        
        public DbSet<Deprem> Depremler { get; set; }

       
    }
}
