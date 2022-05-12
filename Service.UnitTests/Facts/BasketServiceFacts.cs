using AutoMapper;
using basketbackend.Data.UnitOfWork;
using basketbackend.Presentation.MappingProfiles;
using basketbackend.Service.BasketService;
using basketbackend.Service.BasketTotalCalculator;
using basketbackend.Service.Exceptions;
using Data;
using Moq;
using Service.UnitTests.Helpers;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Service.UnitTests.Facts
{
    public class BasketServiceFacts
    {
        private readonly BasketService _basketService;
        private readonly Mock<IUnitOfWork> _unitOfWork = new();
        private static readonly IBasketTotalNetCalculator _basketTotalNetCalculator = new BasketTotalNetCalculator();
        private static readonly IBasketTotalGrossCalculator _basketTotalGrossCalculator = new BasketTotalGrossCalculator(_basketTotalNetCalculator);
        private readonly IMapper _mapper = new MapperConfiguration(cfg => cfg.AddProfile(new BasketProfile(_basketTotalNetCalculator, _basketTotalGrossCalculator))).CreateMapper();
        private readonly MockedBasketRepository _mockedBasketRepository = new MockedBasketRepository();

        public BasketServiceFacts()
        {
            _basketService = new BasketService(_unitOfWork.Object, _mapper);
            _unitOfWork.Setup(uow => uow.BasketRepository).Returns(_mockedBasketRepository);
        }

        [Fact]
        public async Task CreateBasket_throws_BasketExistsException_for_existing_customer()
        {
            // Arrange
            var testCustomerName = "testName";
            var existingBasket = new Basket(testCustomerName, true);
            _mockedBasketRepository.Add(existingBasket);
            

            // Act & Assert
            await Assert.ThrowsAsync<BasketExistsException>(async () => await _basketService.CreateBasket(testCustomerName, true));
        }

        [Fact]
        public async Task CreateBasket()
        {
            // Arrange
            var testCustomerName = "testName";
            var testPaysVat = true;

            // Act
            var createdBasket = await _basketService.CreateBasket(testCustomerName, testPaysVat);
            var getBasket = await _basketService.Get(createdBasket.Id);

            // Assert
            Assert.Equal(createdBasket, getBasket);
        }

        [Fact]
        public async Task AddItemToBasket_throws_BasketDoesNotExistException()
        {
            // Arrange
            var someBasketId = 99;
            var someItem = "testITem";
            var somePrice = 20.0;

            // Act & Assert
            await Assert.ThrowsAsync<BasketDoesNotExistException>(async () => await _basketService.AddItemToBasket(someBasketId, someItem, somePrice));
        }

        [Fact]
        public async Task AddItemToBasket_throws_BasketAlreadyClosedException_for_adding_into_closed_basket()
        {
            // Arrange
            var someItem = "testItem";
            var somePrice = 20.0;
            var createdBasket = await _basketService.CreateBasket("customer", true);
            await _basketService.CloseBasket(createdBasket.Id, true);

            // Act & Assert
            await Assert.ThrowsAsync<BasketAlreadyClosedException>(async () => await _basketService.AddItemToBasket(createdBasket.Id, someItem, somePrice));
        }

        [Fact]
        public async Task AddItemToBasket()
        {
            // Arrange
            var someItem = "testItem";
            var somePrice = 20.0;
            var createdBasket = await _basketService.CreateBasket("customer", true);

            // Act
            await _basketService.AddItemToBasket(createdBasket.Id, someItem, somePrice);
            var basketAfterAdd = await _basketService.Get(createdBasket.Id);

            // Assert
            var firstItemInBasket = basketAfterAdd.Items.FirstOrDefault();
            Assert.NotNull(firstItemInBasket);
            Assert.Equal(firstItemInBasket.Name, someItem);
            Assert.Equal(firstItemInBasket.Price, somePrice);
            Assert.Equal(firstItemInBasket.BasketId, createdBasket.Id);
        }

        [Fact]
        public async Task CloseBasket_throws_BasketDoesNotExistException()
        {
            // Arrange
            var someBasketId = 99;

            // Act & Assert
            await Assert.ThrowsAsync<BasketDoesNotExistException>(async () => await _basketService.CloseBasket(someBasketId, true));
        }

        [Fact]
        public async Task CloseBasket_throws_BasketAlreadyClosedException_closing_already_closed_basket()
        {
            // Arrange
            var createdBasket = await _basketService.CreateBasket("customer", true);
            await _basketService.CloseBasket(createdBasket.Id, true);

            // Act & Assert
            await Assert.ThrowsAsync<BasketAlreadyClosedException>(async () => await _basketService.CloseBasket(createdBasket.Id, true));
        }

        /// <summary>
        /// Could have combined this with <see cref="CloseBasket_close_for_paid_false"/> using [Theory] and [InlineData]
        /// Did not do it because, in my opinion this would decrease readability
        /// </summary>
        [Fact]
        public async Task CloseBasket_close_for_paid_true()
        {
            // Arrange
            var createdBasket = await _basketService.CreateBasket("customer", true);
            
            // Act
            await _basketService.CloseBasket(createdBasket.Id, true);

            // Assert
            var retrievedBasket = await _basketService.Get(createdBasket.Id);
            Assert.True(retrievedBasket.Closed);
        }

        /// <summary>
        /// Could have combined this with <see cref="CloseBasket_close_for_paid_true"/> using [Theory] and [InlineData]
        /// Did not do it because, in my opinion this would decrease readability
        /// </summary>
        [Fact]
        public async Task CloseBasket_close_for_paid_false()
        {
            // Arrange
            var createdBasket = await _basketService.CreateBasket("customer", true);

            // Act
            await _basketService.CloseBasket(createdBasket.Id, false);

            // Assert
            var retrievedBasket = await _basketService.Get(createdBasket.Id);
            Assert.False(retrievedBasket.Closed);
        }

        [Fact]
        public async Task Get_throws_BasketDoesNotExistException()
        {
            // Arrange
            var someBasketId = 99;

            // Act & Assert
            await Assert.ThrowsAsync<BasketDoesNotExistException>(async () => await _basketService.Get(someBasketId));
        }
    }
}
