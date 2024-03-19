using AutoMapper;
using EyeAdvertisingDotNetTask.Core.Dtos.General;
using EyeAdvertisingDotNetTask.Core.ViewModels.General;
using EyeAdvertisingDotNetTask.Data.DbEntities;
using Microsoft.EntityFrameworkCore;

namespace EyeAdvertisingDotNetTask.Infrastructure.Extentions
{
    public static class PagingExtension
    {
        public static async Task<PagingResultViewModel<TViewModel>> ToPagedData<TViewModel>(this IQueryable<BaseEntity> query, PagingDto dto, IMapper mapper)
            where TViewModel : IBaseViewModel
        {
            var pageSize = dto.PageSize;
            var skip = (int)Math.Ceiling(pageSize * (decimal)(dto.Page - 1));

            var totalCount = await query.CountAsync();
            query = query.Skip(skip).Take(pageSize);

            return new PagingResultViewModel<TViewModel>
            {
                Page = dto.Page,
                PageSize = pageSize,
                TotalCount = totalCount,
                Data = mapper.Map<List<TViewModel>>(await query.ToListAsync())
            };
        }

    }
}
