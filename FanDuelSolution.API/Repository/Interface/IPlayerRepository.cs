using FanDuelSolution.API.Entities;

namespace FanDuelSolution.API.Repository.Interface
{
    public interface IPlayerRepository : IRepository<Player>
    {
        Task UpdateAsync(Player player);
    }
}
