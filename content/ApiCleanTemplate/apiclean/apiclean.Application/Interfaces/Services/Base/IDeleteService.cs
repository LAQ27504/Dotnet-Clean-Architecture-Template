using apiclean.Application.Common;

namespace apiclean.Application.Interfaces.Services.Base
{
    public interface IDeleteService
    {
        Task<OperationResult<string>> Delete(Guid id);
    }
}
