using AntecipacaoRecebiveis.Application.DTOs;
using AntecipacaoRecebiveis.Application.Interfaces;
using AntecipacaoRecebiveis.Application.Mapper;
using Microsoft.AspNetCore.Mvc;

namespace AntecipacaoRecebiveis.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var companies = await _companyService.GetAllAsync();
            var result = companies.Select(c => c.ToDto());
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var company = await _companyService.GetByIdAsync(id);
            return company == null ? NotFound() : Ok(company.ToDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CompanyDto dto)
        {
            var company = dto.ToEntity();
            var created = await _companyService.CreateAsync(company);
            return Ok(created.ToDto());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CompanyDto dto)
        {
            if (id != dto.Id) return BadRequest();

            var company = dto.ToEntity();
            var updated = await _companyService.UpdateAsync(company);
            return Ok(updated.ToDto());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _companyService.DeleteAsync(id);
            return NoContent();
        }
    }
}
