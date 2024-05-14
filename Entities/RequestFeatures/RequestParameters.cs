namespace Entities.RequestFeatures
{
	public class RequestParameters
	{
		const int MaxPageSize = 96;
		public int PageNumber { get; set; } = 1;
		private int _pageSize = MaxPageSize;
        public int PageSize { get { return _pageSize; } set { _pageSize = value > MaxPageSize ? MaxPageSize : value; } }
        public uint MinPrice { get; set; }
		public uint MaxPrice { get; set; } = 1000;
		public bool ValidPriceRange => MaxPrice > MinPrice;
		public string SearchTerm { get; set; } = string.Empty;
		public string OrderBy { get; set; } = string.Empty;
		public string Fields { get; set; } = string.Empty;
    }
}
