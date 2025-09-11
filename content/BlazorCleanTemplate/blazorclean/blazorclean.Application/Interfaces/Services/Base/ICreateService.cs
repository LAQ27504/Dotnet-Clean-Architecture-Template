using blazorclean.Application.Common;

namespace blazorclean.Application.Interfaces.Services.Base
{
    public interface ICreateService<TCreateRequest>
        where TCreateRequest : class
    {
        Task<OperationResult<string>> Create(TCreateRequest request);
    }
}
