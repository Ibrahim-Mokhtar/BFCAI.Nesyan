using System;

namespace BFCAI.Nesyan.Application.Abstraction.Models.MindGames
{
    public class PatientMindGameDto
    {
        public int PatientId { get; set; }
        public int MindGameId { get; set; }
        public MindGameDto MindGame { get; set; } = null!;
        public DateTime AssignedOn { get; set; }
    }
}
