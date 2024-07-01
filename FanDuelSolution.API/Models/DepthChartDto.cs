namespace FanDuelSolution.API.Models
{
    public class DepthChartDto
    {
        public int Id { get; set; }
        public string PositionName { get; set; }
        public string PositionType { get; set; }
        public int PositionId { get; set; }
        public string PlayerName { get; set; }
        public int PlayerId { get; set; }
        public int PositionDepth { get; set; }
    }
}
