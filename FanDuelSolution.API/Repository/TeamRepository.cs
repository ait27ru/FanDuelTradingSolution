using FanDuelSolution.API.DbContexts;
using FanDuelSolution.API.Entities;
using FanDuelSolution.API.Repository.Interface;

namespace FanDuelSolution.API.Repository
{
    public class TeamRepository : Repository<Team>, ITeamRepository
    {
        private readonly AppDbContext _db;

        public TeamRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task UpdateAsync(Team team)
        {
            var dbItem = await FirstOrDefaultAsync(i => i.Id == team.Id);

            if (dbItem != null)
            {
                dbItem.Name = team.Name;
            }
        }
    }
}
