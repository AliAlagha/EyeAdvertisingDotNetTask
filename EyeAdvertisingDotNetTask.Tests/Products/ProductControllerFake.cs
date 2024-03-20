using EyeAdvertisingDotNetTask.API.Controllers;
using EyeAdvertisingDotNetTask.Core.Constants;
using EyeAdvertisingDotNetTask.Core.Dtos.Categories;
using EyeAdvertisingDotNetTask.Core.Dtos.General;
using EyeAdvertisingDotNetTask.Core.Dtos.Products;
using EyeAdvertisingDotNetTask.Core.ViewModels.General;
using EyeAdvertisingDotNetTask.Infrastructure.Services.Categories;
using EyeAdvertisingDotNetTask.Tests.SubCategories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace EyeAdvertisingDotNetTask.Tests.Products
{
    public class ProductControllerFake : BaseControllerFake
    {
        private readonly IProductServiceFake _productService;

        public ProductControllerFake(IProductServiceFake productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public ActionResult GetAll(int page = 1, int pageSize = 10)
            => GetResponse(() => new ApiResponseViewModel(_productService.GetAll(new PagingDto(page, pageSize)), true, Messages.GetRequest));

        [HttpGet("{id}")]
        public ActionResult Get(int id)
            => GetResponse(() => new ApiResponseViewModel(_productService.Get(id), true, Messages.GetRequest));

        [HttpPost]
        public ActionResult Create(CreateProductDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(new ApiResponseViewModel(_productService.Create(dto), true, Messages.PostRequest));
        }

        [HttpPut]
        public ActionResult Update(UpdateProductDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(new ApiResponseViewModel(_productService.Update(dto), true, Messages.PutRequest));
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
            => GetResponse(() =>
            {
                _productService.Delete(id);

                return new ApiResponseViewModel(true, Messages.DeleteRequest);
            });

    }
}
