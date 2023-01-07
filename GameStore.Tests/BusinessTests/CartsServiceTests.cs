using GameStore.BLL.Infrastructure;
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
    [Test]
    public async Task CreateAsync_CreatesNewCart()
    {
        // Arrange
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        mockUnitOfWork.Setup(u => u.CartsRepository.CreateAsync(It.IsAny<Cart>()));
        var cartsService = new CartsService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperFromProfile(), null);
        const string userName = "username1";

        // Act
        await cartsService.CreateAsync(userName);

        // Assert
        mockUnitOfWork.Verify(u => u.CartsRepository
            .CreateAsync(It.Is<Cart>(c => c.UserName == userName)), Times.Once);
        mockUnitOfWork.Verify(u => u.SaveChangesAsync(), Times.Once);
    }

    [Test]
    public async Task AddGameAsync_AddsNewGameToCart()
    {
        // Arrange
        const int cartId = 1;
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        mockUnitOfWork.Setup(u => u.CartsRepository.GetByIdWithDetailsAsync(cartId))
            .ReturnsAsync(GetTestCart);
        mockUnitOfWork.Setup(u => u.CartItemsRepository
            .GetByIdAsync(It.IsAny<int>(), It.IsAny<int>()));
        mockUnitOfWork.Setup(u => u.CartItemsRepository
            .CreateAsync(It.Is<CartItem>(opt => opt.CartId.Equals(cartId))));

        var cartItemsService = new CartItemsService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperFromProfile());
        var cartsService = new CartsService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperFromProfile(), cartItemsService);

        // Act
        await cartsService.AddGameAsync(cartId, GetTestGameModel);

        // Assert
        mockUnitOfWork.Verify(u => u.CartItemsRepository
            .CreateAsync(It.Is<CartItem>(c => c.CartId == cartId)), Times.Once);
        mockUnitOfWork.Verify(u => u.SaveChangesAsync(), Times.Once);
    }

    [Test]
    public async Task AddGameAsync_IncreaseQuantityIfCartItemExists()
    {
        // Arrange
        const int cartId = 1;
        const int gameId = 1;
        const int expectedQuantity = 6;
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        mockUnitOfWork.Setup(u => u.CartsRepository.GetByIdWithDetailsAsync(cartId))
            .ReturnsAsync(GetTestCart);
        mockUnitOfWork.Setup(u => u.CartItemsRepository
            .GetByIdAsync(cartId, gameId)).ReturnsAsync(GetTestCartItem);
        mockUnitOfWork.Setup(u => u.CartItemsRepository
            .CreateAsync(It.Is<CartItem>(opt => opt.CartId.Equals(cartId))));

        var cartItemsService = new CartItemsService(mockUnitOfWork.Object,
            UnitTestHelper.CreateMapperFromProfile());
        var cartsService = new CartsService(mockUnitOfWork.Object,
            UnitTestHelper.CreateMapperFromProfile(), cartItemsService);

        // Act
        await cartsService.AddGameAsync(cartId, GetTestGameModel);
        var cartItem = await cartItemsService.GetCartItemByIdAsync(cartId, gameId);

        // Assert
        Assert.That(cartItem.Quantity, Is.EqualTo(expectedQuantity));
        mockUnitOfWork.Verify(u => u.SaveChangesAsync(), Times.Exactly(2));
    }

    [Test]
    public void AddGameAsync_ThrowsExceptionIfCartDoesNotExist()
    {
        // Arrange
        const int cartId = 1;
        var mockUnitOfWork = new Mock<IUnitOfWork>();

        var cartsService = new CartsService(mockUnitOfWork.Object,
            UnitTestHelper.CreateMapperFromProfile(), null);
        mockUnitOfWork.Setup(u => u.CartsRepository.GetByIdWithDetailsAsync(cartId))
            .ReturnsAsync((Cart)null);

        // Assert
        Assert.ThrowsAsync<GameStoreException>(async () =>
        {
            await cartsService.AddGameAsync(cartId, null);
        });
    }

    [Test]
    public async Task RemoveItemAsync_RemovesCartItemFromCart()
    {
        // Arrange
        const int cartId = 1;
        const int gameId = 1;
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        mockUnitOfWork.Setup(u => u.CartItemsRepository.DeleteByIdAsync(It.IsAny<int>(), It.IsAny<int>()));

        var cartItemsService = new CartItemsService(mockUnitOfWork.Object,
            UnitTestHelper.CreateMapperFromProfile());
        var cartsService = new CartsService(mockUnitOfWork.Object,
            UnitTestHelper.CreateMapperFromProfile(), cartItemsService);

        // Act
        await cartsService.RemoveItemAsync(cartId, gameId);

        // Assert
        mockUnitOfWork.Verify(u => u.CartItemsRepository
            .DeleteByIdAsync(cartId, gameId), Times.Once);
        mockUnitOfWork.Verify(u => u.SaveChangesAsync(), Times.Once);
    }

    [Test]
    public async Task UpdateAsync_UpdatesCart()
    {
        // Arrange
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        mockUnitOfWork.Setup(u => u.CartsRepository.Update(It.IsAny<Cart>()));

        var cartsService = new CartsService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperFromProfile(), null);

        // Act
        await cartsService.UpdateAsync(GetTestCartModel);

        // Assert
        mockUnitOfWork.Verify(u => u.CartsRepository
            .Update(It.Is<Cart>(c => c.Id == GetTestCartModel.Id
                                     && c.UserName == GetTestCartModel.UserName)), Times.Once);
        mockUnitOfWork.Verify(u => u.SaveChangesAsync(), Times.Once);
    }

    [Test]
    public async Task DeleteByIdAsyncAsync_DeletesCartById()
    {
        // Arrange
        const int cartId = 1;
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        mockUnitOfWork.Setup(u => u.CartsRepository.DeleteByIdAsync(It.IsAny<int>()));

        var cartsService = new CartsService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperFromProfile(), null);

        // Act
        await cartsService.DeleteByIdAsync(cartId);

        // Assert
        mockUnitOfWork.Verify(u => u.CartsRepository
            .DeleteByIdAsync(It.Is<int>(i => i == cartId)), Times.Once);
        mockUnitOfWork.Verify(u => u.SaveChangesAsync(), Times.Once);
    }


    private static CartItem GetTestCartItem =>
        new()
        {
            CartId = 1,
            GameId = 1,
            Quantity = 5
        };

    private static Cart GetTestCart =>
        new()
        {
            Id = 1,
            UserName = "username1"
        };

    private static GameModel GetTestGameModel =>
        new()
        {
            Id = 1,
            Name = "game1",
            Description = "game1 desc",
            Price = 100m
        };

    private static CartModel GetTestCartModel =>
            new ()
            {
                Id = 1,
                UserName = "username1"
            };
}