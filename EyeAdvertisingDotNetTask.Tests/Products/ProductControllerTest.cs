using EyeAdvertisingDotNetTask.API.Controllers;
using EyeAdvertisingDotNetTask.Core.Constants;
using EyeAdvertisingDotNetTask.Core.Dtos.Categories;
using EyeAdvertisingDotNetTask.Core.Dtos.General;
using EyeAdvertisingDotNetTask.Core.Dtos.Products;
using EyeAdvertisingDotNetTask.Core.Exceptions;
using EyeAdvertisingDotNetTask.Core.ViewModels.Categories;
using EyeAdvertisingDotNetTask.Core.ViewModels.General;
using EyeAdvertisingDotNetTask.Core.ViewModels.Products;
using EyeAdvertisingDotNetTask.Data.DbEntities;
using EyeAdvertisingDotNetTask.Infrastructure.Services.Categories;
using EyeAdvertisingDotNetTask.Tests.Categories;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using NuGet.Protocol.Plugins;

namespace EyeAdvertisingDotNetTask.Tests.Products
{
    public class ProductControllerTest
    {
        private readonly ProductControllerFake _productControllerFake;
        private readonly IProductServiceFake _productService;

        public ProductControllerTest()
        {
            _productService = new ProductServiceFake();
            _productControllerFake = new ProductControllerFake(_productService);
        }

        [Fact]
        public void GetAll_WhenCalled_ReturnsOkResult()
        {
            // Act
            var okResult = _productControllerFake.GetAll();

            // Assert
            Assert.IsType<OkObjectResult>(okResult as OkObjectResult);
        }

        [Fact]
        public void GetAll_WhenCalled_ReturnsAllItems()
        {
            // Act
            var okResult = _productControllerFake.GetAll() as OkObjectResult;

            // Assert
            var responseObj = Assert.IsType<ApiResponseViewModel>(okResult?.Value);
            var pagingResultObj = Assert.IsType<PagingResultViewModel<ProductViewModel>>(responseObj?.Data);
            var dataList = Assert.IsType<List<ProductViewModel>>(pagingResultObj?.Data);

            Assert.Equal(3, dataList?.Count);
        }

        [Fact]
        public void GetById_UnknownIdPassed_ReturnsNotFoundResult()
        {
            try
            {
                // Arrange
                var testId = 58;

                // Act
                var notFoundResult = _productControllerFake.Get(testId);

                // Assert
                Assert.Fail("An exception should have been thrown");
            }
            catch (EntityNotFoundException ex)
            {
                // Assert
                Assert.Equal("Entity Not Found", ex.Message);
            }
        }

        [Fact]
        public void GetById_ExistingIdPassed_ReturnsOkResult()
        {
            // Arrange
            var testId = 1;

            // Act
            var okResult = _productControllerFake.Get(testId);

            // Assert
            Assert.IsType<OkObjectResult>(okResult as OkObjectResult);
        }

        [Fact]
        public void GetById_ExistingGuidPassed_ReturnsRightItem()
        {
            // Arrange
            var testId = 1;

            // Act
            var okResult = _productControllerFake.Get(testId) as OkObjectResult;

            // Assert
            var responseObj = Assert.IsType<ApiResponseViewModel>(okResult?.Value);
            var productVm = Assert.IsType<ProductViewModel>(responseObj?.Data);
            Assert.Equal(testId, productVm.Id);
        }

        [Fact]
        public void Create_InvalidObjectPassed_ReturnsBadRequest()
        {
            // Arrange
            var createProductDto = new CreateProductDto
            {
                Description = "Product Description"
            };

            _productControllerFake.ModelState.AddModelError("Name", "Required");

            // Act
            var badResponse = _productControllerFake.Create(createProductDto);

            // Assert
            Assert.IsType<BadRequestObjectResult>(badResponse);
        }

        [Fact]
        public void Create_ValidObjectPassed_ReturnsCreatedResponse()
        {
            // Arrange
            var createProductDto = new CreateProductDto
            {
                Name = "New Product Name",
                Description = "New Product Description",
                Qty = 54,
                InStock = 987,
                SubCategoryId = 1
            };

            // Act
            var createdResponse = _productControllerFake.Create(createProductDto);

            // Assert
            Assert.IsType<OkObjectResult>(createdResponse);
        }

        [Fact]
        public void Create_ValidObjectPassed_ReturnedResponseHasCreatedItem()
        {
            // Arrange
            var createProductDto = new CreateProductDto
            {
                Name = "New Product Name",
                Description = "New Product Description",
                Qty = 54,
                InStock = 987,
                SubCategoryId = 1
            };

            // Act
            var createdResponse = _productControllerFake.Create(createProductDto) as OkObjectResult;
            var responseObj = createdResponse?.Value as ApiResponseViewModel;

            // Assert
            var product = Assert.IsType<Product>(responseObj?.Data);
            Assert.Equal("New Product Name", product.Name);
        }

        [Fact]
        public void Update_InvalidObjectPassed_ReturnsBadRequest()
        {
            // Arrange
            var updateProductDto = new UpdateProductDto
            {
                Id = 1,
                Description = "Edited Product Description",
                Qty = 54,
                InStock = 987,
                SubCategoryId = 1
            };

            _productControllerFake.ModelState.AddModelError("Name", "Required");

            // Act
            var badResponse = _productControllerFake.Update(updateProductDto);

            // Assert
            Assert.IsType<BadRequestObjectResult>(badResponse);
        }

        [Fact]
        public void Update_ValidObjectPassed_ReturnsOkResult()
        {
            // Arrange
            var updateProductDto = new UpdateProductDto
            {
                Id = 1,
                Name = "Edited Product Name",
                Description = "Edited Product Description",
                Qty = 54,
                InStock = 987,
                SubCategoryId = 1
            };

            // Act
            var createdResponse = _productControllerFake.Update(updateProductDto);

            // Assert
            Assert.IsType<OkObjectResult>(createdResponse);
        }

        [Fact]
        public void Update_ValidObjectPassed_ReturnedResponseHasCreatedItem()
        {
            // Arrange
            var updateProductDto = new UpdateProductDto
            {
                Id = 1,
                Name = "Edited Product Name",
                Description = "Edited Product Description",
                Qty = 54,
                InStock = 987,
                SubCategoryId = 1
            };

            // Act
            var createdResponse = _productControllerFake.Update(updateProductDto) as OkObjectResult;
            var responseObj = createdResponse?.Value as ApiResponseViewModel;

            // Assert
            var product = Assert.IsType<Product>(responseObj?.Data);
            Assert.Equal("Edited Product Name", product.Name);
        }

        [Fact]
        public void Delete_NotExistingIdPassed_ReturnsNotFoundResponse()
        {
            // Arrange
            var notExistingId = 49;

            try
            {
                // Act
                var notFoundResult = _productControllerFake.Delete(notExistingId);

                // Assert
                Assert.Fail("An exception should have been thrown");
            }
            catch (EntityNotFoundException ex)
            {
                // Assert
                Assert.Equal("Entity Not Found", ex.Message);
            }
        }

        [Fact]
        public void Delete_ExistingIdPassed_ReturnsNoContentResult()
        {
            // Arrange
            var existingId = 1;

            // Act
            var okResponse = _productControllerFake.Delete(existingId);

            // Assert
            Assert.IsType<OkObjectResult>(okResponse);
        }

        [Fact]
        public void Delete_ExistingIdPassed_RemovesOneItem()
        {
            // Arrange
            var existingId = 1;

            // Act
            _productControllerFake.Delete(existingId);

            // Assert
            Assert.True(_productService.Get(1).IsDeleted);
        }

    }
}
