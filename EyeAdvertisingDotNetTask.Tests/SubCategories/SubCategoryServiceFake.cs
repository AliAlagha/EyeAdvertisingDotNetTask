using AutoMapper;
using EyeAdvertisingDotNetTask.Core.Dtos.Categories;
using EyeAdvertisingDotNetTask.Core.Dtos.General;
using EyeAdvertisingDotNetTask.Core.Dtos.SubCategories;
using EyeAdvertisingDotNetTask.Core.Exceptions;
using EyeAdvertisingDotNetTask.Core.ViewModels.Categories;
using EyeAdvertisingDotNetTask.Core.ViewModels.General;
using EyeAdvertisingDotNetTask.Core.ViewModels.SubCategories;
using EyeAdvertisingDotNetTask.Data;
using EyeAdvertisingDotNetTask.Data.DbEntities;
using EyeAdvertisingDotNetTask.Infrastructure.Extentions;
using EyeAdvertisingDotNetTask.Infrastructure.Services.Categories;
using Humanizer;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace EyeAdvertisingDotNetTask.Tests.SubCategories
{
    public class SubCategoryServiceFake : ISubCategoryServiceFake
    {
        private readonly List<SubCategory> _subCategories;

        public SubCategoryServiceFake()
        {
            _subCategories = new List<SubCategory>
            {
                new SubCategory
                {
                    Id = 1,
                    Name = "SubCategory 1",
                    Description = "SubCategory 1 Description",
                    CategoryId = 1
                },
                new SubCategory
                {
                    Id= 2,
                    Name = "SubCategory 2",
                    Description = "SubCategory 2 Description",
                    CategoryId = 1
                },
                new SubCategory
                {
                    Id= 3,
                    Name = "SubCategory 3",
                    Description = "SubCategory 3 Description",
                    CategoryId = 1
                }
            };
        }

        public PagingResultViewModel<SubCategoryViewModel> GetAll(PagingDto pagingDto)
        {
            var pageSize = pagingDto.PageSize;
            var skip = (int)Math.Ceiling(pageSize * (decimal)(pagingDto.Page - 1));

            var totalCount = _subCategories.Count();
            var paginatedSubCategories = _subCategories.Skip(skip).Take(pageSize).ToList();

            var paginatedCategoriesVms = new List<SubCategoryViewModel>();
            foreach (var subCategory in paginatedSubCategories)
            {
                var categoryVm = new SubCategoryViewModel
                {
                    Id = subCategory.Id,
                    Name = subCategory.Name,
                    Description = subCategory.Description
                };

                paginatedCategoriesVms.Add(categoryVm);
            }

            return new PagingResultViewModel<SubCategoryViewModel>
            {
                Page = pagingDto.Page,
                PageSize = pageSize,
                TotalCount = totalCount,
                Data = paginatedCategoriesVms
            };
        }

        public SubCategoryViewModel Get(int id)
        {
            var subCategory = _subCategories.SingleOrDefault(x => x.Id == id);
            if (subCategory == null)
            {
                throw new EntityNotFoundException();
            }

            var subCategoryVm = new SubCategoryViewModel
            {
                Id = subCategory.Id,
                Name = subCategory.Name,
                Description = subCategory.Description,
                IsDeleted = subCategory.IsDeleted
            };

            return subCategoryVm;
        }

        public SubCategory Create(CreateSubCategoryDto dto)
        {
            var subCategory = new SubCategory
            {
                Id = _subCategories.Count() + 1,
                Name = dto.Name,
                Description = dto.Description,
                CategoryId = dto.CategoryId
            };

            _subCategories.Add(subCategory);

            return subCategory;
        }

        public SubCategory Update(UpdateSubCategoryDto dto)
        {
            var subCategory = _subCategories.SingleOrDefault(x => x.Id == dto.Id);
            if (subCategory == null)
            {
                throw new EntityNotFoundException();
            }

            subCategory.Name = dto.Name;
            subCategory.Description = dto.Description;
            subCategory.CategoryId = dto.CategoryId;

            return subCategory;
        }

        public void Delete(int id)
        {
            var subCategory = _subCategories.SingleOrDefault(x => x.Id == id);
            if (subCategory == null)
            {
                throw new EntityNotFoundException();
            }

            subCategory.IsDeleted = true;
        }

    }
}
