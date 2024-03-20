using EyeAdvertisingDotNetTask.API.Controllers;
using EyeAdvertisingDotNetTask.Core.Constants;
using EyeAdvertisingDotNetTask.Core.Dtos.Categories;
using EyeAdvertisingDotNetTask.Core.Dtos.General;
using EyeAdvertisingDotNetTask.Core.Dtos.SubCategories;
using EyeAdvertisingDotNetTask.Core.ViewModels.General;
using EyeAdvertisingDotNetTask.Infrastructure.Services.Categories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace EyeAdvertisingDotNetTask.Tests.SubCategories
{
    public class SubCategoryControllerFake : BaseControllerFake
    {
        private readonly ISubCategoryServiceFake _subCategoryService;

        public SubCategoryControllerFake(ISubCategoryServiceFake categoryService)
        {
            _subCategoryService = categoryService;
        }

        [HttpGet]
        public ActionResult GetAll(int page = 1, int pageSize = 10)
            => GetResponse(() => new ApiResponseViewModel(_subCategoryService.GetAll(new PagingDto(page, pageSize)), true, Messages.GetRequest));

        [HttpGet("{id}")]
        public ActionResult Get(int id)
            => GetResponse(() => new ApiResponseViewModel(_subCategoryService.Get(id), true, Messages.GetRequest));

        [HttpPost]
        public ActionResult Create(CreateSubCategoryDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(new ApiResponseViewModel(_subCategoryService.Create(dto), true, Messages.PostRequest));
        }

        [HttpPut]
        public ActionResult Update(UpdateSubCategoryDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(new ApiResponseViewModel(_subCategoryService.Update(dto), true, Messages.PutRequest));
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
            => GetResponse(() =>
            {
                _subCategoryService.Delete(id);

                return new ApiResponseViewModel(true, Messages.DeleteRequest);
            });

    }
}
