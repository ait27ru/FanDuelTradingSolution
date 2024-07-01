using FanDuelSolution.API.DbContexts;
using FanDuelSolution.API.Entities;
using FanDuelSolution.API.Repository.Interface;

namespace FanDuelSolution.API.Repository
{
    public class PlayerRepository : Repository<Player>, IPlayerRepository
    {
        private readonly AppDbContext _db;

        public PlayerRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task UpdateAsync(Player player)
        {
            var dbItem = await FirstOrDefaultAsync(i => i.Id == player.Id);

            if (dbItem != null)
            {
                dbItem.FirstName = player.FirstName;
                dbItem.LastName = player.LastName;
                dbItem.Number = player.Number;
            }
        }
    }
}
