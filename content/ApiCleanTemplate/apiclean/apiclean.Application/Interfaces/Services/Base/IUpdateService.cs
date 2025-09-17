using apiclean.Application.Common;

namespace apiclean.Application.Interfaces.Services.Base
{
    public interface IUpdateService<TUpdateRequest>
        where TUpdateRequest : class
    {
        Task<OperationResult<string>> Update(Guid Id, TUpdateRequest request);
    }
}
