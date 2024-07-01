using FanDuelSolution.API.Entities;

namespace FanDuelSolution.API.Repository.Interface
{
    public interface ITeamRepository : IRepository<Team>
    {
        Task UpdateAsync(Team team);
    }
}
