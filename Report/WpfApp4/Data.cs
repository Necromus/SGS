using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace WpfApp4
{
    public class ApplicationContext : DbContext
    {

        public DbSet<Report> Reports { get; set; }

        public DbSet<City> Cities { get; set; }
        public DbSet<Brigade> Brigades { get; set; }

        public DbSet<WorkShop> WorkShops { get; set; }

        public DbSet<Employee> Employees { get; set; }

        public ApplicationContext() => Database.EnsureCreated();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=Report;Trusted_Connection=True;TrustServerCertificate=True");
        }
    }

    public class Report
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int id { get; set; }
        public string city { get; set; }
        public string workShop { get; set; }
        public string employee { get; set; }
        public string brigade { get; set; }
        public string change { get; set; }

    }

    public class Brigade
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int id { get; set; }
        public string brigade { get; set; }
    }

    public class City
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int id { get; set; }
        public string city { get; set; }
        
    }

    public class WorkShop
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int id { get; set; }
        public string workShop { get; set;}
        public int cityId { get; set; }
        [ForeignKey("cityId")]
        public City City { get; set; }
    }

    public class Employee
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int id { get; set; }
        public string employee { get; set;}
        public int workShopId { get; set; }
        [ForeignKey("workShopId")]
        public WorkShop WorkShop { get; set; }
    }
}
