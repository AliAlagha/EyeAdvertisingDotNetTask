using EyeAdvertisingDotNetTask.Core.Dtos.Categories;
using EyeAdvertisingDotNetTask.Core.Dtos.General;
using EyeAdvertisingDotNetTask.Core.Dtos.SubCategories;
using EyeAdvertisingDotNetTask.Core.ViewModels.Categories;
using EyeAdvertisingDotNetTask.Core.ViewModels.General;
using EyeAdvertisingDotNetTask.Core.ViewModels.SubCategories;
using EyeAdvertisingDotNetTask.Data.DbEntities;

namespace EyeAdvertisingDotNetTask.Tests.SubCategories
{
    public interface ISubCategoryServiceFake
    {
        PagingResultViewModel<SubCategoryViewModel> GetAll(PagingDto pagingDto);
        SubCategoryViewModel Get(int id);
        SubCategory Create(CreateSubCategoryDto dto);
        SubCategory Update(UpdateSubCategoryDto dto);
        void Delete(int id);
    }
}
