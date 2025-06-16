using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntecipacaoRecebiveis.Application.DTOs
{
    public class NfeDto
    {
        public int Id { get; set; }
        public string Numero { get; set; } = string.Empty;
        public decimal Valor { get; set; }
        public DateTime DataVencimento { get; set; }
        public int CompanyId { get; set; } 
    }
}
