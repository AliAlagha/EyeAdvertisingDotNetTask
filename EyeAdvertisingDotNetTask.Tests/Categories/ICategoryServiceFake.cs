using EyeAdvertisingDotNetTask.Core.Dtos.Categories;
using EyeAdvertisingDotNetTask.Core.Dtos.General;
using EyeAdvertisingDotNetTask.Core.ViewModels.Categories;
using EyeAdvertisingDotNetTask.Core.ViewModels.General;
using EyeAdvertisingDotNetTask.Data.DbEntities;

namespace EyeAdvertisingDotNetTask.Infrastructure.Services.Categories
{
    public interface ICategoryServiceFake
    {
        PagingResultViewModel<CategoryViewModel> GetAll(PagingDto pagingDto);
        CategoryViewModel Get(int id);
        Category Create(CreateCategoryDto dto);
        Category Update(UpdateCategoryDto dto);
        void Delete(int id);
    }
}
