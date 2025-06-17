using AntecipacaoRecebiveis.Application.DTOs;
using AntecipacaoRecebiveis.Application.Interfaces;
using AntecipacaoRecebiveis.Application.Mapper; 
using Microsoft.AspNetCore.Mvc;

namespace AntecipacaoRecebiveis.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NfeController : ControllerBase
    {
        private readonly INfeService _nfeService;

        public NfeController(INfeService nfeService)
        {
            _nfeService = nfeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var nfes = await _nfeService.GetAllAsync();
            var result = nfes.Select(n => n.ToDto());
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var nfe = await _nfeService.GetByIdAsync(id);
            return nfe == null ? NotFound() : Ok(nfe.ToDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] NfeDto dto)
        {
            var nfe = dto.ToEntity();
            var created = await _nfeService.CreateAsync(nfe);
            return Ok(created.ToDto());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] NfeDto dto)
        {
            if (id != dto.Id) return BadRequest();

            var nfe = dto.ToEntity();
            var updated = await _nfeService.UpdateAsync(nfe);
            return Ok(updated.ToDto());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _nfeService.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("company/{id}")]
        public async Task<IActionResult> GetByCompanyId(int id)
        {
            var nfes = await _nfeService.GetByCompanyIdAsync(id);
            var result = nfes.Select(n => n.ToDto());
            return Ok(result);
        }

        [HttpGet("total-liquido/{companyId}")]
        public async Task<IActionResult> GetTotalLiquido(int companyId)
        {
            var total = await _nfeService.CalculateNetTotalAsync(companyId);
            return Ok(total);
        }
    }
}
