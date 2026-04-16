using BFCAI.Nesyan.Domain.Entities.Medications;
using BFCAI.Nesyan.Infrastructure.Presistence.Data.Config.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BFCAI.Nesyan.Infrastructure.Presistence.Data.Config.Medications
{
    internal class MedicationConfigurations : BaseEntityConfigurations<Medication, int>
    {
        public override void Configure(EntityTypeBuilder<Medication> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Dosage)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.Frequency)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.ScheduleInstructions)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(x => x.Notes)
                .HasMaxLength(500);

            builder.HasOne(x => x.Patient)
                .WithMany()
                .HasForeignKey(x => x.PatientId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Doctor)
                .WithMany()
                .HasForeignKey(x => x.DoctorId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(x => x.PatientId);
            builder.HasIndex(x => x.DoctorId);
        }
    }
}
