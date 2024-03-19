using EyeAdvertisingDotNetTask.Core.Dtos.Categories;
using EyeAdvertisingDotNetTask.Core.Dtos.General;
using EyeAdvertisingDotNetTask.Core.ViewModels.Categories;
using EyeAdvertisingDotNetTask.Core.ViewModels.General;

namespace EyeAdvertisingDotNetTask.Infrastructure.Services.Categories
{
    public interface ICategoryService
    {
        Task<PagingResultViewModel<CategoryViewModel>> GetAll(PagingDto pagingDto);
        Task<CategoryViewModel> Get(int id);
        Task<int> Create(CreateCategoryDto dto, string userId);
        Task<int> Update(UpdateCategoryDto dto, string userId);
        Task Delete(int id);
    }
}
