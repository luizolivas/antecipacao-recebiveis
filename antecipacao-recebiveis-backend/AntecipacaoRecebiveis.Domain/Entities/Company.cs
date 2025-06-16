using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntecipacaoRecebiveis.Domain.Entities
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal MonthlyBiling { get; set; }
        public Sector Sector { get; set; }
        public Decimal CreditLimit { get; set; }
    }
}
