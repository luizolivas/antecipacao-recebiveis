
using AntecipacaoRecebiveis.Domain.Entities;

    public class Nfe
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public decimal Value { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }
    }

