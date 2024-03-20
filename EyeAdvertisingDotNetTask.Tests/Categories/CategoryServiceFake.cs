using AutoMapper;
using EyeAdvertisingDotNetTask.Core.Dtos.Categories;
using EyeAdvertisingDotNetTask.Core.Dtos.General;
using EyeAdvertisingDotNetTask.Core.Exceptions;
using EyeAdvertisingDotNetTask.Core.ViewModels.Categories;
using EyeAdvertisingDotNetTask.Core.ViewModels.General;
using EyeAdvertisingDotNetTask.Data;
using EyeAdvertisingDotNetTask.Data.DbEntities;
using EyeAdvertisingDotNetTask.Infrastructure.Extentions;
using Humanizer;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace EyeAdvertisingDotNetTask.Infrastructure.Services.Categories
{
    public class CategoryServiceFake : ICategoryServiceFake
    {
        private readonly List<Category> _categories;

        public CategoryServiceFake()
        {
            _categories = new List<Category>
            {
                new Category
                {
                    Id = 1,
                    Name = "Category 1",
                    Description = "Category 1 Description"
                },
                new Category
                {
                    Id= 2,
                    Name = "Category 2",
                    Description = "Category 2 Description"
                },
                new Category
                {
                    Id= 3,
                    Name = "Category 3",
                    Description = "Category 3 Description"
                }
            };
        }

        public PagingResultViewModel<CategoryViewModel> GetAll(PagingDto pagingDto)
        {
            var pageSize = pagingDto.PageSize;
            var skip = (int)Math.Ceiling(pageSize * (decimal)(pagingDto.Page - 1));

            var totalCount = _categories.Count();
            var paginatedCategories = _categories.Skip(skip).Take(pageSize).ToList();

            var paginatedCategoriesVms = new List<CategoryViewModel>();
            foreach (var category in paginatedCategories)
            {
                var categoryVm = new CategoryViewModel
                {
                    Id = category.Id,
                    Name = category.Name,
                    Description = category.Description
                };

                paginatedCategoriesVms.Add(categoryVm);
            }

            return new PagingResultViewModel<CategoryViewModel>
            {
                Page = pagingDto.Page,
                PageSize = pageSize,
                TotalCount = totalCount,
                Data = paginatedCategoriesVms
            };
        }

        public CategoryViewModel Get(int id)
        {
            var category = _categories.SingleOrDefault(x => x.Id == id);
            if (category == null)
            {
                throw new EntityNotFoundException();
            }

            var categoryVm = new CategoryViewModel
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description,
                IsDeleted = category.IsDeleted
            };

            return categoryVm;
        }

        public Category Create(CreateCategoryDto dto)
        {
            var category = new Category
            {
                Id = _categories.Count() + 1,
                Name = dto.Name,
                Description = dto.Description
            };

            _categories.Add(category);

            return category;
        }

        public Category Update(UpdateCategoryDto dto)
        {
            var category = _categories.SingleOrDefault(x => x.Id == dto.Id);
            if (category == null)
            {
                throw new EntityNotFoundException();
            }

            category.Name = dto.Name;
            category.Description = dto.Description;

            return category;
        }

        public void Delete(int id)
        {
            var category = _categories.SingleOrDefault(x => x.Id == id);
            if (category == null)
            {
                throw new EntityNotFoundException();
            }

            category.IsDeleted = true;
        }

    }
}
