using AntecipacaoRecebiveis.Application.Interfaces;
using AntecipacaoRecebiveis.Application.Services;
using AntecipacaoRecebiveis.Domain.Entities;
using AntecipacaoRecebiveis.Infrastructure.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AntecipacaoRecebiveis.Tests
{
    public class NfeServiceTests
    {
        private readonly Mock<INfeRepository> _nfeRepositoryMock;
        private readonly INfeService _nfeService;
        private readonly Mock<ICartItemRepository> _cartItemRepositoryMock;

        public NfeServiceTests()
        {
            _nfeRepositoryMock = new Mock<INfeRepository>();
            _cartItemRepositoryMock = new Mock<ICartItemRepository>();
            _nfeService = new NfeService(_nfeRepositoryMock.Object, _cartItemRepositoryMock.Object);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnNfeList()
        {
            var nfes = new List<Nfe>
            {
                new Nfe { Id = 1, Number = "001", ExpirationDate = DateTime.Now, Value= 20000, CompanyId = 1 },
                new Nfe { Id = 2, Number = "002", ExpirationDate = DateTime.Now,Value= 30000, CompanyId = 1 }
            };

            _nfeRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(nfes);

            var result = await _nfeService.GetAllAsync();

            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            _nfeRepositoryMock.Verify(repo => repo.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task CreateAsync_ShouldCreateNfe()
        {
            var nfe = new Nfe { Id = 1, Number = "002", ExpirationDate = DateTime.Now, Value = 30000, CompanyId = 1 };

            _nfeRepositoryMock.Setup(repo => repo.CreateAsync(nfe)).ReturnsAsync(nfe);

            var result = await _nfeService.CreateAsync(nfe);

            Assert.NotNull(result);
            Assert.Equal("002", result.Number);
            Assert.Equal(1, result.CompanyId);

            _nfeRepositoryMock.Verify(repo => repo.CreateAsync(nfe), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateNfe()
        {
            var nfe = new Nfe { Id = 1, Number = "999", ExpirationDate = DateTime.Now, Value = 30000, CompanyId = 2 };

            _nfeRepositoryMock
                .Setup(repo => repo.UpdateAsync(nfe))
                .ReturnsAsync(nfe);

            var result = await _nfeService.UpdateAsync(nfe);

            Assert.NotNull(result);
            Assert.Equal("999", result.Number);
            Assert.Equal(2, result.CompanyId);
            _nfeRepositoryMock.Verify(repo => repo.UpdateAsync(nfe), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ShouldCallRepositoryOnce()
        {
            int idToDelete = 1;

            _nfeRepositoryMock
                .Setup(repo => repo.DeleteAsync(idToDelete))
                .Returns(Task.CompletedTask);

            await _nfeService.DeleteAsync(idToDelete);

            _nfeRepositoryMock.Verify(repo => repo.DeleteAsync(idToDelete), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnNfe_WhenExists()
        {
            int id = 1;
            var nfe = new Nfe { Id = id, Number = "001", ExpirationDate = DateTime.Now, Value = 30000, CompanyId = 1 };

            _nfeRepositoryMock
                .Setup(repo => repo.GetByIdAsync(id))
                .ReturnsAsync(nfe);

            var result = await _nfeService.GetByIdAsync(id);

            Assert.NotNull(result);
            Assert.Equal(id, result.Id);
            _nfeRepositoryMock.Verify(repo => repo.GetByIdAsync(id), Times.Once);
        }

        [Fact]
        public async Task GetByCompanyIdAsync_ShouldReturnNfesOfCompany()
        {
            int companyId = 1;
            var nfes = new List<Nfe>
            {
        new Nfe { Id = 1, Number = "001", ExpirationDate = DateTime.Now.AddDays(10), Value = 10000, CompanyId = companyId },
        new Nfe { Id = 2, Number = "002", ExpirationDate = DateTime.Now.AddDays(20), Value = 20000, CompanyId = companyId }
    };

            _nfeRepositoryMock
                .Setup(repo => repo.GetByCompanyIdAsync(companyId))
                .ReturnsAsync(nfes);

            var result = await _nfeService.GetByCompanyIdAsync(companyId);

            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.All(result, nfe => Assert.Equal(companyId, nfe.CompanyId));

            _nfeRepositoryMock.Verify(repo => repo.GetByCompanyIdAsync(companyId), Times.Once);
        }

        [Fact]
        public async Task CalculateNetTotalAsync_ShouldReturnCorrectNetValue()
        {
            int companyId = 1;
            var today = new DateTime(2025, 6, 17); 

            var nfe1 = new Nfe { Id = 1, Number = "001", ExpirationDate = today.AddDays(30), Value = 30000, CompanyId = companyId };
            var nfe2 = new Nfe { Id = 2, Number = "002", ExpirationDate = today.AddDays(60), Value = 20000, CompanyId = companyId };

            var cartItems = new List<CartItem>
            {
        new CartItem { Id = 1, NfeId = nfe1.Id, Nfe = nfe1 },
        new CartItem { Id = 2, NfeId = nfe2.Id, Nfe = nfe2 }
    };

            _cartItemRepositoryMock
                .Setup(s => s.GetAllByCompanyIdAsync(companyId))
                .ReturnsAsync(cartItems);

            var result = await _nfeService.CalculateNetTotalAsync(companyId);

            const double rate = 0.0465;
            double expected1 = 30000 / Math.Pow(1 + rate, (Math.Max(0, (nfe1.ExpirationDate - today).Days)) / 30.0);
            double expected2 = 20000 / Math.Pow(1 + rate, (Math.Max(0, (nfe2.ExpirationDate - today).Days)) / 30.0);
            decimal expectedTotal = (decimal)(expected1 + expected2);

            Assert.Equal(Math.Round(expectedTotal, 2), Math.Round(result, 2));
        }
    }
}
