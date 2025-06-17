using AntecipacaoRecebiveis.Application.DTOs;
using AntecipacaoRecebiveis.Application.Interfaces;
using AntecipacaoRecebiveis.Application.Mapper;
using AntecipacaoRecebiveis.Application.Services;
using AntecipacaoRecebiveis.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace AntecipacaoRecebiveis.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartItemService _cartItemService;

        public CartController(ICartItemService cartItemService)
        {
            _cartItemService = cartItemService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int companyId)
        {
            var items = await _cartItemService.GetAllByCompanyIdAsync(companyId);
            var result = items.Select(i => i.ToDto());
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CartItemDto dto)
        {
            try
            {
                var item = await _cartItemService.AddAsync(dto.ToEntity());
                return Ok(item.ToDto());
            }
            catch (CreditLimitExceededException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _cartItemService.RemoveAsync(id);
            return NoContent();
        }

        [HttpGet("total")]
        public async Task<IActionResult> GetTotal([FromQuery] int companyId)
        {
            var total = await _cartItemService.GetTotalCartValueAsync(companyId);
            return Ok(total);
        }

        [HttpGet("total-valor-bruto/{companyId}")]
        public async Task<IActionResult> GetTotalValorBruto(int companyId)
        {
            var total = await _cartItemService.CalculateTotalBrutoAsync(companyId);
            return Ok(total);
        }

        [HttpGet("calculo-detalhado/{companyId}")]
        public async Task<IActionResult> GetDetailedCalculation(int companyId)
        {
            var resultado = await _cartItemService.GetDetailedCalculationAsync(companyId);
            return Ok(resultado);
        }
    }
}
