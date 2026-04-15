using System;

using BFCAI.Nesyan.Domain.Entities.Common;

namespace BFCAI.Nesyan.Domain.Entities.Primary.MindGames
{
    public class PatientMindGame : BaseEntity<int>
    {
        public int PatientId { get; set; }
        public Patient.Patient Patient { get; set; } = null!;

        public int MindGameId { get; set; }
        public MindGame MindGame { get; set; } = null!;

        public DateTime AssignedOn { get; set; }
    }
}
