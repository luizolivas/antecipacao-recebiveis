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
        public string numero { get; set; } = string.Empty;
        public decimal valor { get; set; }
        public DateTime dataVencimento { get; set; }
        public int companyId { get; set; } 
    }
}
