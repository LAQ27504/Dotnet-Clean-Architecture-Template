namespace blazorclean.Application.DTOs.Common
{
    public class PaginationResponse<TResponse>
    {
        public required ICollection<TResponse> Items { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalItem { get; set; }
        public int TotalPages => (int)Math.Ceiling(TotalItem / (double)PageSize);
    }
}
