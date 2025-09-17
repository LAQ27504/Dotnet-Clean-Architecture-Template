using mudblazorclean.Application.Common;

namespace mudblazorclean.Application.Interfaces.Services.Base
{
    public interface ICreateService<TCreateRequest>
        where TCreateRequest : class
    {
        Task<OperationResult<string>> Create(TCreateRequest request);
    }
}
