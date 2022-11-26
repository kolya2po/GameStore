using FluentAssertions;
using GameStore.DAL;
using GameStore.DAL.Entities;
using GameStore.DAL.Repositories;
using NUnit.Framework;

namespace GameStore.Tests.DataTests;

[TestFixture]
public class GamesRepositoryTests
{
    private GameStoreDbContext _context = null!;
    private GamesRepository _repository = null!;

    [SetUp]
    public void InitializeDbContextAndGamesRepository()
    {
        _context = new GameStoreDbContext(UnitTestHelper.GetDbContextOptions());
        _repository = new GamesRepository(_context);
    }

    [TearDown]
    public async Task CleanupDbContext()
    {
        await _context.Database.EnsureDeletedAsync();
    }

    [TestCase(1)]
    [TestCase(2)]
    [TestCase(3)]
    public async Task GetByIdAsync_ShouldReturnGameById_WhenGameExists(int id)
    {
        // Arrange
        var expected = UnitTestHelper.GetTestGames().First(g => g.Id == id);

        // Act
        var actual = await _repository.GetByIdAsync(id);

        // Assert
        actual.Should().BeEquivalentTo(expected);
    }

    [Test]
    public async Task GetAllAsync_ShouldReturnAllGamesFromDb()
    {
        // Arrange
        var expected = UnitTestHelper.GetTestGames();

        // Act
        var actual = await _repository.GetAllAsync();

        // Assert
        actual.Should().BeEquivalentTo(expected);
    }

    [Test]
    public async Task CreateAsync_ShouldAddNewGameToDb()
    {
        // Arrange
        var initialCount = UnitTestHelper.GetTestGames().Count();

        var newGame = new Game
        {
            Id = 4,
            Name = "game 4",
            AuthorId = 1
        };

        // Act
        await _repository.CreateAsync(newGame);
        await _context.SaveChangesAsync();

        // Assert
        _context.Games.Count().Should().Be(initialCount + 1);
    }

    [TestCase(1)]
    [TestCase(2)]
    public async Task DeleteByIdAsync_ShouldDeleteGameByIdFromDb(int id)
    {
        // Arrange
        var initialCount = UnitTestHelper.GetTestGames().Count();

        // Act
        await _repository.DeleteByIdAsync(id);
        await _context.SaveChangesAsync();

        // Assert
        _context.Games.Count().Should().Be(initialCount - 1);
    }

    [TestCase(1)]
    [TestCase(2)]
    public async Task Delete_ShouldDeleteGameFromDb(int id)
    {
        // Arrange
        var initialCount = UnitTestHelper.GetTestGames().Count();
        var gameToDelete = UnitTestHelper.GetTestGames().First(c => c.Id == id);

        // Act
        _repository.Delete(gameToDelete);
        await _context.SaveChangesAsync();

        // Assert
        _context.Games.Count().Should().Be(initialCount - 1);
    }

    [TestCase(1)]
    [TestCase(2)]
    public async Task Update_ShouldUpdateGamesProperties(int id)
    {
        // Arrange
        var oldGame = UnitTestHelper.GetTestGames().First(c => c.Id == id);

        var newGame = new Game
        {
            Id = id,
            Name = $"new game {id}",
            Description = $"desc {id}",
            Price = 230.33m,
            AuthorId = 1
        };

        // Act
        _repository.Update(newGame);
        await _context.SaveChangesAsync();

        // Assert
        newGame.Should().NotBeEquivalentTo(oldGame);
    }


}