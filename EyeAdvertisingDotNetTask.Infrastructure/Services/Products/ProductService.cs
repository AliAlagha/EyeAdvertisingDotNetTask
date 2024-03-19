using AutoMapper;
using EyeAdvertisingDotNetTask.Core.Dtos.Categories;
using EyeAdvertisingDotNetTask.Core.Dtos.General;
using EyeAdvertisingDotNetTask.Core.Dtos.Products;
using EyeAdvertisingDotNetTask.Core.Exceptions;
using EyeAdvertisingDotNetTask.Core.ViewModels.Categories;
using EyeAdvertisingDotNetTask.Core.ViewModels.General;
using EyeAdvertisingDotNetTask.Core.ViewModels.Products;
using EyeAdvertisingDotNetTask.Data;
using EyeAdvertisingDotNetTask.Data.DbEntities;
using EyeAdvertisingDotNetTask.Infrastructure.Extentions;
using Microsoft.EntityFrameworkCore;

namespace EyeAdvertisingDotNetTask.Infrastructure.Services.Products
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ProductService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PagingResultViewModel<ProductViewModel>> GetAll(PagingDto pagingDto)
        {
            var query = _context.Products
                .Include(x => x.SubCategory)
                .OrderByDescending(x => x.CreatedAt).AsQueryable();
            return await query.ToPagedData<ProductViewModel>(pagingDto, _mapper);
        }

        public async Task<ProductViewModel> Get(int id)
        {
            var product = await _context.Products.SingleOrDefaultAsync(x => x.Id == id);
            if (product == null)
            {
                throw new EntityNotFoundException();
            }

            var productVm = _mapper.Map<Product, ProductViewModel>(product);
            return productVm;
        }

        public async Task<int> Create(CreateProductDto dto, string userId)
        {
            var product = _mapper.Map<CreateProductDto, Product>(dto);
            product.CreatedById = userId;
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

            return product.Id;
        }

        public async Task<int> Update(UpdateProductDto dto, string userId)
        {
            var product = await _context.Products
                .SingleOrDefaultAsync(x => x.Id == dto.Id);
            if (product == null)
            {
                throw new EntityNotFoundException();
            }

            product.UpdatedById = userId;
            _mapper.Map(dto, product);
            _context.Products.Update(product);
            await _context.SaveChangesAsync();

            return product.Id;
        }

        public async Task Delete(int id)
        {
            var product = await _context.Products
                .SingleOrDefaultAsync(x => x.Id == id);
            if (product == null)
            {
                throw new EntityNotFoundException();
            }

            product.IsDeleted = true;
            _context.Products.Update(product);

            await _context.SaveChangesAsync();
        }

    }
}
