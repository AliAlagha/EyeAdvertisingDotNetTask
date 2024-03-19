using System.Collections.Generic;

namespace EyeAdvertisingDotNetTask.Core.ViewModels.General
{
    public class PagingResultViewModel<TViewModel> where TViewModel : IBaseViewModel
    {
        public int TotalCount { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public List<TViewModel> Data { get; set; }
    }
}
