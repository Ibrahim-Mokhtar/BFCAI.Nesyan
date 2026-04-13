using BFCAI.Nesyan.Domain.Entities.Primary.Doctor;
using BFCAI.Nesyan.Domain.Entities.Primary.Patient;
using BFCAI.Nesyan.Domain.Entities.Primary.Relative;
using BFCAI.Nesyan.Domain.Entities.Primary.TreatmentRequests;
using BFCAI.Nesyan.Domain.Entities.Primary.Medications;
using BFCAI.Nesyan.Domain.Entities.Primary.MindGames;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BFCAI.Nesyan.Infrastructure.Presistence.Data
{
    public class StoreContext:DbContext
    {
        public StoreContext(DbContextOptions<StoreContext> options):base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AssemblyInformation).Assembly);
        }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Relative> Relatives { get; set; }
        public DbSet<TreatmentRequest> TreatmentRequests { get; set; }
        public DbSet<Medication> Medications { get; set; }
        public DbSet<MindGame> MindGames { get; set; }
        public DbSet<PatientMindGame> PatientMindGames { get; set; }
    }
}
