using FanDuelSolution.API.DbContexts;
using FanDuelSolution.API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Moq;

namespace FanDuelSolution.API.Repository.Tests
{
    public class RepositoryTests
    {
        private readonly Mock<AppDbContext> _mockContext;
        private readonly Mock<DbSet<DepthChart>> _mockDbSet;
        private readonly Repository<DepthChart> _repository;
        private readonly Mock<DatabaseFacade> _mockDbFacade;
        public RepositoryTests()
        {
            _mockContext = new Mock<AppDbContext>();
            _mockDbFacade = new Mock<DatabaseFacade>(_mockContext.Object);
            _mockDbSet = new Mock<DbSet<DepthChart>>();
            _mockContext.Setup(m => m.Set<DepthChart>()).Returns(_mockDbSet.Object);
            _mockContext.Setup(m => m.Database).Returns(_mockDbFacade.Object);
            _repository = new Repository<DepthChart>(_mockContext.Object);
        }

        [Fact]
        public async Task BeginTransactionAsync_ShouldBeginTransaction()
        {
            // Arrange
            var mockTransaction = new Mock<IDbContextTransaction>();
            _mockContext.Setup(m => m.Database.BeginTransactionAsync(default)).ReturnsAsync(mockTransaction.Object);

            // Act
            var result = await _repository.BeginTransactionAsync();

            // Assert
            _mockContext.Verify(m => m.Database.BeginTransactionAsync(default), Times.Once);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task AddAsync_ShouldAddEntity()
        {
            // Arrange
            var testDepthChart = GetTestDepthCharts().First();

            // Act
            await _repository.AddAsync(testDepthChart);

            // Assert
            _mockDbSet.Verify(m => m.AddAsync(testDepthChart, default), Times.Once);
        }

        [Fact]
        public async Task FindAsync_ShouldFindEntityById()
        {
            // Arrange
            var testDepthChart = GetTestDepthCharts().First();
            _mockDbSet.Setup(m => m.FindAsync(1)).ReturnsAsync(testDepthChart);

            // Act
            var result = await _repository.FindAsync(1);

            // Assert
            _mockDbSet.Verify(m => m.FindAsync(1), Times.Once);
            Assert.Equal(testDepthChart, result);
        }


        [Fact]
        public void Remove_ShouldRemoveEntity()
        {
            // Arrange
            var testDepthChart = GetTestDepthCharts().First();

            // Act
            _repository.Remove(testDepthChart);

            // Assert
            _mockDbSet.Verify(m => m.Remove(testDepthChart), Times.Once);
        }

        [Fact]
        public async Task SaveAsync_ShouldSaveChanges()
        {
            // Act
            await _repository.SaveAsync();

            // Assert
            _mockContext.Verify(m => m.SaveChangesAsync(default), Times.Once);
        }

        private List<DepthChart> GetTestDepthCharts()
        {
            var depthCharts = new List<DepthChart>
            {
                new DepthChart
                {
                    Id = 1,
                    TeamId = 1,
                    PositionId = 1,
                    PlayerId = 1,
                    PositionDepth = 1,
                },
                new DepthChart
                {
                    Id = 2,
                    TeamId = 1,
                    PositionId = 1,
                    PlayerId = 1,
                    PositionDepth = 2,
                }
            };
            return depthCharts;
        }
    }
}