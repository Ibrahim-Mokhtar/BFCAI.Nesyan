using BFCAI.Nesyan.Domain.Entities.Primary.TreatmentRequests;
using BFCAI.Nesyan.Infrastructure.Presistence.Data.Config.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BFCAI.Nesyan.Infrastructure.Presistence.Data.Config.Primary.TreatmentRequests
{
    internal class TreatmentRequestConfigurations : BaseEntityConfigurations<TreatmentRequest, int>
    {
        public override void Configure(EntityTypeBuilder<TreatmentRequest> builder)
        {
            base.Configure(builder);

            builder.Property(tr => tr.MemoryLossFrequency).IsRequired().HasMaxLength(50);
            builder.Property(tr => tr.ConfusionFrequency).IsRequired().HasMaxLength(50);
            builder.Property(tr => tr.LanguageProblems).IsRequired().HasMaxLength(50);
            builder.Property(tr => tr.MoodChanges).IsRequired().HasMaxLength(50);
            builder.Property(tr => tr.RepetitiveBehavior).IsRequired().HasMaxLength(50);

            builder.Property(tr => tr.Status)
                .HasConversion<int>()
                .IsRequired();

            builder.HasOne(tr => tr.Patient)
                .WithMany(p => p.TreatmentRequests)
                .HasForeignKey(tr => tr.PatientId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(tr => tr.Doctor)
                .WithMany()
                .HasForeignKey(tr => tr.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(tr => tr.Relative)
                .WithMany()
                .HasForeignKey(tr => tr.RelativeId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
