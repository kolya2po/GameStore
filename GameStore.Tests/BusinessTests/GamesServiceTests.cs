using FluentAssertions;
using GameStore.BLL.Models;
using GameStore.BLL.Services;
using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;
using Moq;
using NUnit.Framework;

namespace GameStore.Tests.BusinessTests;

[TestFixture]
public class GamesServiceTests
{
    private Mock<IUnitOfWork> _mockUnitOfWork = null!;
    private GamesService _gamesService = null!;

    [SetUp]
    public void InitializeUnitOfWork()
    {
        _mockUnitOfWork = new Mock<IUnitOfWork>();
        _gamesService = new GamesService(_mockUnitOfWork.Object, UnitTestHelper.CreateMapperFromProfile(), null);
    }

    [Test]
    public async Task GetAllAsync_ShouldReturnAllGameModels()
    {
        // Arrange
        _mockUnitOfWork.Setup(u => u.GamesRepository.GetAllWithDetailsAsync())
            .ReturnsAsync(UnitTestHelper.TestGames);

        var expected = new List<GameModel>
        {
            new()
            {
                Id = 1,
                Name = "game 1",
                Description = "desc 1",
                Price = 230.33m,
                AuthorId = 1
            },
            new()
            {
                Id = 2,
                Name = "game 2",
                Description = "desc 2",
                Price = 20.31m,
                AuthorId = 2
            },
            new()
            {
                Id = 3,
                Name = "game 3",
                Description = "desc 3",
                Price = 10.13m,
                AuthorId = 3
            }
        };

        // Act
        var actual = await _gamesService.GetAllAsync();

        // Assert
        actual.Should().BeEquivalentTo(expected, opt =>
        {
            return opt.Excluding(c => c.Comments)
                .Excluding(c => c.Genres);
        });
    }

    [TestCase(1)]
    [TestCase(2)]
    public async Task GetByIdAsync_ShouldReturnGameModelById_WhenGameWithIdExistsInDb(int id)
    {
        // Arrange
        var game = new Game
        {
            Id = 1,
            Name = "game 1",
            Description = "desc 1",
            Price = 230.33m,
            AuthorId = 1
        };
        var expected = new GameModel
        {
            Id = 1,
            Name = "game 1",
            Description = "desc 1",
            Price = 230.33m,
            AuthorId = 1
        };

        _mockUnitOfWork.Setup(u => u.GamesRepository.GetByIdWithDetailsAsync(id))
            .ReturnsAsync(game);

        // Act
        var actual = await _gamesService.GetByIdAsync(id);

        // Assert
        actual.Should().BeEquivalentTo(expected, opt =>
        {
            return opt.Excluding(c => c.Comments)
                .Excluding(c => c.Genres);
        });
    }

    [Test]
    public async Task CreateAsync_ShouldAddNewGameToDb()
    {
        // Arrange
        _mockUnitOfWork.Setup(u => u.GamesRepository.CreateAsync(It.IsAny<Game>()));

        var gameModel = new GameModel
        {
            Name = "game 1",
            Description = "desc 1",
            Price = 231.132m,
            AuthorId = 1
        };

        // Act
        await _gamesService.CreateAsync(gameModel);

        // Assert
        _mockUnitOfWork.Verify(u => u.GamesRepository
            .CreateAsync(It.Is<Game>(g => g.Name == gameModel.Name &&
                                          g.Description == gameModel.Description && 
                                          g.AuthorId == gameModel.AuthorId && 
                                          g.Price == gameModel.Price)), Times.Once);
        _mockUnitOfWork.Verify(u => u.SaveChangesAsync(), Times.Once);
    }

    [Test]
    public async Task DeleteAsync_ShouldDeleteGameByIdFromDb()
    {
        // Arrange
        const int id = 1;
        _mockUnitOfWork.Setup(u => u.GamesRepository.DeleteByIdAsync(It.IsAny<int>()));

        // Act
        await _gamesService.DeleteAsync(id);

        // Assert
        _mockUnitOfWork.Verify(u => u.GamesRepository.DeleteByIdAsync(It.Is<int>(i => i == id)), Times.Once);
        _mockUnitOfWork.Verify(u => u.SaveChangesAsync(), Times.Once);
    }
}