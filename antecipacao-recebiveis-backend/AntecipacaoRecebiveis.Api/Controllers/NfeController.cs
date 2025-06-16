using AntecipacaoRecebiveis.Application.Interfaces;
using Microsoft.AspNetCore.Http;
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
        public async Task<IActionResult> GetAll() => Ok(await _nfeService.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _nfeService.GetByIdAsync(id);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Nfe nfe) => Ok(await _nfeService.CreateAsync(nfe));

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Nfe nfe)
        {
            if (id != nfe.Id) return BadRequest();
            return Ok(await _nfeService.UpdateAsync(nfe));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _nfeService.DeleteAsync(id);
            return NoContent();
        }
    }
}
