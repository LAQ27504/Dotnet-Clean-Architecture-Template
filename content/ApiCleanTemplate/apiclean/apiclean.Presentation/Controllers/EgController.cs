using apiclean.Application.Common;
using apiclean.Application.DTOs.Common;
using apiclean.Application.DTOs.Query;
using apiclean.Application.DTOs.Requests;
using apiclean.Application.DTOs.Responses;
using apiclean.Application.Extensions;
using apiclean.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace apiclean.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //#if (isAuthen)
    [Authorize]
    //#endif
    public class EgController : BaseController
    {
        private readonly IEgService _egService;

        public EgController(IEgService egService)
        {
            _egService = egService;
        }

        [HttpPost("create")]
        //#if (isAuthen)
        [Authorize]
        //#endif
        [ProducesResponseType(typeof(string), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(FailResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(FailResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] EgRequest request)
        {
            var result = await _egService.Create(request);
            return ToActionResult(result);
        }

        [HttpDelete("{id:guid}/delete")]
        //#if (isAuthen)
        [Authorize]
        //#endif
        [ProducesResponseType(typeof(string), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(FailResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(FailResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _egService.Delete(id);
            return ToActionResult(result);
        }

        [HttpGet("{id:guid}/details")]
        //#if (isAuthen)
        [Authorize]
        //#endif
        [ProducesResponseType(typeof(EgResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(FailResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(FailResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetEntityDetails(Guid id)
        {
            var result = await _egService.GetById(id);
            return ToActionResult(result);
        }

        [HttpGet("pagination")]
        //#if (isAuthen)
        [Authorize]
        //#endif
        [ProducesResponseType(typeof(PaginationResponse<EgResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPagination([FromQuery] EgPaginationRequest request)
        {
            var result = await _egService.GetPagination(request);
            return ToActionResult(result);
        }

        [HttpPut("{Id:guid}/update")]
        //#if (isAuthen)
        [Authorize]
        //#endif
        [ProducesResponseType(typeof(string), StatusCodes.Status202Accepted)]
        [ProducesResponseType(typeof(FailResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(FailResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(Guid Id, [FromBody] EgUpdateRequest request)
        {
            var result = await _egService.Update(Id, request);
            return ToActionResult(result);
        }
    }
}
