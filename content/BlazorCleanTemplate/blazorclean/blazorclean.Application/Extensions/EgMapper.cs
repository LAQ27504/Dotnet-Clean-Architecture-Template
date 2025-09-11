using blazorclean.Application.DTOs.Common;
using blazorclean.Application.DTOs.Query;

namespace blazorclean.Application.Extensions
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
