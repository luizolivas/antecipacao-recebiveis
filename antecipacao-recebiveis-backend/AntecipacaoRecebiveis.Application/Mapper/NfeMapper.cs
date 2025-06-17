using AntecipacaoRecebiveis.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntecipacaoRecebiveis.Application.Mapper
{
    public static class NfeMapper
    {
        public static NfeDto ToDto(this Nfe nfe)
        {
            return new NfeDto {
                Id = nfe.Id,
                numero = nfe.Number,
                valor = nfe.Value,
                dataVencimento = nfe.ExpirationDate,
                companyId = nfe.CompanyId
            };
        }

        public static Nfe ToEntity(this NfeDto dto)
        {
            return new Nfe {
                Id = dto.Id,
                Number = dto.numero,
                Value = dto.valor,
                ExpirationDate = dto.dataVencimento,
                CompanyId = dto.companyId
            };
        }
    }
}
