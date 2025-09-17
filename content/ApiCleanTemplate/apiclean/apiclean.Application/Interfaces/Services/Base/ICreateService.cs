using apiclean.Application.Common;

namespace apiclean.Application.Interfaces.Services.Base
{
    public interface ICreateService<TCreateRequest>
        where TCreateRequest : class
    {
        Task<OperationResult<string>> Create(TCreateRequest request);
    }
}
