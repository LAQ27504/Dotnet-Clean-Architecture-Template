using blazorclean.Application.Common;

namespace blazorclean.Application.Interfaces.Services.Base
{
    public interface IGetPaginationService<TPaginationRequest, TPaginationResponse>
        where TPaginationRequest : class
        where TPaginationResponse : class
    {
        Task<OperationResult<TPaginationResponse>> GetPagination(TPaginationRequest request);
    }
}
