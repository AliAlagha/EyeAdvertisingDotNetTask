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
using Microsoft.EntityFrameworkCore;

namespace EyeAdvertisingDotNetTask.Infrastructure.Services.SubCategories
{
    public class SubCategoryService : ISubCategoryService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public SubCategoryService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PagingResultViewModel<SubCategoryViewModel>> GetAll(PagingDto pagingDto)
        {
            var query = _context.SubCategories
                .Include(x => x.Category)
                .Include(x => x.Products)
                .OrderByDescending(x => x.CreatedAt).AsQueryable();
            return await query.ToPagedData<SubCategoryViewModel>(pagingDto, _mapper);
        }

        public async Task<SubCategoryViewModel> Get(int id)
        {
            var subCategory = await _context.SubCategories.SingleOrDefaultAsync(x => x.Id == id);
            if (subCategory == null)
            {
                throw new EntityNotFoundException();
            }

            var subCategoryVm = _mapper.Map<SubCategory, SubCategoryViewModel>(subCategory);
            return subCategoryVm;
        }

        public async Task<int> Create(CreateSubCategoryDto dto, string userId)
        {
            var subCategory = _mapper.Map<CreateSubCategoryDto, SubCategory>(dto);
            subCategory.CreatedById = userId;
            await _context.SubCategories.AddAsync(subCategory);
            await _context.SaveChangesAsync();

            return subCategory.Id;
        }

        public async Task<int> Update(UpdateSubCategoryDto dto, string userId)
        {
            var subCategory = await _context.SubCategories
                .SingleOrDefaultAsync(x => x.Id == dto.Id);
            if (subCategory == null)
            {
                throw new EntityNotFoundException();
            }

            subCategory.UpdatedById = userId;
            _mapper.Map(dto, subCategory);
            _context.SubCategories.Update(subCategory);
            await _context.SaveChangesAsync();

            return subCategory.Id;
        }

        public async Task Delete(int id)
        {
            var subCategory = await _context.SubCategories
                .SingleOrDefaultAsync(x => x.Id == id);
            if (subCategory == null)
            {
                throw new EntityNotFoundException();
            }

            subCategory.IsDeleted = true;
            _context.SubCategories.Update(subCategory);

            await _context.SaveChangesAsync();
        }

    }
}
