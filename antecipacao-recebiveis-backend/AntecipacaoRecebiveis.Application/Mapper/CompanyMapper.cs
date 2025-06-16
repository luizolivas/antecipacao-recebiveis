using AntecipacaoRecebiveis.Application.DTOs;
using AntecipacaoRecebiveis.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntecipacaoRecebiveis.Application.Mapper
{
    public static class CompanyMapper
    {
        public static CompanyDto ToDto(this Company company)
        {
            return new CompanyDto {
                Id = company.Id,
                nome = company.Name,
                cnpj = company.Cnpj, 
                faturamentoMensal = company.MonthlyBiling,
                setor = company.Sector,
                creditLimit = company.CreditLimit
            };
        }

        public static Company ToEntity(this CompanyDto dto)
        {
            return new Company {
                Id = dto.Id,
                Name = dto.nome,
                MonthlyBiling = dto.faturamentoMensal,
                Sector = dto.setor,
                CreditLimit = dto.creditLimit,
                Cnpj = dto.cnpj               
            };
        }
    }
}
