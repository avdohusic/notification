namespace Models
{
    public class ResourceParameters
    {
        private const int MaxPageSize = 100;
        private int _pageSize = 50;

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value > MaxPageSize ? MaxPageSize : value;
        }

        public int PageNumber { get; set; } = 1;
        public string OrderBy { get; set; }
        public string Fields { get; set; }
        public string SearchString { get; set; }
    }
}
