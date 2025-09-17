using mudblazorclean.Application.Common;

namespace mudblazorclean.Application.Interfaces.Services.Base
{
    public interface IDeleteService
    {
        Task<OperationResult<string>> Delete(Guid id);
    }
}
