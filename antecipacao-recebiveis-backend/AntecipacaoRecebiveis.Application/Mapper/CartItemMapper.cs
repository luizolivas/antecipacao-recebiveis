using AntecipacaoRecebiveis.Application.DTOs;
using AntecipacaoRecebiveis.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntecipacaoRecebiveis.Application.Mapper
{
    public static class CartItemMapper
    {
        public static CartItemDto ToDto(this CartItem item) => new() {
            Id = item.Id,
            companyId = item.CompanyId,
            nfeId = item.NfeId
        };

        public static CartItem ToEntity(this CartItemDto dto) => new() {
            Id = dto.Id,
            CompanyId = dto.companyId,
            NfeId = dto.nfeId
        };
    }
}
