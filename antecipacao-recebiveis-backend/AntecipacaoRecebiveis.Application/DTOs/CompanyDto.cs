using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntecipacaoRecebiveis.Application.DTOs
{
    public class CompanyDto
    {
        public int Id { get; set; }
        public string nome { get; set; }
        public string cnpj { get; set; }
        public decimal faturamentoMensal { get; set; }
        public Sector setor { get; set; }
        public decimal creditLimit { get; set; }
    }
}
