using BFCAI.Nesyan.Domain.Entities.Primary.Medications;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BFCAI.Nesyan.Infrastructure.Presistence.Data.Config.Primary.Medications
{
    internal class MedicationConfigurations : IEntityTypeConfiguration<Medication>
    {
        public void Configure(EntityTypeBuilder<Medication> builder)
        {
            builder.Property(m => m.Name).IsRequired().HasMaxLength(255);
            builder.Property(m => m.Dosage).IsRequired().HasMaxLength(100);
            builder.Property(m => m.Frequency).IsRequired().HasMaxLength(100);
            builder.Property(m => m.ScheduleInstructions).IsRequired().HasMaxLength(500);
            builder.Property(m => m.Notes).HasMaxLength(1000);

            builder.HasOne(m => m.Patient)
                .WithMany()
                .HasForeignKey(m => m.PatientId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(m => m.Doctor)
                .WithMany()
                .HasForeignKey(m => m.DoctorId)
                .OnDelete(DeleteBehavior.Restrict); 
        }
    }
}
