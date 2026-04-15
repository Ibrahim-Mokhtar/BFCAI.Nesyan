using BFCAI.Nesyan.Domain.Entities.Primary.MindGames;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BFCAI.Nesyan.Infrastructure.Presistence.Data.Config.Primary.MindGames
{
    internal class MindGameConfigurations : IEntityTypeConfiguration<MindGame>
    {
        public void Configure(EntityTypeBuilder<MindGame> builder)
        {
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Name).IsRequired().HasMaxLength(255);
            builder.Property(m => m.Description).IsRequired().HasMaxLength(1500);
            builder.Property(m => m.Category).HasMaxLength(100);
        }
    }

    internal class PatientMindGameConfigurations : IEntityTypeConfiguration<PatientMindGame>
    {
        public void Configure(EntityTypeBuilder<PatientMindGame> builder)
        {
            builder.HasOne(pm => pm.Patient)
                .WithMany(p => p.AssignedGames)
                .HasForeignKey(pm => pm.PatientId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(pm => pm.MindGame)
                .WithMany(m => m.PatientAssignments)
                .HasForeignKey(pm => pm.MindGameId)
                .OnDelete(DeleteBehavior.Cascade);
                
            builder.Property(pm => pm.AssignedOn).IsRequired();
        }
    }
}
