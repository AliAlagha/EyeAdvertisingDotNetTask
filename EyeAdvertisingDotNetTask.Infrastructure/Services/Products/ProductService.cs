using AutoMapper;
using EyeAdvertisingDotNetTask.Core.Constants;
using EyeAdvertisingDotNetTask.Core.Dtos.Categories;
using EyeAdvertisingDotNetTask.Core.Dtos.General;
using EyeAdvertisingDotNetTask.Core.Dtos.Products;
using EyeAdvertisingDotNetTask.Core.Exceptions;
using EyeAdvertisingDotNetTask.Core.Helpers;
using EyeAdvertisingDotNetTask.Core.ViewModels.Categories;
using EyeAdvertisingDotNetTask.Core.ViewModels.General;
using EyeAdvertisingDotNetTask.Core.ViewModels.Products;
using EyeAdvertisingDotNetTask.Data;
using EyeAdvertisingDotNetTask.Data.DbEntities;
using EyeAdvertisingDotNetTask.Infrastructure.Extentions;
using EyeAdvertisingDotNetTask.Infrastructure.Files;
using Humanizer;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace EyeAdvertisingDotNetTask.Infrastructure.Services.Products
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;

        public ProductService(ApplicationDbContext context, IMapper mapper
            , IFileService fileService)
        {
            _context = context;
            _mapper = mapper;
            _fileService = fileService;
        }

        public async Task<PagingResultViewModel<ProductViewModel>> GetAll(PagingDto pagingDto)
        {
            var query = _context.Products
                .Include(x => x.SubCategory)
                .OrderByDescending(x => x.CreatedAt).AsQueryable();

            var pageSize = pagingDto.PageSize;
            var skip = (int)Math.Ceiling(pageSize * (decimal)(pagingDto.Page - 1));

            var totalCount = await query.CountAsync();
            query = query.Skip(skip).Take(pageSize);

            var products = await query.ToListAsync();
            var productVms = new List<ProductViewModel>();
            foreach (var product in products)
            {
                var productVm = _mapper.Map<ProductViewModel>(product);
                productVm.ProductImgsList = ImageHelper.GetImageLinks(product.ProductImgs);

                productVms.Add(productVm);
            }

            return new PagingResultViewModel<ProductViewModel>
            {
                Page = pagingDto.Page,
                PageSize = pageSize,
                TotalCount = totalCount,
                Data = productVms
            };
        }

        public async Task<ProductViewModel> Get(int id)
        {
            var product = await _context.Products
                .Include(x => x.SubCategory)
                .SingleOrDefaultAsync(x => x.Id == id);
            if (product == null)
            {
                throw new EntityNotFoundException();
            }

            var productVm = _mapper.Map<Product, ProductViewModel>(product);
            productVm.ProductImgsList = ImageHelper.GetImageLinks(product.ProductImgs);
            return productVm;
        }

        public async Task<int> Create(CreateProductDto dto, string userId)
        {
            var product = _mapper.Map<CreateProductDto, Product>(dto);

            // Add product imgs
            var productImgs = new List<string>();
            if (dto.ProductImgs != null && dto.ProductImgs.Any())
            {
                foreach (var productImg in dto.ProductImgs)
                {
                    var fileName = await _fileService.SaveFile(productImg, FolderNames.Images);
                    if (fileName != null)
                    {
                        productImgs.Add(fileName);
                    }
                }
            }

            product.ProductImgs = JsonConvert.SerializeObject(productImgs);
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

            // Update product imgs
            var productImgs = new List<string>();
            if (dto.ProductImgs != null && dto.ProductImgs.Any())
            {
                foreach (var productImg in dto.ProductImgs)
                {
                    var fileName = await _fileService.SaveFile(productImg, FolderNames.Images);
                    if (fileName != null)
                    {
                        productImgs.Add(fileName);
                    }
                }
            }

            _mapper.Map(dto, product);

            product.ProductImgs = JsonConvert.SerializeObject(productImgs);
            product.UpdatedById = userId;
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
