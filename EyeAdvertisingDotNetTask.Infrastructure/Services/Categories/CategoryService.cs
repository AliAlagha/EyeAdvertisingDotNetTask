using AutoMapper;
using EyeAdvertisingDotNetTask.Core.Dtos.Categories;
using EyeAdvertisingDotNetTask.Core.Dtos.General;
using EyeAdvertisingDotNetTask.Core.Exceptions;
using EyeAdvertisingDotNetTask.Core.ViewModels.Categories;
using EyeAdvertisingDotNetTask.Core.ViewModels.General;
using EyeAdvertisingDotNetTask.Data;
using EyeAdvertisingDotNetTask.Data.DbEntities;
using EyeAdvertisingDotNetTask.Infrastructure.Extentions;
using Microsoft.EntityFrameworkCore;

namespace EyeAdvertisingDotNetTask.Infrastructure.Services.Categories
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CategoryService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PagingResultViewModel<CategoryViewModel>> GetAll(PagingDto pagingDto)
        {
            var query = _context.Categories
                .OrderByDescending(x => x.CreatedAt).AsQueryable();
            return await query.ToPagedData<CategoryViewModel>(pagingDto, _mapper);
        }

        public async Task<CategoryViewModel> Get(int id)
        {
            var category = await _context.Categories.SingleOrDefaultAsync(x => x.Id == id);
            if (category == null)
            {
                throw new EntityNotFoundException();
            }

            var categoryVm = _mapper.Map<Category, CategoryViewModel>(category);
            return categoryVm;
        }

        public async Task<int> Create(CreateCategoryDto dto, string userId)
        {
            var category = _mapper.Map<CreateCategoryDto, Category>(dto);
            category.CreatedById = userId;
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();

            return category.Id;
        }

        public async Task<int> Update(UpdateCategoryDto dto, string userId)
        {
            var category = await _context.Categories
                .SingleOrDefaultAsync(x => x.Id == dto.Id);
            if (category == null)
            {
                throw new EntityNotFoundException();
            }

            _mapper.Map(dto, category);
            category.UpdatedById = userId;
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();

            return category.Id;
        }

        public async Task Delete(int id)
        {
            var category = await _context.Categories
                .SingleOrDefaultAsync(x => x.Id == id);
            if (category == null)
            {
                throw new EntityNotFoundException();
            }

            category.IsDeleted = true;
            _context.Categories.Update(category);

            await _context.SaveChangesAsync();
        }

    }
}
