using BFCAI.Nesyan.Domain.Entities.Primary.Patient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BFCAI.Nesyan.Infrastructure.Presistence.Data.Config.Primary.Patients
{
    internal class PatientConfigurations : UserConfigurations<Patient>
    {
        public override void Configure(EntityTypeBuilder<Patient> builder)
        {
            base.Configure(builder);

            builder.Property(p => p.BloodType)
                .IsRequired()
                .HasMaxLength(10);

            builder.Property(p => p.CurrentStage)
                .HasConversion<int>()
                .IsRequired();

            // Setup Many-to-Many with Doctor
            builder.HasMany(p => p.Doctors)
                .WithMany(d => d.Patients)
                .UsingEntity(j => j.ToTable("DoctorPatients"));

            // Setup One-to-Many with Relative
            builder.HasMany(p => p.Relatives)
                .WithOne(r => r.Patient)
                .HasForeignKey(r => r.PatientId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
