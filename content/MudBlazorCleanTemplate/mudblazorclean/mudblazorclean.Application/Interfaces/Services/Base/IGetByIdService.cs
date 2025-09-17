using mudblazorclean.Application.Common;

namespace mudblazorclean.Application.Interfaces.Services.Base
{
    public interface IGetByIdService<TResponse>
        where TResponse : class
    {
        Task<OperationResult<TResponse>> GetById(Guid id);
    }
}
