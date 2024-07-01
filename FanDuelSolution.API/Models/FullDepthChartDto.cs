namespace FanDuelSolution.API.Models
{
    public class FullDepthChartDto
    {
        public string TeamName { get; set; }

        public IEnumerable<DepthChartDto> DepthChart { get; set; } = new List<DepthChartDto>();

    }
}
