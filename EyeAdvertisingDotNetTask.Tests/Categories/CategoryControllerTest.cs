using EyeAdvertisingDotNetTask.API.Controllers;
using EyeAdvertisingDotNetTask.Core.Constants;
using EyeAdvertisingDotNetTask.Core.Dtos.Categories;
using EyeAdvertisingDotNetTask.Core.Dtos.General;
using EyeAdvertisingDotNetTask.Core.Exceptions;
using EyeAdvertisingDotNetTask.Core.ViewModels.Categories;
using EyeAdvertisingDotNetTask.Core.ViewModels.General;
using EyeAdvertisingDotNetTask.Data.DbEntities;
using EyeAdvertisingDotNetTask.Infrastructure.Services.Categories;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using NuGet.Protocol.Plugins;

namespace EyeAdvertisingDotNetTask.Tests.Categories
{
    public class CategoryControllerTest
    {
        private readonly CategoryControllerFake _categoryControllerFake;
        private readonly ICategoryServiceFake _categoryService;

        public CategoryControllerTest()
        {
            _categoryService = new CategoryServiceFake();
            _categoryControllerFake = new CategoryControllerFake(_categoryService);
        }

        [Fact]
        public void GetAll_WhenCalled_ReturnsOkResult()
        {
            // Act
            var okResult = _categoryControllerFake.GetAll();

            // Assert
            Assert.IsType<OkObjectResult>(okResult as OkObjectResult);
        }

        [Fact]
        public void GetAll_WhenCalled_ReturnsAllItems()
        {
            // Act
            var okResult = _categoryControllerFake.GetAll() as OkObjectResult;

            // Assert
            var responseObj = Assert.IsType<ApiResponseViewModel>(okResult?.Value);
            var pagingResultObj = Assert.IsType<PagingResultViewModel<CategoryViewModel>>(responseObj?.Data);
            var dataList = Assert.IsType<List<CategoryViewModel>>(pagingResultObj?.Data);

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
                var notFoundResult = _categoryControllerFake.Get(testId);

                // Assert
                Assert.Fail("An exception should have been thrown");
            }
            catch(EntityNotFoundException ex)
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
            var okResult = _categoryControllerFake.Get(testId);

            // Assert
            Assert.IsType<OkObjectResult>(okResult as OkObjectResult);
        }

        [Fact]
        public void GetById_ExistingGuidPassed_ReturnsRightItem()
        {
            // Arrange
            var testId = 1;

            // Act
            var okResult = _categoryControllerFake.Get(testId) as OkObjectResult;

            // Assert
            var responseObj = Assert.IsType<ApiResponseViewModel>(okResult?.Value);
            var categoryVm = Assert.IsType<CategoryViewModel>(responseObj?.Data);
            Assert.Equal(testId, categoryVm.Id);
        }

        [Fact]
        public void Create_InvalidObjectPassed_ReturnsBadRequest()
        {
            // Arrange
            var createCategoryDto = new CreateCategoryDto
            {
                Description = "Category Description"
            };

            _categoryControllerFake.ModelState.AddModelError("Name", "Required");

            // Act
            var badResponse = _categoryControllerFake.Create(createCategoryDto);

            // Assert
            Assert.IsType<BadRequestObjectResult>(badResponse);
        }

        [Fact]
        public void Create_ValidObjectPassed_ReturnsOkResult()
        {
            // Arrange
            var createCategoryDto = new CreateCategoryDto
            {
                Name = "Category Name",
                Description = "Category Description"
            };

            // Act
            var createdResponse = _categoryControllerFake.Create(createCategoryDto);

            // Assert
            Assert.IsType<OkObjectResult>(createdResponse);
        }

        [Fact]
        public void Create_ValidObjectPassed_ReturnedResponseHasCreatedItem()
        {
            // Arrange
            var createCategoryDto = new CreateCategoryDto
            {
                Name = "New Category Name",
                Description = "New Category Description"
            };

            // Act
            var createdResponse = _categoryControllerFake.Create(createCategoryDto) as OkObjectResult;
            var responseObj = createdResponse?.Value as ApiResponseViewModel;

            // Assert
            var category = Assert.IsType<Category>(responseObj?.Data);
            Assert.Equal("New Category Name", category.Name);
        }

        [Fact]
        public void Update_InvalidObjectPassed_ReturnsBadRequest()
        {
            // Arrange
            var updateCategoryDto = new UpdateCategoryDto
            {
                Id = 1,
                Description = "Category Description"
            };

            _categoryControllerFake.ModelState.AddModelError("Name", "Required");

            // Act
            var badResponse = _categoryControllerFake.Update(updateCategoryDto);

            // Assert
            Assert.IsType<BadRequestObjectResult>(badResponse);
        }

        [Fact]
        public void Update_ValidObjectPassed_ReturnsOkResult()
        {
            // Arrange
            var updateCategoryDto = new UpdateCategoryDto
            {
                Id = 1,
                Name = "Edited Category Name",
                Description = "Edited Category Description"
            };

            // Act
            var createdResponse = _categoryControllerFake.Update(updateCategoryDto);

            // Assert
            Assert.IsType<OkObjectResult>(createdResponse);
        }

        [Fact]
        public void Update_ValidObjectPassed_ReturnedResponseHasCreatedItem()
        {
            // Arrange
            var updateCategoryDto = new UpdateCategoryDto
            {
                Id = 1,
                Name = "Edited Category Name",
                Description = "Edited Category Description"
            };

            // Act
            var createdResponse = _categoryControllerFake.Update(updateCategoryDto) as OkObjectResult;
            var responseObj = createdResponse?.Value as ApiResponseViewModel;

            // Assert
            var category = Assert.IsType<Category>(responseObj?.Data);
            Assert.Equal("Edited Category Name", category.Name);
        }

        [Fact]
        public void Delete_NotExistingIdPassed_ReturnsNotFoundResponse()
        {
            // Arrange
            var notExistingId = 49;

            try
            {
                // Act
                var notFoundResult = _categoryControllerFake.Delete(notExistingId);

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
            var okResponse = _categoryControllerFake.Delete(existingId);

            // Assert
            Assert.IsType<OkObjectResult>(okResponse);
        }

        [Fact]
        public void Delete_ExistingIdPassed_RemovesOneItem()
        {
            // Arrange
            var existingId = 1;

            // Act
            _categoryControllerFake.Delete(existingId);

            // Assert
            Assert.True(_categoryService.Get(1).IsDeleted);
        }

    }
}
