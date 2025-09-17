using mudblazorclean.Application.DTOs.Common;
using mudblazorclean.Application.DTOs.Query;
using mudblazorclean.Application.DTOs.Requests;
using mudblazorclean.Application.DTOs.Responses;
using mudblazorclean.Application.Interfaces.Services.Base;

namespace mudblazorclean.Application.Interfaces.Services
{
    public interface IEgService
        : ICreateService<EgRequest>,
            IDeleteService,
            IGetByIdService<EgResponse>,
            IGetPaginationService<EgPaginationRequest, PaginationResponse<EgDetailsResponse>>,
            IUpdateService<EgUpdateRequest> { }
}
