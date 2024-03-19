namespace EyeAdvertisingDotNetTask.Core.Dtos.General
{
    public class PagingDto
    {
        public PagingDto(int page, int pageSize)
        {
            PageSize = pageSize;
            Page = page;
        }

        public PagingDto()
        {
        }

        public int Page { get; }
        public int PageSize { get; }
    }
}