using blazorclean.Application.Common;

namespace blazorclean.Application.Interfaces.Services.Base
{
    public interface IUpdateService<TUpdateRequest>
        where TUpdateRequest : class
    {
        Task<OperationResult<string>> Update(Guid Id, TUpdateRequest request);
    }
}
