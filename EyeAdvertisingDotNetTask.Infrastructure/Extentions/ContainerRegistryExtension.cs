using EyeAdvertisingDotNetTask.Infrastructure.Services.Auth;
using EyeAdvertisingDotNetTask.Infrastructure.Services.Categories;
using EyeAdvertisingDotNetTask.Infrastructure.Services.Products;
using EyeAdvertisingDotNetTask.Infrastructure.Services.SubCategories;
using Microsoft.Extensions.DependencyInjection;

namespace SBS.Infrastructure.Extensions
{
    public static class ContainerRegistryExtension
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ISubCategoryService, SubCategoryService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IAuthService, AuthService>();

            return services;
        }
    }
}
