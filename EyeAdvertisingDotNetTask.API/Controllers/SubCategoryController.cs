using EyeAdvertisingDotNetTask.Core.Constants;
using EyeAdvertisingDotNetTask.Core.Dtos.Categories;
using EyeAdvertisingDotNetTask.Core.Dtos.General;
using EyeAdvertisingDotNetTask.Core.Dtos.SubCategories;
using EyeAdvertisingDotNetTask.Core.ViewModels.General;
using EyeAdvertisingDotNetTask.Infrastructure.Services.Categories;
using EyeAdvertisingDotNetTask.Infrastructure.Services.SubCategories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace EyeAdvertisingDotNetTask.API.Controllers
{
    public class SubCategoryController : BaseController
    {
        private readonly ISubCategoryService _subCategoryService;

        public SubCategoryController(ISubCategoryService subCategoryService)
        {
            _subCategoryService = subCategoryService;
        }

        [HttpGet]
        public Task<ActionResult> GetAll(int page = 1, int pageSize = 10)
            => GetResponse(async () => new ApiResponseViewModel(await _subCategoryService.GetAll(new PagingDto(page, pageSize)), true, Messages.GetRequest));

        [HttpGet("{id}")]
        public Task<ActionResult> Get(int id)
            => GetResponse(async () => new ApiResponseViewModel(await _subCategoryService.Get(id), true, Messages.GetRequest));

        [HttpPost]
        public Task<ActionResult> Create(CreateSubCategoryDto dto)
            => GetResponse(async () =>
            {
                return new ApiResponseViewModel(await _subCategoryService.Create(dto, UserId), true, Messages.PostRequest);
            });

        [HttpPut]
        public Task<ActionResult> Update(UpdateSubCategoryDto dto)
            => GetResponse(async () =>
            {
                return new ApiResponseViewModel(await _subCategoryService.Update(dto, UserId), true, Messages.PutRequest);
            });

        [HttpDelete("{id}")]
        public Task<ActionResult> Delete(int id)
            => GetResponse(async () =>
            {
                await _subCategoryService.Delete(id);

                return new ApiResponseViewModel(true, Messages.DeleteRequest);
            });

    }
}
