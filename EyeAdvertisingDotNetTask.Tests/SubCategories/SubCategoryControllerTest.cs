using EyeAdvertisingDotNetTask.Core.Dtos.SubCategories;
using EyeAdvertisingDotNetTask.Core.Exceptions;
using EyeAdvertisingDotNetTask.Core.ViewModels.General;
using EyeAdvertisingDotNetTask.Core.ViewModels.SubCategories;
using EyeAdvertisingDotNetTask.Data.DbEntities;
using Microsoft.AspNetCore.Mvc;

namespace EyeAdvertisingDotNetTask.Tests.SubCategories
{
    public class SubCategoryControllerTest
    {
        private readonly SubCategoryControllerFake _subCategoryControllerFake;
        private readonly ISubCategoryServiceFake _subCategoryService;

        public SubCategoryControllerTest()
        {
            _subCategoryService = new SubCategoryServiceFake();
            _subCategoryControllerFake = new SubCategoryControllerFake(_subCategoryService);
        }

        [Fact]
        public void GetAll_WhenCalled_ReturnsOkResult()
        {
            // Act
            var okResult = _subCategoryControllerFake.GetAll();

            // Assert
            Assert.IsType<OkObjectResult>(okResult as OkObjectResult);
        }

        [Fact]
        public void GetAll_WhenCalled_ReturnsAllItems()
        {
            // Act
            var okResult = _subCategoryControllerFake.GetAll() as OkObjectResult;

            // Assert
            var responseObj = Assert.IsType<ApiResponseViewModel>(okResult?.Value);
            var pagingResultObj = Assert.IsType<PagingResultViewModel<SubCategoryViewModel>>(responseObj?.Data);
            var dataList = Assert.IsType<List<SubCategoryViewModel>>(pagingResultObj?.Data);

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
                var notFoundResult = _subCategoryControllerFake.Get(testId);

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
            var okResult = _subCategoryControllerFake.Get(testId);

            // Assert
            Assert.IsType<OkObjectResult>(okResult as OkObjectResult);
        }

        [Fact]
        public void GetById_ExistingGuidPassed_ReturnsRightItem()
        {
            // Arrange
            var testId = 1;

            // Act
            var okResult = _subCategoryControllerFake.Get(testId) as OkObjectResult;

            // Assert
            var responseObj = Assert.IsType<ApiResponseViewModel>(okResult?.Value);
            var categoryVm = Assert.IsType<SubCategoryViewModel>(responseObj?.Data);
            Assert.Equal(testId, categoryVm.Id);
        }

        [Fact]
        public void Create_InvalidObjectPassed_ReturnsBadRequest()
        {
            // Arrange
            var createCategoryDto = new CreateSubCategoryDto
            {
                Description = "New SubCategory Description",
                CategoryId = 1
            };

            _subCategoryControllerFake.ModelState.AddModelError("Name", "Required");

            // Act
            var badResponse = _subCategoryControllerFake.Create(createCategoryDto);

            // Assert
            Assert.IsType<BadRequestObjectResult>(badResponse);
        }

        [Fact]
        public void Create_ValidObjectPassed_ReturnsCreatedResponse()
        {
            // Arrange
            var createCategoryDto = new CreateSubCategoryDto
            {
                Name = "New SubCategory Name",
                Description = "New SubCategory Description",
                CategoryId = 1
            };

            // Act
            var createdResponse = _subCategoryControllerFake.Create(createCategoryDto);

            // Assert
            Assert.IsType<OkObjectResult>(createdResponse);
        }

        [Fact]
        public void Create_ValidObjectPassed_ReturnedResponseHasCreatedItem()
        {
            // Arrange
            var createCategoryDto = new CreateSubCategoryDto
            {
                Name = "New SubCategory Name",
                Description = "New SubCategory Description",
                CategoryId = 1
            };

            // Act
            var createdResponse = _subCategoryControllerFake.Create(createCategoryDto) as OkObjectResult;
            var responseObj = createdResponse?.Value as ApiResponseViewModel;

            // Assert
            var category = Assert.IsType<SubCategory>(responseObj?.Data);
            Assert.Equal("New SubCategory Name", category.Name);
        }

        [Fact]
        public void Update_InvalidObjectPassed_ReturnsBadRequest()
        {
            // Arrange
            var updateSubCategoryDto = new UpdateSubCategoryDto
            {
                Id = 1,
                Description = "Edited SubCategory Description",
                CategoryId = 1
            };

            _subCategoryControllerFake.ModelState.AddModelError("Name", "Required");

            // Act
            var badResponse = _subCategoryControllerFake.Update(updateSubCategoryDto);

            // Assert
            Assert.IsType<BadRequestObjectResult>(badResponse);
        }

        [Fact]
        public void Update_ValidObjectPassed_ReturnsOkResult()
        {
            // Arrange
            var updateSubCategoryDto = new UpdateSubCategoryDto
            {
                Id = 1,
                Name = "Edited SubCategory Name",
                Description = "Edited SubCategory Description",
                CategoryId = 1
            };

            // Act
            var createdResponse = _subCategoryControllerFake.Update(updateSubCategoryDto);

            // Assert
            Assert.IsType<OkObjectResult>(createdResponse);
        }

        [Fact]
        public void Update_ValidObjectPassed_ReturnedResponseHasCreatedItem()
        {
            // Arrange
            var updateSubCategoryDto = new UpdateSubCategoryDto
            {
                Id = 1,
                Name = "Edited SubCategory Name",
                Description = "Edited SubCategory Description",
                CategoryId = 1
            };

            // Act
            var createdResponse = _subCategoryControllerFake.Update(updateSubCategoryDto) as OkObjectResult;
            var responseObj = createdResponse?.Value as ApiResponseViewModel;

            // Assert
            var SubCategory = Assert.IsType<SubCategory>(responseObj?.Data);
            Assert.Equal("Edited SubCategory Name", SubCategory.Name);
        }

        [Fact]
        public void Delete_NotExistingIdPassed_ReturnsNotFoundResponse()
        {
            // Arrange
            var notExistingId = 49;

            try
            {
                // Act
                var notFoundResult = _subCategoryControllerFake.Delete(notExistingId);

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
            var okResponse = _subCategoryControllerFake.Delete(existingId);

            // Assert
            Assert.IsType<OkObjectResult>(okResponse);
        }

        [Fact]
        public void Delete_ExistingIdPassed_RemovesOneItem()
        {
            // Arrange
            var existingId = 1;

            // Act
            _subCategoryControllerFake.Delete(existingId);

            // Assert
            Assert.True(_subCategoryService.Get(1).IsDeleted);
        }

    }
}
