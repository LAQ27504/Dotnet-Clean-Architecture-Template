using apiclean.Application.DTOs.Common;
using apiclean.Application.DTOs.Query;

namespace apiclean.Application.Extensions
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
