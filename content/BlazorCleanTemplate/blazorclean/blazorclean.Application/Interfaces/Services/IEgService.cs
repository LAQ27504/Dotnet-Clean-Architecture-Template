using blazorclean.Application.DTOs.Common;
using blazorclean.Application.DTOs.Query;
using blazorclean.Application.DTOs.Requests;
using blazorclean.Application.DTOs.Responses;
using blazorclean.Application.Interfaces.Services.Base;

namespace blazorclean.Application.Interfaces.Services
{
    public interface IEgService
        : ICreateService<EgRequest>,
            IDeleteService,
            IGetByIdService<EgResponse>,
            IGetPaginationService<EgPaginationRequest, PaginationResponse<EgDetailsResponse>>,
            IUpdateService<EgUpdateRequest> { }
}
