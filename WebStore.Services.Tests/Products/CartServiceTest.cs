using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.Domain;
using WebStore.Domain.DTO.Products;
using WebStore.Domain.Entities;
using WebStore.Domain.ViewModels;
using WebStore.Interfaces.Services;
using WebStore.Services.Products;
using Assert = Xunit.Assert;

namespace WebStore.Services.Tests.Products
{
    [TestClass]
    public class CartServiceTest 
    {
        private Cart _cart;
        private CartViewModel _cartViewModel;

        private Mock<IProductData> _productDataMock;
        private Mock<ICartStore> _cartStoreMock;
        private ICartService _cartService;

        [TestInitialize]
        public void Initialize() {
            _cart = new Cart { 
                Items = new List<CartItem>{ 
                    new CartItem{ ProductId = 1, Quantity = 1 },
                    new CartItem{ ProductId = 2, Quantity = 2 }
                }
            };

            _cartViewModel = new CartViewModel() { 
                Items = new List<(ProductViewModel product, int quantity)>() { 
                    (new ProductViewModel(){ Price = 11m }, 1),
                    (new ProductViewModel(){ Price = 22m }, 2)
                }
            };

            _productDataMock = new Mock<IProductData>();
            _productDataMock.Setup(x => x.GetProducts(It.IsAny<ProductFilter>()))
                .Returns(() => new ProductDTO[]
                {
                    new ProductDTO(1, "Product_1", 1, 1.1m, "img1.png", new BrandDTO(11, "Brand_11", 1, 1), new SectionDTO(1, "Section_1", 1, null)),
                    new ProductDTO(2, "Product_2", 2, 2.2m, "img2.png", new BrandDTO(22, "Brand_22", 2, 2), new SectionDTO(2, "Section_2", 2, null))
                }
            );

            _cartStoreMock = new Mock<ICartStore>();
            _cartStoreMock.Setup(x => x.Cart).Returns(_cart);

            _cartService = new CartService(_productDataMock.Object, _cartStoreMock.Object);
        }

        [TestMethod]
        public void Cart_Class_ItemsCount_Returns_Correct_Quantity() {
            //Arrange
            Cart cart = _cart;
            int expectedCount = 3;

            //Act
            int actualCount = cart.ItemsCount;

            //Assert
            Assert.Equal(expectedCount, actualCount);
        }

        [TestMethod]
        public void CartViewModels_TotalPrice_Returns_CorrectValues() {
            //Arrange
            CartViewModel cartViewModel = _cartViewModel;
            int expectedTotalPrice = 55;

            //Act
            decimal actualTotalPrice = _cartViewModel.TotalPrice();

            //Assert
            Assert.Equal(expectedTotalPrice, actualTotalPrice);
        }

        [TestMethod]
        public void CartService_AddToCart_WorkCorrect() {
            _cart.Items.Clear();

            int expectedProductId = 42;

            _cartService.AddToCart(expectedProductId);

            ICollection<CartItem> cartItems = _cart.Items;

            Assert.Single(cartItems);
            Assert.Equal(expectedProductId, cartItems.First().ProductId);
        }

        [TestMethod]
        public void CartService_RemoveFromCart_WorkCorrect()
        {
            int product_id_to_remove = 1;
            int expected_product_id = 2;

            _cartService.RemoveFromCart(product_id_to_remove);

            ICollection<CartItem> cartItems = _cart.Items;

            Assert.Single(cartItems);
            Assert.Equal(expected_product_id, cartItems.Single().ProductId);
        }

        [TestMethod]
        public void CartService_DecrementFromCart_WorkCorrect()
        {
            int product_id_1 = 1;
            int product_id_2 = 2;
            int expected_product_id_2_quantity = 1;

            _cartService.DecrementFromCart(product_id_1);
            _cartService.DecrementFromCart(product_id_2);            

            Assert.Single(_cart.Items);
            Assert.Equal(product_id_2, _cart.Items.Single().ProductId);
            Assert.Equal(expected_product_id_2_quantity, _cart.Items.Single().Quantity);
        }

        [TestMethod]
        public void CartService_Clear_WorkCorrect()
        {
            _cartService.Clear();

            Assert.Empty(_cart.Items);
        }

        [TestMethod]
        public void CartService_TransformToViewModel_Returns_CorrectModel()
        {
            int expectedItemsCount = 2;
            decimal expectedItemPrice = 2.2m;

            var viewModel = _cartService.TransformToViewModel();

            Assert.Equal(expectedItemsCount, viewModel.Items.Count());
            Assert.Equal(expectedItemPrice, viewModel.Items.ElementAt(1).product.Price);
        }
    }
}
