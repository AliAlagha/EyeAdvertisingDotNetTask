using EyeAdvertisingDotNetTask.Core.Dtos.General;
using EyeAdvertisingDotNetTask.Core.Dtos.Products;
using EyeAdvertisingDotNetTask.Core.Exceptions;
using EyeAdvertisingDotNetTask.Core.Helper;
using EyeAdvertisingDotNetTask.Core.ViewModels.General;
using EyeAdvertisingDotNetTask.Core.ViewModels.Products;
using EyeAdvertisingDotNetTask.Data.DbEntities;

namespace EyeAdvertisingDotNetTask.Tests.Products
{
    public class ProductServiceFake : IProductServiceFake
    {
        private readonly List<Product> _categories;

        public ProductServiceFake()
        {
            _categories = new List<Product>
            {
                new Product
                {
                    Id = 1,
                    Name = "Product 1",
                    Description = "Product 1 Description",
                    Qty = 5,
                    InStock = 789,
                    ProductImgs = "[\"Image1.png\", \"Image2.png\"]",
                    SubCategoryId = 1
                },
                new Product
                {
                    Id= 2,
                    Name = "Product 2",
                    Description = "Product 2 Description",
                    Qty = 6,
                    InStock = 245,
                    ProductImgs = "[\"Image3.png\", \"Image4.png\"]",
                    SubCategoryId = 1
                },
                new Product
                {
                    Id= 3,
                    Name = "Product 3",
                    Description = "Product 3 Description",
                    Qty = 45,
                    InStock = 123,
                    ProductImgs = "[\"Image5.png\", \"Image6.png\"]",
                    SubCategoryId = 1
                }
            };
        }

        public PagingResultViewModel<ProductViewModel> GetAll(PagingDto pagingDto)
        {
            var pageSize = pagingDto.PageSize;
            var skip = (int)Math.Ceiling(pageSize * (decimal)(pagingDto.Page - 1));

            var totalCount = _categories.Count();
            var paginatedCategories = _categories.Skip(skip).Take(pageSize).ToList();

            var paginatedCategoriesVms = new List<ProductViewModel>();
            foreach (var product in paginatedCategories)
            {
                var productVm = new ProductViewModel
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    Qty = product.Qty,
                    InStock = product.InStock,
                    ProductImgsList = ImageHelper.GetImageLinks( product.ProductImgs)
                };

                paginatedCategoriesVms.Add(productVm);
            }

            return new PagingResultViewModel<ProductViewModel>
            {
                Page = pagingDto.Page,
                PageSize = pageSize,
                TotalCount = totalCount,
                Data = paginatedCategoriesVms
            };
        }

        public ProductViewModel Get(int id)
        {
            var product = _categories.SingleOrDefault(x => x.Id == id);
            if (product == null)
            {
                throw new EntityNotFoundException();
            }

            var productVm = new ProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Qty = product.Qty,
                InStock = product.InStock,
                ProductImgsList = ImageHelper.GetImageLinks(product.ProductImgs),
                IsDeleted = product.IsDeleted
            };

            return productVm;
        }

        public Product Create(CreateProductDto dto)
        {
            var product = new Product
            {
                Id = _categories.Count() + 1,
                Name = dto.Name,
                Description = dto.Description,
                Qty = dto.Qty,
                InStock = dto.InStock
            };

            _categories.Add(product);

            return product;
        }

        public Product Update(UpdateProductDto dto)
        {
            var product = _categories.SingleOrDefault(x => x.Id == dto.Id);
            if (product == null)
            {
                throw new EntityNotFoundException();
            }

            product.Name = dto.Name;
            product.Description = dto.Description;
            product.Qty = dto.Qty;
            product.InStock = dto.InStock;

            return product;
        }

        public void Delete(int id)
        {
            var product = _categories.SingleOrDefault(x => x.Id == id);
            if (product == null)
            {
                throw new EntityNotFoundException();
            }

            product.IsDeleted = true;
        }

    }
}
