using mudblazorclean.Application.DTOs.Common;
using mudblazorclean.Application.DTOs.Query;

namespace mudblazorclean.Application.Extensions
{
    public static class EgMapper
    {
        public static EgPaginationRequest ToEgPaginationRequest(this PaginationRequest request)
        {
            return new EgPaginationRequest
            {
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Query = request.Query,
            };
        }
    }
}
