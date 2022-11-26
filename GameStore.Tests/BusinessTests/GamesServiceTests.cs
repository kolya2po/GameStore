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
    [Test]
    public async Task GetAllAsync_ShouldReturnAllGameModels()
    {
        // Arrange
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        mockUnitOfWork.Setup(u => u.GamesRepository.GetAllWithDetailsAsync())
            .ReturnsAsync(UnitTestHelper.GetTestGames);
        var gamesService = new GamesService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperFromProfile(), null);

        var expected = GetTestGameModels;

        // Act
        var actual = await gamesService.GetAllAsync();

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
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        mockUnitOfWork.Setup(u => u.GamesRepository.GetByIdWithDetailsAsync(id))
            .ReturnsAsync(UnitTestHelper.GetTestGames.First(g => g.Id == id));
        var gamesService = new GamesService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperFromProfile(), null);
        var expected = GetTestGameModels.First(c => c.Id == id);

        // Act
        var actual = await gamesService.GetByIdAsync(id);

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
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        mockUnitOfWork.Setup(u => u.GamesRepository.CreateAsync(It.IsAny<Game>()));
        var gamesService = new GamesService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperFromProfile(), null);

        var gameModel = new GameModel
        {
            Name = "game 1",
            Description = "desc 1",
            Price = 231.132m,
            AuthorId = 1
        };

        // Act
        await gamesService.CreateAsync(gameModel);

        // Assert
        mockUnitOfWork.Verify(u => u.GamesRepository
            .CreateAsync(It.Is<Game>(g => g.Name == gameModel.Name &&
                                          g.Description == gameModel.Description && 
                                          g.AuthorId == gameModel.AuthorId && 
                                          g.Price == gameModel.Price)), Times.Once);
        mockUnitOfWork.Verify(u => u.SaveChangesAsync(), Times.Once);
    }

    [Test]
    public async Task DeleteAsync_ShouldDeleteGameByIdFromDb()
    {
        // Arrange
        const int id = 1;
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        mockUnitOfWork.Setup(u => u.GamesRepository.DeleteByIdAsync(It.IsAny<int>()));
        var gamesService = new GamesService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperFromProfile(), null);

        // Act
        await gamesService.DeleteAsync(id);

        // Assert
        mockUnitOfWork.Verify(u => u.GamesRepository.DeleteByIdAsync(It.Is<int>(i => i == id)), Times.Once);
        mockUnitOfWork.Verify(u => u.SaveChangesAsync(), Times.Once);
    }


    private static IEnumerable<GameModel> GetTestGameModels =>
        new[]
        {
            new GameModel
            {
                Id = 1,
                Name = "game 1",
                Description = "desc 1",
                Price = 230.33m,
                AuthorId = 1
            },
            new GameModel
            {
                Id = 2,
                Name = "game 2",
                Description = "desc 2",
                Price = 20.31m,
                AuthorId = 2
            },
            new GameModel
            {
                Id = 3,
                Name = "game 3",
                Description = "desc 3",
                Price = 10.13m,
                AuthorId = 3
            }
        };
}