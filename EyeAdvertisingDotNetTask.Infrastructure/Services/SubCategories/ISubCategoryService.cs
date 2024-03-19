using EyeAdvertisingDotNetTask.Core.Dtos.Categories;
using EyeAdvertisingDotNetTask.Core.Dtos.General;
using EyeAdvertisingDotNetTask.Core.Dtos.SubCategories;
using EyeAdvertisingDotNetTask.Core.ViewModels.Categories;
using EyeAdvertisingDotNetTask.Core.ViewModels.General;
using EyeAdvertisingDotNetTask.Core.ViewModels.SubCategories;

namespace EyeAdvertisingDotNetTask.Infrastructure.Services.SubCategories
{
    public interface ISubCategoryService
    {
        Task<PagingResultViewModel<SubCategoryViewModel>> GetAll(PagingDto pagingDto);
        Task<SubCategoryViewModel> Get(int id);
        Task<int> Create(CreateSubCategoryDto dto, string userId);
        Task<int> Update(UpdateSubCategoryDto dto, string userId);
        Task Delete(int id);
    }
}
