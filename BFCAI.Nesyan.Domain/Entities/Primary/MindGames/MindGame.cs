using BFCAI.Nesyan.Domain.Entities.Common;
using System.Collections.Generic;

namespace BFCAI.Nesyan.Domain.Entities.Primary.MindGames
{
    public class MindGame : BaseEntity<int>
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Category { get; set; } = null!;
        public string? ThumbnailUrl { get; set; }
        
        public ICollection<PatientMindGame> PatientAssignments { get; set; } = new List<PatientMindGame>();
    }
}
