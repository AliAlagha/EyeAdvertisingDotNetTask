using EyeAdvertisingDotNetTask.Core.Constants;
using EyeAdvertisingDotNetTask.Core.Dtos.Categories;
using EyeAdvertisingDotNetTask.Core.Dtos.General;
using EyeAdvertisingDotNetTask.Core.Dtos.Products;
using EyeAdvertisingDotNetTask.Core.ViewModels.General;
using EyeAdvertisingDotNetTask.Infrastructure.Services.Categories;
using EyeAdvertisingDotNetTask.Infrastructure.Services.Products;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace EyeAdvertisingDotNetTask.API.Controllers
{
    public class ProductController : BaseController
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public Task<ActionResult> GetAll(int page = 1, int pageSize = 10)
            => GetResponse(async () => new ApiResponseViewModel(await _productService.GetAll(new PagingDto(page, pageSize)), true, Messages.GetRequest));

        [HttpGet("{id}")]
        public Task<ActionResult> Get(int id)
            => GetResponse(async () => new ApiResponseViewModel(await _productService.Get(id), true, Messages.GetRequest));

        [HttpPost]
        public Task<ActionResult> Create(CreateProductDto dto)
            => GetResponse(async () =>
            {
                await _productService.Create(dto, UserId);

                return new ApiResponseViewModel(true, Messages.PostRequest);
            });

        [HttpPut("{id}")]
        public Task<ActionResult> Update(UpdateProductDto dto)
            => GetResponse(async () =>
            {
                await _productService.Update(dto, UserId);

                return new ApiResponseViewModel(true, Messages.PutRequest);
            });

        [HttpDelete("{id}")]
        public Task<ActionResult> Delete(int id)
            => GetResponse(async () =>
            {
                await _productService.Delete(id);

                return new ApiResponseViewModel(true, Messages.DeleteRequest);
            });

    }
}
