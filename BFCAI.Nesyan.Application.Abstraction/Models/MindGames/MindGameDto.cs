namespace BFCAI.Nesyan.Application.Abstraction.Models.MindGames
{
    public class MindGameDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Category { get; set; } = null!;
        public string Brief { get; set; } = null!;
        public string TargetMetrics { get; set; } = null!;
    }
}
