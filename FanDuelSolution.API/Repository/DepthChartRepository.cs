using FanDuelSolution.API.DbContexts;
using FanDuelSolution.API.Entities;
using FanDuelSolution.API.Repository.Interface;

namespace FanDuelSolution.API.Repository
{
    public class DepthChartRepository : Repository<DepthChart>, IDepthChartRepository
    {
        private readonly AppDbContext _db;

        public DepthChartRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task UpdateAsync(DepthChart depthChart)
        {
            var dbItem = await FirstOrDefaultAsync(i => i.Id == depthChart.Id);

            if (dbItem != null)
            {
                dbItem.TeamId = depthChart.TeamId;
                dbItem.PositionId = depthChart.PositionId;
                dbItem.PlayerId = depthChart.PlayerId;
                dbItem.PositionDepth = depthChart.PositionDepth;
            }
        }
    }
}
