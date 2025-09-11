using blazorclean.Application.Common;

namespace blazorclean.Application.Interfaces.Services.Base
{
    public interface IDeleteService
    {
        Task<OperationResult<string>> Delete(Guid id);
    }
}
