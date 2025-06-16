using AntecipacaoRecebiveis.Application.Interfaces;
using AntecipacaoRecebiveis.Application.Services;
using AntecipacaoRecebiveis.Domain.Entities;
using AntecipacaoRecebiveis.Infrastructure.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntecipacaoRecebiveis.Tests
{
    public class CompanyServiceTest
    {
        private readonly Mock<ICompanyRepository> _companyRepositoryMock;
        private readonly ICompanyService _companyService;
        private readonly Mock<ICreditLimitCalculator> _creditLimitCalculatorMock;

        public CompanyServiceTest()
        {
            _companyRepositoryMock = new Mock<ICompanyRepository>();
            _creditLimitCalculatorMock = new Mock<ICreditLimitCalculator>();
            _companyService = new CompanyService(_companyRepositoryMock.Object,_creditLimitCalculatorMock.Object);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnCompanyList()
        {
            var companies = new List<Company>
            {
                new Company { Id = 1, Name = "Empresa A", MonthlyBiling = 10000, Sector = Sector.PRODUCAO, CreditLimit = 5000 },
                new Company { Id = 2, Name = "Empresa B", MonthlyBiling = 20000, Sector = Sector.PRODUCAO, CreditLimit = 10000 }
            };

            _companyRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(companies);

            var result = await _companyService.GetAllAsync();

            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            _companyRepositoryMock.Verify(repo => repo.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task CreateAsync_ShouldCreateCompany()
        {
            var company = new Company { Id = 1, Name = "Empresa Nova", MonthlyBiling = 15000, Sector = Sector.SERVICO, CreditLimit = 7000 };

            _companyRepositoryMock.Setup(repo => repo.CreateAsync(company)).ReturnsAsync(company);

            var result = await _companyService.CreateAsync(company);

            Assert.NotNull(result);
            Assert.Equal("Empresa Nova", result.Name);
            Assert.Equal(Sector.SERVICO, result.Sector);
            _companyRepositoryMock.Verify(repo => repo.CreateAsync(company), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateCompany()
        {
            var company = new Company { Id = 1, Name = "Empresa Atualizada", MonthlyBiling = 20000, Sector = Sector.PRODUCAO, CreditLimit = 10000 };

            _companyRepositoryMock.Setup(repo => repo.UpdateAsync(company)).ReturnsAsync(company);

            var result = await _companyService.UpdateAsync(company);

            Assert.NotNull(result);
            Assert.Equal("Empresa Atualizada", result.Name);
            Assert.Equal(Sector.PRODUCAO, result.Sector);
            _companyRepositoryMock.Verify(repo => repo.UpdateAsync(company), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ShouldCallRepositoryOnce()
        {
            int idToDelete = 1;

            _companyRepositoryMock.Setup(repo => repo.DeleteAsync(idToDelete)).Returns(Task.CompletedTask);

            await _companyService.DeleteAsync(idToDelete);

            _companyRepositoryMock.Verify(repo => repo.DeleteAsync(idToDelete), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnCompany_WhenExists()
        {
            int id = 1;
            var company = new Company { Id = id, Name = "Empresa Existente", MonthlyBiling = 10000, Sector = Sector.SERVICO, CreditLimit = 5000 };

            _companyRepositoryMock.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync(company);

            var result = await _companyService.GetByIdAsync(id);

            Assert.NotNull(result);
            Assert.Equal(id, result.Id);
            _companyRepositoryMock.Verify(repo => repo.GetByIdAsync(id), Times.Once);
        }
    }
}
