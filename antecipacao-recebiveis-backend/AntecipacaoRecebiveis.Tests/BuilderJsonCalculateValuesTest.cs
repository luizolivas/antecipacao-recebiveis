using AntecipacaoRecebiveis.Application.Services;
using AntecipacaoRecebiveis.Domain.Entities;
using AntecipacaoRecebiveis.Infrastructure.Interfaces;
using Moq;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntecipacaoRecebiveis.Tests
{
    public class BuilderJsonCalculateValuesTests
    {
        private readonly Mock<ICartItemRepository> _cartItemRepositoryMock;
        private readonly Mock<ICompanyRepository> _companyRepositoryMock;
        private readonly BuilderJsonCalculateValues _builder;

        public BuilderJsonCalculateValuesTests()
        {
            _cartItemRepositoryMock = new Mock<ICartItemRepository>();
            _companyRepositoryMock = new Mock<ICompanyRepository>();
            _builder = new BuilderJsonCalculateValues(_cartItemRepositoryMock.Object, _companyRepositoryMock.Object);
        }

        [Fact]
        public async Task GetDetailedCalculationJsonAsync_ShouldReturnCorrectStructure()
        {
            int companyId = 1;
            var company = new Company { Id = companyId, Name = "Empresa X", Cnpj = "00.000.000/0001-00", CreditLimit = 5000m };
            var cartItems = new List<CartItem>
            {
            new CartItem
            {
                Nfe = new Nfe { Number = "001", Value = 1000m, ExpirationDate = DateTime.Now.AddDays(10) }
            },
            new CartItem
            {
                Nfe = new Nfe { Number = "002", Value = 2000m, ExpirationDate = DateTime.Now.AddDays(20) }
            }
        };

            _companyRepositoryMock.Setup(r => r.GetByIdAsync(companyId)).ReturnsAsync(company);
            _cartItemRepositoryMock.Setup(r => r.GetAllByCompanyIdAsync(companyId)).ReturnsAsync(cartItems);

            var result = await _builder.GetDetailedCalculationJsonAsync(companyId);

            Assert.NotNull(result);

            var jsonString = JsonConvert.SerializeObject(result);
            var jsonObject = JObject.Parse(jsonString);

            Assert.Equal("Empresa X", (string)jsonObject["empresa"]);
            Assert.Equal("00.000.000/0001-00", (string)jsonObject["cnpj"]);
            Assert.Equal(5000m, (decimal)jsonObject["limite"]);
            Assert.Equal(2, jsonObject["notas_fiscais"].Count());


        }
    }

}
