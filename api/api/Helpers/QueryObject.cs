namespace api.Helpers
{
	public class QueryObject
	{
		public string? Symbol { get; set; } = null;
		public string? ComapnyName { get; set; } = null;
		public string? SortBy { get; set; } = null;
        public bool isDecsending { get; set; } = false;
    }
}
