using GameStore.BLL.Infrastructure;
using GameStore.BLL.Interfaces;
using GameStore.BLL.Models;
using GameStore.BLL.Services;
using GameStore.DAL.Entities;
using GameStore.DAL.Interfaces;
using Moq;
using NUnit.Framework;

namespace GameStore.Tests.BusinessTests;

[TestFixture]
public class CartsServiceTests
{
    private Mock<IUnitOfWork> _mockUnitOfWork = null!;

    [SetUp]
    public void InitializeUnitOfWork()
    {
        _mockUnitOfWork = new Mock<IUnitOfWork>();
    }

    [Test]
    public async Task CreateAsync_ShouldCreateNewCart()
    {
        // Arrange
        _mockUnitOfWork.Setup(u => u.CartsRepository.CreateAsync(It.IsAny<Cart>()));
        var cartsService = new CartsService(_mockUnitOfWork.Object, UnitTestHelper.CreateMapperFromProfile(), null);
        const string userName = "username1";

        // Act
        var actual = await cartsService.CreateAsync(userName);

        // Assert
        Assert.That(actual.UserName, Is.EqualTo(userName));
        _mockUnitOfWork.Verify(u => u.CartsRepository
            .CreateAsync(It.Is<Cart>(c => c.UserName == userName)), Times.Once);
        _mockUnitOfWork.Verify(u => u.SaveChangesAsync(), Times.Once);
    }

    [Test]
    public async Task AddGameAsync_ShouldAddNewGameToCart_WhenCartDoesNotHaveGameInIt()
    {
        // Arrange
        const int cartId = 1;
        var gameModel = new GameModel
        {
            Id = 1,
            Name = "game1",
            Description = "game1 desc",
            Price = 100m
        };

        var mockCartItemsService = new Mock<ICartItemsService>();
        mockCartItemsService.Setup(s => s.CreateAsync(It.IsAny<int>(), It.IsAny<GameModel>()));

        // Looks like this setup is useless as GetByIdAsync is not used in AddGameAsync.
        _mockUnitOfWork.Setup(u => u.CartsRepository.GetByIdWithDetailsAsync(cartId))
            .ReturnsAsync(TestCart);

        var cartsService = new CartsService(_mockUnitOfWork.Object, UnitTestHelper.CreateMapperFromProfile(), mockCartItemsService.Object);

        // Act
        await cartsService.AddGameAsync(cartId, gameModel);

        // Assert
        mockCartItemsService.Verify(s => s.CreateAsync(It.Is<int>(c => c == cartId), It.Is<GameModel>(g => g.Id == gameModel.Id)), Times.Once);
    }

    [Test]
    public async Task AddGameAsync_ShouldIncreaseQuantityOfCartItem_WhenGameAlreadyInCart()
    {
        // Arrange
        const int cartId = 1;
        const int gameId = 1;
        var gameModel = new GameModel
        {
            Id = 1,
            Name = "game1",
            Description = "game1 desc",
            Price = 100m
        };

        _mockUnitOfWork.Setup(u => u.CartsRepository.GetByIdWithDetailsAsync(cartId))
            .ReturnsAsync(TestCart);

        var mockCartItemsService = new Mock<ICartItemsService>();
        mockCartItemsService.Setup(s => s.GetCartItemByIdAsync(cartId, gameId))
            .ReturnsAsync(TestCartItemModel);
        mockCartItemsService.Setup(s => s.IncreaseQuantityAsync(cartId, gameId));

        var cartsService = new CartsService(_mockUnitOfWork.Object,
            UnitTestHelper.CreateMapperFromProfile(), mockCartItemsService.Object);

        // Act
        await cartsService.AddGameAsync(cartId, gameModel);

        // Assert
        mockCartItemsService.Verify(s => s.IncreaseQuantityAsync(cartId, gameId), Times.Once);
        _mockUnitOfWork.Verify(u => u.SaveChangesAsync(), Times.Once);
    }

    [Test]
    public void AddGameAsync_ShouldThrowException_WhenCartDoesNotExist()
    {
        // Arrange
        const int cartId = 1;

        var cartsService = new CartsService(_mockUnitOfWork.Object,
            UnitTestHelper.CreateMapperFromProfile(), null);
        _mockUnitOfWork.Setup(u => u.CartsRepository.GetByIdWithDetailsAsync(cartId))
            .ReturnsAsync((Cart?)null);

        // Assert
        Assert.ThrowsAsync<GameStoreException>(async () =>
        {
            await cartsService.AddGameAsync(cartId, null);
        });
    }

    [Test]
    public async Task RemoveItemAsync_ShouldRemoveCartItemFromCart()
    {
        // Arrange
        const int cartId = 1;
        const int gameId = 1;
        _mockUnitOfWork.Setup(u => u.CartItemsRepository.DeleteByIdAsync(It.IsAny<int>(), It.IsAny<int>()));

        var cartItemsService = new CartItemsService(_mockUnitOfWork.Object,
            UnitTestHelper.CreateMapperFromProfile());
        var cartsService = new CartsService(_mockUnitOfWork.Object,
            UnitTestHelper.CreateMapperFromProfile(), cartItemsService);

        // Act
        await cartsService.RemoveItemAsync(cartId, gameId);

        // Assert
        _mockUnitOfWork.Verify(u => u.CartItemsRepository
            .DeleteByIdAsync(cartId, gameId), Times.Once);
        _mockUnitOfWork.Verify(u => u.SaveChangesAsync(), Times.Once);
    }

    [Test]
    public async Task UpdateAsync_ShouldUpdateCart()
    {
        // Arrange
        _mockUnitOfWork.Setup(u => u.CartsRepository.Update(It.IsAny<Cart>()));

        var cartsService = new CartsService(_mockUnitOfWork.Object, UnitTestHelper.CreateMapperFromProfile(), null);

        // Act
        await cartsService.UpdateAsync(TestCartModel);

        // Assert
        _mockUnitOfWork.Verify(u => u.CartsRepository
            .Update(It.Is<Cart>(c => c.Id == TestCartModel.Id
                                     && c.UserName == TestCartModel.UserName)), Times.Once);
        _mockUnitOfWork.Verify(u => u.SaveChangesAsync(), Times.Once);
    }

    [Test]
    public async Task DeleteByIdAsyncAsync_ShouldDeleteCartById()
    {
        // Arrange
        const int cartId = 1;
        _mockUnitOfWork.Setup(u => u.CartsRepository.DeleteByIdAsync(It.IsAny<int>()));

        var cartsService = new CartsService(_mockUnitOfWork.Object, UnitTestHelper.CreateMapperFromProfile(), null);

        // Act
        await cartsService.DeleteByIdAsync(cartId);

        // Assert
        _mockUnitOfWork.Verify(u => u.CartsRepository
            .DeleteByIdAsync(It.Is<int>(i => i == cartId)), Times.Once);
        _mockUnitOfWork.Verify(u => u.SaveChangesAsync(), Times.Once);
    }


    private static CartItemModel TestCartItemModel =>
        new()
        {
            CartId = 1,
            GameId = 1,
            Quantity = 5
        };

    private static Cart TestCart =>
        new()
        {
            Id = 1,
            UserName = "username1"
        };

    private static CartModel TestCartModel =>
            new ()
            {
                Id = 1,
                UserName = "username1"
            };
}