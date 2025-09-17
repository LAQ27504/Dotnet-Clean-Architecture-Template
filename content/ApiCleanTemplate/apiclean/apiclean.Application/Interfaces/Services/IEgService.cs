using apiclean.Application.DTOs.Common;
using apiclean.Application.DTOs.Query;
using apiclean.Application.DTOs.Requests;
using apiclean.Application.DTOs.Responses;
using apiclean.Application.Interfaces.Services.Base;

namespace apiclean.Application.Interfaces.Services
{
    public interface IEgService
        : ICreateService<EgRequest>,
            IDeleteService,
            IGetByIdService<EgResponse>,
            IGetPaginationService<EgPaginationRequest, PaginationResponse<EgDetailsResponse>>,
            IUpdateService<EgUpdateRequest> { }
}
