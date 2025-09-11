using blazorclean.Application.Common;

namespace blazorclean.Application.Interfaces.Services.Base
{
    public interface IGetByIdService<TResponse>
        where TResponse : class
    {
        Task<OperationResult<TResponse>> GetById(Guid id);
    }
}
