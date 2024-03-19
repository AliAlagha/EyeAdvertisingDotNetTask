using EyeAdvertisingDotNetTask.Core.Constants;
using EyeAdvertisingDotNetTask.Core.Dtos.Categories;
using EyeAdvertisingDotNetTask.Core.Dtos.General;
using EyeAdvertisingDotNetTask.Core.ViewModels.General;
using EyeAdvertisingDotNetTask.Infrastructure.Services.Categories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace EyeAdvertisingDotNetTask.API.Controllers
{
    public class CategoryController : BaseController
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public Task<ActionResult> GetAll(int page = 1, int pageSize = 10)
            => GetResponse(async () => new ApiResponseViewModel(await _categoryService.GetAll(new PagingDto(page, pageSize)), true, Messages.GetRequest));

        [HttpGet("{id}")]
        public Task<ActionResult> Get(int id)
            => GetResponse(async () => new ApiResponseViewModel(await _categoryService.Get(id), true, Messages.GetRequest));

        [HttpPost]
        public Task<ActionResult> Create(CreateCategoryDto dto)
            => GetResponse(async () =>
            {
                await _categoryService.Create(dto, UserId);

                return new ApiResponseViewModel(true, Messages.PostRequest);
            });

        [HttpPut]
        public Task<ActionResult> Update(UpdateCategoryDto dto)
            => GetResponse(async () =>
            {
                await _categoryService.Update(dto, UserId);

                return new ApiResponseViewModel(true, Messages.PutRequest);
            });

        [HttpDelete("{id}")]
        public Task<ActionResult> Delete(int id)
            => GetResponse(async () =>
            {
                await _categoryService.Delete(id);

                return new ApiResponseViewModel(true, Messages.DeleteRequest);
            });

    }
}
