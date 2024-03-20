using EyeAdvertisingDotNetTask.Core.Dtos.Categories;
using EyeAdvertisingDotNetTask.Core.Dtos.General;
using EyeAdvertisingDotNetTask.Core.Dtos.Products;
using EyeAdvertisingDotNetTask.Core.ViewModels.Categories;
using EyeAdvertisingDotNetTask.Core.ViewModels.General;
using EyeAdvertisingDotNetTask.Core.ViewModels.Products;
using EyeAdvertisingDotNetTask.Data.DbEntities;

namespace EyeAdvertisingDotNetTask.Tests.Products
{
    public interface IProductServiceFake
    {
        PagingResultViewModel<ProductViewModel> GetAll(PagingDto pagingDto);
        ProductViewModel Get(int id);
        Product Create(CreateProductDto dto);
        Product Update(UpdateProductDto dto);
        void Delete(int id);
    }
}
