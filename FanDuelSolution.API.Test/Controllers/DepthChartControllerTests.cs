using AutoMapper;
using FanDuelSolution.API.Entities;
using FanDuelSolution.API.Models;
using FanDuelSolution.API.Repository.Interface;
using FanDuelSolution.API.Test.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using Moq;
using System.Linq.Expressions;

namespace FanDuelSolution.API.Controllers.Tests
{
    public class DepthChartControllerTests
    {
        private readonly Mock<IDepthChartRepository> _mockDepthChartRepository;
        private readonly Mock<ITeamRepository> _mockTeamRepository;
        private readonly IMapper _mapper;
        private readonly DepthChartController _controller;

        public DepthChartControllerTests()
        {
            _mockDepthChartRepository = new Mock<IDepthChartRepository>();
            _mockTeamRepository = new Mock<ITeamRepository>();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile<MappingProfile>();
            });

            _mapper = mappingConfig.CreateMapper();

            _controller = new DepthChartController(_mockDepthChartRepository.Object, _mockTeamRepository.Object, _mapper);
        }

        [Fact]
        public async Task AddPlayerToDepthChart_ReturnsOkResult()
        {
            // Arrange
            int teamId = 1, positionId = 1, playerId = 1, positionDepth = 0;
            var depthChartItems = new List<DepthChart>();

            _mockDepthChartRepository.Setup(repo => repo.GetAllAsync(It.IsAny<Expression<Func<DepthChart, bool>>>(), It.IsAny<Func<IQueryable<DepthChart>, IOrderedQueryable<DepthChart>>>(), It.IsAny<string>(), true))
                .ReturnsAsync(depthChartItems);

            _mockDepthChartRepository.Setup(repo => repo.BeginTransactionAsync())
                .ReturnsAsync(new Mock<IDbContextTransaction>().Object);

            // Act
            var result = await _controller.AddPlayerToDepthChart(teamId, positionId, playerId, positionDepth);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<DepthChartDto>(okResult.Value);
            Assert.Equal(playerId, returnValue.PlayerId);
        }

        [Fact]
        public async Task RemovePlayerFromDepthChart_ReturnsOkResult()
        {
            // Arrange
            int teamId = 1, positionId = 1, playerId = 1;
            var depthChartItem = new DepthChart { TeamId = teamId, PositionId = positionId, PlayerId = playerId };

            _mockDepthChartRepository.Setup(repo => repo.FirstOrDefaultAsync(It.IsAny<Expression<Func<DepthChart, bool>>>(), It.IsAny<string>(), It.IsAny<bool>()))
                .ReturnsAsync(depthChartItem);

            // Act
            var result = await _controller.RemovePlayerFromDepthChart(teamId, positionId, playerId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<PlayerDto>(okResult.Value);
        }

        [Fact]
        public async Task GetBackups_ReturnsOkResult()
        {
            // Arrange
            int teamId = 1, positionId = 1, playerId = 1;
            var depthChartItem = new DepthChart { TeamId = teamId, PositionId = positionId, PlayerId = playerId, PositionDepth = 1 };
            var depthChartItems = new List<DepthChart> {
                new DepthChart { TeamId = teamId, PositionId = positionId, PlayerId = 2, PositionDepth = 2 },
                new DepthChart { TeamId = teamId, PositionId = positionId, PlayerId = 3, PositionDepth = 3 }
            };

            _mockDepthChartRepository.Setup(repo => repo.FirstOrDefaultAsync(It.IsAny<Expression<Func<DepthChart, bool>>>(), It.IsAny<string>(), It.IsAny<bool>()))
                .ReturnsAsync(depthChartItem);

            _mockDepthChartRepository.Setup(repo => repo.GetAllAsync(It.IsAny<Expression<Func<DepthChart, bool>>>(), It.IsAny<Func<IQueryable<DepthChart>, IOrderedQueryable<DepthChart>>>(), It.IsAny<string>(), It.IsAny<bool>()))
                .ReturnsAsync(depthChartItems);

            // Act
            var result = await _controller.GetBackups(teamId, positionId, playerId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsAssignableFrom<IEnumerable<PlayerDto>>(okResult.Value);
            Assert.Equal(2, returnValue.Count());
        }

        [Fact]
        public async Task GetFullDepthChart_ReturnsOkResult()
        {
            // Arrange
            int teamId = 1;
            var team = new Team("Team1") { Id = teamId };
            var depthChartItems = new List<DepthChart> {
                new DepthChart { TeamId = teamId, PositionId = 1, PlayerId = 1, PositionDepth = 1, Player = new Player("Player1", "1") },
                new DepthChart { TeamId = teamId, PositionId = 2, PlayerId = 2, PositionDepth = 1, Player = new Player("Player2", "2") }
            };

            _mockTeamRepository.Setup(repo => repo.FindAsync(teamId)).ReturnsAsync(team);
            _mockDepthChartRepository.Setup(repo => repo.GetAllAsync(It.IsAny<Expression<Func<DepthChart, bool>>>(), It.IsAny<Func<IQueryable<DepthChart>, IOrderedQueryable<DepthChart>>>(), It.IsAny<string>(), It.IsAny<bool>()))
                .ReturnsAsync(depthChartItems);

            // Act
            var result = await _controller.GetFullDepthChart(teamId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<FullDepthChartDto>(okResult.Value);
            Assert.Equal("Team1", returnValue.TeamName);
        }
    }
}