using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntecipacaoRecebiveis.Application.DTOs
{
    public class CartItemDto
    {
        public int Id { get; set; }
        public int companyId { get; set; }
        public int nfeId { get; set; }
    }
}
