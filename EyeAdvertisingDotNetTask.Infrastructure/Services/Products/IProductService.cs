using EyeAdvertisingDotNetTask.Core.Dtos.Categories;
using EyeAdvertisingDotNetTask.Core.Dtos.General;
using EyeAdvertisingDotNetTask.Core.Dtos.Products;
using EyeAdvertisingDotNetTask.Core.ViewModels.Categories;
using EyeAdvertisingDotNetTask.Core.ViewModels.General;
using EyeAdvertisingDotNetTask.Core.ViewModels.Products;

namespace EyeAdvertisingDotNetTask.Infrastructure.Services.Products
{
    public interface IProductService
    {
        Task<PagingResultViewModel<ProductViewModel>> GetAll(PagingDto pagingDto);
        Task<ProductViewModel> Get(int id);
        Task<int> Create(CreateProductDto dto, string userId);
        Task<int> Update(UpdateProductDto dto, string userId);
        Task Delete(int id);
    }
}
