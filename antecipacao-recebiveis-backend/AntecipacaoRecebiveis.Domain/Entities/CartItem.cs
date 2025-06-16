using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntecipacaoRecebiveis.Domain.Entities
{
    public class CartItem
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public int NfeId { get; set; }
        public Nfe Nfe { get; set; }
    }
}
