using AntecipacaoRecebiveis.Application.Interfaces;
using AntecipacaoRecebiveis.Application.Services;
using AntecipacaoRecebiveis.Infrastructure.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntecipacaoRecebiveis.Tests
{
    public class NfeServiceTests
    {
        private readonly Mock<INfeRepository> _nfeRepositoryMock;
        private readonly INfeService _nfeService;

        public NfeServiceTests()
        {
            _nfeRepositoryMock = new Mock<INfeRepository>();
            _nfeService = new NfeService(_nfeRepositoryMock.Object);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnNfeList()
        {
            var nfes = new List<Nfe>
            {
                new Nfe { Id = 1, Number = "001", ExpirationDate = DateTime.Now, CompanyId = 1 },
                new Nfe { Id = 2, Number = "002", ExpirationDate = DateTime.Now, CompanyId = 1 }
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
            var nfe = new Nfe { Id = 1, Number = "002", ExpirationDate = DateTime.Now, CompanyId = 1 };

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
            var nfe = new Nfe { Id = 1, Number = "999", ExpirationDate = DateTime.Now, CompanyId = 2 };

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
            var nfe = new Nfe { Id = id, Number = "001", ExpirationDate = DateTime.Now, CompanyId = 1 };

            _nfeRepositoryMock
                .Setup(repo => repo.GetByIdAsync(id))
                .ReturnsAsync(nfe);

            var result = await _nfeService.GetByIdAsync(id);

            Assert.NotNull(result);
            Assert.Equal(id, result.Id);
            _nfeRepositoryMock.Verify(repo => repo.GetByIdAsync(id), Times.Once);
        }
    }
}
