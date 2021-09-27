using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShopBridge.Controllers;
using ShopBridge.Dtos;
using ShopBridge.Profiles;
using System;
using System.Collections.Generic;
using Xunit;

namespace ShopBridgeTestProject
{
    public class ProductControllerTest
    {
        ProductController _productController;
        ProductRepoFack _productRepo;
        private static IMapper _mapper;
        public ProductControllerTest()
        {
            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new ProductProfile());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = mapper;
            }
            _productRepo = new ProductRepoFack();
            _productController = new ProductController(_productRepo, _mapper);
        }
        [Fact]
        public void Get_WhenCalled_ReturnsOkResult()
        {
            // Act
            var okResult = _productController.GetAllProducts();
            // Assert
            Assert.IsType<OkObjectResult>(okResult.Result);
        }
        [Fact]
        public void Get_WhenCalled_ReturnsAllItems()
        {
            // Act
            var okResult = _productController.GetAllProducts().Result as OkObjectResult;
            // Assert
            var items = Assert.IsType<List<ProductReadDto>>(okResult.Value);
            Assert.Equal(3, items.Count);
        }

        [Fact]
        public void GetById_UnknownGuidPassed_ReturnsNotFoundResult()
        {
            Random random = new Random();
            // Act
            var notFoundResult = _productController.GetProductById(random.Next(1, 20));

            // Assert
            Assert.IsType<NotFoundResult>(notFoundResult.Result);
        }

        [Fact]
        public void GetById_ExistingGuidPassed_ReturnsOkResult()
        {
            // Arrange
            Random random = new Random();
            int productid = random.Next(1, 20);
            // Act
            var okResult = _productController.GetProductById(productid);

            // Assert
            Assert.IsType<OkObjectResult>(okResult.Result);
        }

        [Fact]
        public void GetById_ExistingGuidPassed_ReturnsRightItem()
        {
            // Arrange
            Random random = new Random();
            int productid = random.Next(1, 20);

            // Act
            var okResult = _productController.GetProductById(productid).Result as OkObjectResult;

            // Assert
            Assert.IsType<ProductReadDto>(okResult.Value);
            Assert.Equal(productid, (okResult.Value as ProductReadDto).ProductId);
        }

        [Fact]
        public void Add_InvalidObjectPassed_ReturnsBadRequest()
        {
            // Arrange
            var nameMissingItem = new ProductAddDto()
            {
                ProductDescription = "Test UT Product Desc",
                ProductName = "Test UT Product Name",
                ProductPrice = 10,
                ProductQuantity = 1,
                ProductUnit = "KG"
            };
            _productController.ModelState.AddModelError("ProductName", "Required");

            // Act
            var badResponse = _productController.AddProduct(nameMissingItem);

            // Assert
            Assert.IsType<BadRequestObjectResult>(badResponse);
        }


        [Fact]
        public void Add_ValidObjectPassed_ReturnsCreatedResponse()
        {
            // Arrange
            ProductAddDto testItem = new ProductAddDto()
            {
                ProductDescription = "Test UT Product Desc1",
                ProductName = "Test UT Product Name1",
                ProductPrice = 2,
                ProductQuantity = 13,
                ProductUnit = "KG"
            };

            // Act
            var createdResponse = _productController.AddProduct(testItem);

            // Assert
            Assert.IsType<CreatedAtActionResult>(createdResponse);
        }


        //[Fact]
        //public void Add_ValidObjectPassed_ReturnedResponseHasCreatedItem()
        //{
        //    // Arrange
        //    var testItem = new ProductAddDto()
        //    {
        //        ProductDescription = "Test UT Product Desc2",
        //        ProductName = "Test UT Product Name2",
        //        ProductPrice = 2,
        //        ProductQuantity = 13,
        //        ProductUnit = "KG"
        //    };

        //    // Act
        //    var createdResponse = _productController.AddProduct(testItem) as CreatedAtActionResult;
        //    var item = createdResponse.Value as ProductReadDto;

        //    // Assert
        //    Assert.IsType<ProductReadDto>(item);
        //    Assert.Equal("Test UT Product Desc2", item.ProductName);
        //}

        [Fact]
        public void Remove_NotExistingGuidPassed_ReturnsNotFoundResponse()
        {
            // Arrange
            Random random = new Random();
            int productid = random.Next(1, 20);

            // Act
            var badResponse = _productController.RemoveProduct(productid);

            // Assert
            Assert.IsType<NotFoundResult>(badResponse);
        }

        [Fact]
        public void Remove_ExistingGuidPassed_ReturnsOkResult()
        {
            // Arrange
            var existingGuid = new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200");

            // Act
            var okResponse = _productController.RemoveProduct(1);

            // Assert
            Assert.IsType<OkResult>(okResponse);
        }
        //[Fact]
        //public void Remove_ExistingGuidPassed_RemovesOneItem()
        //{
        //    // Arrange
        //    var existingGuid = new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200");

        //    // Act
        //    var okResponse = _productController.RemoveProduct(1);

        //    // Assert
        //    Assert.Equal(2, _productRepo.GetAllProducts().Count());
        //}
    }
}
