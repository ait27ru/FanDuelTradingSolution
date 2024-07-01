using FanDuelSolution.API.Entities;

namespace FanDuelSolution.API.Repository.Interface
{
    public interface IDepthChartRepository : IRepository<DepthChart>
    {
        Task UpdateAsync(DepthChart depthChart);
    }
}
