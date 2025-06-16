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
                Numero = nfe.Number,
                Valor = nfe.Value,
                DataVencimento = nfe.ExpirationDate,
                CompanyId = nfe.CompanyId
            };
        }

        public static Nfe ToEntity(this NfeDto dto)
        {
            return new Nfe {
                Id = dto.Id,
                Number = dto.Numero,
                Value = dto.Valor,
                ExpirationDate = dto.DataVencimento,
                CompanyId = dto.CompanyId
            };
        }
    }
}
