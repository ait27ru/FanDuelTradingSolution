using AutoMapper;
using FanDuelSolution.API.Entities;
using FanDuelSolution.API.Models;
using FanDuelSolution.API.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace FanDuelSolution.API.Controllers
{
    [Route("api/teams/{teamId}")]
    [ApiController]
    public class DepthChartController : ControllerBase
    {
        private readonly IDepthChartRepository _depthChartRepository;
        private readonly ITeamRepository _teamRepository;
        private readonly IMapper _mapper;

        public DepthChartController(IDepthChartRepository depthChartRepository,
            ITeamRepository teamRepository,
            IMapper mapper)
        {
            _depthChartRepository = depthChartRepository
                ?? throw new ArgumentNullException(nameof(depthChartRepository));

            _teamRepository = teamRepository
                ?? throw new ArgumentNullException(nameof(teamRepository));

            _mapper = mapper
                ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpPost("addPlayerToDepthChart")]
        public async Task<ActionResult> AddPlayerToDepthChart(int teamId, [FromQuery] int positionId, [FromQuery] int playerId, [FromQuery] int? positionDepth)
        {

            using var tran = await _depthChartRepository.BeginTransactionAsync();
            try
            {
                var depthChartItems = await _depthChartRepository.GetAllAsync(
                    i => i.TeamId == teamId && i.PositionId == positionId,
                    i => i.OrderBy(d => d.PositionDepth),
                    "Player");

                var lastDepth = depthChartItems.LastOrDefault()?.PositionDepth ?? 0;

                var newDepth = 0;

                if (positionDepth == null)
                {
                    newDepth = lastDepth + 1;
                }
                else
                {
                    newDepth = positionDepth.Value;

                    if (lastDepth >= newDepth)
                    {
                        for (var i = 0; i < depthChartItems.Count; i++)
                        {
                            if (depthChartItems[i].PositionDepth >= newDepth)
                            {
                                depthChartItems[i].PositionDepth++;
                            }
                        }
                    }
                }

                var newItem = new DepthChart
                {
                    TeamId = teamId,
                    PositionId = positionId,
                    PlayerId = playerId,
                    PositionDepth = newDepth
                };

                await _depthChartRepository.AddAsync(newItem);
                await _depthChartRepository.SaveAsync();

                return Ok(_mapper.Map<DepthChartDto>(newItem));
            }
            catch (Exception)
            {
                await tran.RollbackAsync();
                return StatusCode(500, "An internal server error occurred. Please try again later.");
            }
        }

        [HttpDelete("removePlayerFromDepthChart")]
        public async Task<ActionResult<PlayerDto>> RemovePlayerFromDepthChart(int teamId, [FromQuery] int positionId, [FromQuery] int playerId)
        {
            var depthChartItem = await _depthChartRepository.FirstOrDefaultAsync(
                i => i.TeamId == teamId && i.PlayerId == playerId && i.PositionId == positionId,
                "Player");

            if (depthChartItem == null)
            {
                return NotFound(new List<PlayerDto>());
            }

            _depthChartRepository.Remove(depthChartItem);
            await _depthChartRepository.SaveAsync();

            var player = _mapper.Map<PlayerDto>(depthChartItem);

            return Ok(player);
        }


        [HttpGet("getBackups")]
        public async Task<ActionResult<IEnumerable<PlayerDto>>> GetBackups(int teamId, [FromQuery] int positionId, [FromQuery] int playerId)
        {
            var player = await _depthChartRepository.FirstOrDefaultAsync(
                i => i.TeamId == teamId && i.PlayerId == playerId && i.PositionId == positionId,
                "Player");

            if (player == null)
            {
                return NotFound(new List<PlayerDto>());
            }

            var depthChartItems = await _depthChartRepository.GetAllAsync(
                i => i.TeamId == teamId && i.PositionId == positionId && i.PositionDepth > player.PositionDepth,
                i => i.OrderBy(d => d.PositionId)
                      .ThenBy(d => d.PositionDepth),
                "Player");

            if (depthChartItems.Count == 0)
            {
                return NotFound(new List<PlayerDto>());
            }

            return Ok(_mapper.Map<IEnumerable<PlayerDto>>(depthChartItems));
        }


        [HttpGet("getFullDepthChart")]
        public async Task<ActionResult<FullDepthChartDto>> GetFullDepthChart(int teamId)
        {
            var team = await _teamRepository.FindAsync(teamId);

            if (team == null)
            {
                return NotFound();
            }

            var depthChartItems = await _depthChartRepository.GetAllAsync(
                i => i.TeamId == teamId,
                i => i.OrderBy(d => d.Position.PositionType)
                    .ThenBy(d => d.PositionId)
                    .ThenBy(d => d.PositionDepth),
                "Position,Player");

            if (depthChartItems.Count == 0)
            {
                return NotFound();
            }

            var fullDepthChart = new FullDepthChartDto()
            {
                TeamName = team.Name,
                DepthChart = _mapper.Map<IEnumerable<DepthChartDto>>(depthChartItems)
            };

            return Ok(fullDepthChart);
        }
    }
}
