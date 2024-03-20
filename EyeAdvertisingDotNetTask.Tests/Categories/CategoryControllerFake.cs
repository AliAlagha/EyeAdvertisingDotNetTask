using EyeAdvertisingDotNetTask.API.Controllers;
using EyeAdvertisingDotNetTask.Core.Constants;
using EyeAdvertisingDotNetTask.Core.Dtos.Categories;
using EyeAdvertisingDotNetTask.Core.Dtos.General;
using EyeAdvertisingDotNetTask.Core.ViewModels.General;
using EyeAdvertisingDotNetTask.Infrastructure.Services.Categories;
using EyeAdvertisingDotNetTask.Tests.SubCategories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace EyeAdvertisingDotNetTask.Tests.Categories
{
    public class CategoryControllerFake : BaseControllerFake
    {
        private readonly ICategoryServiceFake _categoryService;

        public CategoryControllerFake(ICategoryServiceFake categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public ActionResult GetAll(int page = 1, int pageSize = 10)
            => GetResponse(() => new ApiResponseViewModel(_categoryService.GetAll(new PagingDto(page, pageSize)), true, Messages.GetRequest));

        [HttpGet("{id}")]
        public ActionResult Get(int id)
            => GetResponse(() => new ApiResponseViewModel(_categoryService.Get(id), true, Messages.GetRequest));

        [HttpPost]
        public ActionResult Create(CreateCategoryDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(new ApiResponseViewModel(_categoryService.Create(dto), true, Messages.PostRequest));
        }

        [HttpPut]
        public ActionResult Update(UpdateCategoryDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(new ApiResponseViewModel(_categoryService.Update(dto), true, Messages.PutRequest));
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
            => GetResponse(() =>
            {
                _categoryService.Delete(id);

                return new ApiResponseViewModel(true, Messages.DeleteRequest);
            });

    }
}
