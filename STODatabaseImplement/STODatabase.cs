using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using STODatabaseImplement.Models;
using Microsoft.EntityFrameworkCore;


namespace STODatabaseImplement
{
    public class STODatabase : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured == false)
            {
                optionsBuilder.UseSqlServer(@"Data Source=localhost\SQLEXPRESS;Initial Catalog=STODatabase;Integrated Security=True;MultipleActiveResultSets=True;Trusted_Connection=True;TrustServerCertificate=True");
            }
            base.OnConfiguring(optionsBuilder);
        }

        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<StoreKeeper> StoreKeepers { get; set; }
        public virtual DbSet<Car> Cars { get; set; }
        public virtual DbSet<CarSparePart> CarSpareParts { get; set; }
        public virtual DbSet<ServiceRecord> ServiceRecords { get; set; }
        public virtual DbSet<SparePart> SpareParts { get; set; }
        public virtual DbSet<TimeOfWork> TimeOfWorks  { get; set; }
        public virtual DbSet<TO> TOs { get; set; }
        public virtual DbSet<TOWork> TOWorks  { get; set; }
        public virtual DbSet<Work> Works { get; set; }
        public virtual DbSet<WorkSparePart> WorkSpareParts { get; set; }
        public virtual DbSet<WorkType> WorkTypes { get; set; }
    }
}
