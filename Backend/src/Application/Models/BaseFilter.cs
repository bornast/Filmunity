namespace Application.Models
{
	public class BaseFilter
    {		
		public int PageNumber { get; set; } = 1;

		// TODO: how to extract these values from config?
		private int _pageSize = 10;		
		private const int _maxPageSize = 50;
		public int PageSize
		{
			get { return _pageSize; }
			set { _pageSize = value > _maxPageSize ? _maxPageSize : value; }
		}

		public int Skip { get => (PageNumber - 1) * _pageSize; }
		public int Take { get => _pageSize; }
		//public string OrderBy { get; set; }
	}
}
