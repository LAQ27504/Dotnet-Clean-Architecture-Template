using apiclean.Application.Common;

namespace apiclean.Application.Interfaces.Services.Base
{
    public interface IGetByIdService<TResponse>
        where TResponse : class
    {
        Task<OperationResult<TResponse>> GetById(Guid id);
    }
}
