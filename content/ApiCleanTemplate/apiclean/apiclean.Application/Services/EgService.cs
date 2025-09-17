using apiclean.Application.Common;
using apiclean.Application.DTOs.Common;
using apiclean.Application.DTOs.Query;
using apiclean.Application.DTOs.Requests;
using apiclean.Application.DTOs.Responses;
using apiclean.Application.Interfaces.Repositories;
using apiclean.Application.Interfaces.Services;
using apiclean.Application.Interfaces.UnitOfWork;
using apiclean.Domain.Entities;

namespace apiclean.Application.Services
{
    public class EgService : IEgService
    {
        private readonly IEgRepository _egRepository;
        private readonly IUnitOfWork _unitOfWork;

        public EgService(IEgRepository egRepository, IUnitOfWork unitOfWork)
        {
            _egRepository = egRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<OperationResult<string>> Create(EgRequest request)
        {
            var name = request.Name;

            if (string.IsNullOrWhiteSpace(name))
            {
                return OperationResult<string>.BadRequest("Name cannot be empty.");
            }

            var entity = new EgEntity { Id = Guid.NewGuid(), Name = name };

            await _egRepository.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();

            return OperationResult<string>.Created("Entity created successfully.");
        }

        public async Task<OperationResult<string>> Delete(Guid id)
        {
            var entity = await _egRepository.GetEntityByIdAsync(id);

            if (entity == null)
            {
                return OperationResult<string>.NotFound("Entity not found.");
            }

            _egRepository.Delete(entity);
            await _unitOfWork.SaveChangesAsync();

            return OperationResult<string>.NoContent();
        }

        public async Task<OperationResult<EgResponse>> GetById(Guid id)
        {
            var entity = await _egRepository.GetEntityByIdAsync(id);
            if (entity == null)
            {
                return OperationResult<EgResponse>.NotFound("Entity not found.");
            }

            var response = new EgResponse
            {
                Message =
                    $"Hello {entity.Name}. This is the getting demo for project and this should show this line to represent the getting details of this object",
            };
            return OperationResult<EgResponse>.Ok(response);
        }

        public async Task<OperationResult<PaginationResponse<EgDetailsResponse>>> GetPagination(
            EgPaginationRequest request
        )
        {
            QueryFilter<EgEntity>? filter = null;

            if (!string.IsNullOrWhiteSpace(request.Query))
            {
                var keyword = request.Query.ToLower();
                filter = query => query.Where(g => g.Name.ToLower().Contains(keyword));
            }

            var (entities, total) = await _egRepository.GetPagedAsync(
                filter,
                request.PageIndex,
                request.PageSize
            );

            // You can add the transfer with the dto mapper here
            var egEntities = entities
                .Select(entity => new EgDetailsResponse
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    Message = $"{entity.Id}: The name is {entity.Name}",
                })
                .ToList();

            var response = new PaginationResponse<EgDetailsResponse>
            {
                TotalItem = total,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Items = egEntities,
            };

            if (response.TotalPages < request.PageIndex)
            {
                response.PageIndex = response.TotalPages;
            }

            return OperationResult<PaginationResponse<EgDetailsResponse>>.Ok(response);
        }

        public async Task<OperationResult<string>> Update(Guid Id, EgUpdateRequest request)
        {
            var name = request.Name;

            if (string.IsNullOrWhiteSpace(name))
            {
                return OperationResult<string>.BadRequest("Name cannot be empty.");
            }

            var entity = await _egRepository.GetEntityByIdAsync(Id);

            if (entity == null)
            {
                return OperationResult<string>.NotFound("Entity not found.");
            }

            entity.Name = name;
            _egRepository.Update(entity);
            await _unitOfWork.SaveChangesAsync();

            return OperationResult<string>.Accepted("Entity updated successfully.");
        }
    }
}
