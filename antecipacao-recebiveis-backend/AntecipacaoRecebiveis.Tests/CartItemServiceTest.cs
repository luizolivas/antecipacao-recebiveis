using AntecipacaoRecebiveis.Application.Interfaces;
using AntecipacaoRecebiveis.Application.Services;
using AntecipacaoRecebiveis.Domain.Entities;
using AntecipacaoRecebiveis.Domain.Exceptions;
using AntecipacaoRecebiveis.Infrastructure.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntecipacaoRecebiveis.Tests
{
    public class CartItemServiceTest
    {
        private readonly Mock<ICartItemRepository> _repositoryMock;
        private readonly Mock<ICompanyService> _companyServiceMock;
        private readonly Mock<INfeService> _nfeServiceMock;
        private readonly Mock<IBuilderJsonCalculateValues> _builderJsonMock;

        private readonly CartItemService _cartItemService;

        public CartItemServiceTest()
        {
            _repositoryMock = new Mock<ICartItemRepository>();
            _companyServiceMock = new Mock<ICompanyService>();
            _nfeServiceMock = new Mock<INfeService>();
            _builderJsonMock = new Mock<IBuilderJsonCalculateValues>();

            _cartItemService = new CartItemService(
                _repositoryMock.Object,
                _companyServiceMock.Object,
                _nfeServiceMock.Object,
                _builderJsonMock.Object);
        }

        [Fact]
        public async Task AddAsync_ShouldAddCartItem_WhenNfeExistsAndWithinCreditLimit()
        {
            var cartItem = new CartItem { Id = 1, CompanyId = 1, NfeId = 1 };
            var nfe = new Nfe { Id = 1, Value = 1000m };

            _nfeServiceMock.Setup(s => s.GetByIdAsync(cartItem.NfeId))
                .ReturnsAsync(nfe);

            _companyServiceMock.Setup(s => s.GetCreditLimitByIdAsync(cartItem.CompanyId))
                .ReturnsAsync(5000m);

            _repositoryMock.Setup(r => r.GetTotalValorBrutoByCompanyidAsync(cartItem.CompanyId))
                .ReturnsAsync(2000m);

            _repositoryMock.Setup(r => r.AddAsync(cartItem))
                .ReturnsAsync(cartItem);

            var result = await _cartItemService.AddAsync(cartItem);

            Assert.NotNull(result);
            Assert.Equal(cartItem.Id, result.Id);
            _repositoryMock.Verify(r => r.AddAsync(cartItem), Times.Once);
        }

        [Fact]
        public async Task AddAsync_ShouldThrowException_WhenNfeNotFound()
        {
            var cartItem = new CartItem { Id = 1, CompanyId = 1, NfeId = 99 };

            _nfeServiceMock.Setup(s => s.GetByIdAsync(cartItem.NfeId))
                .ReturnsAsync((Nfe?)null);

            var exception = await Assert.ThrowsAsync<Exception>(() => _cartItemService.AddAsync(cartItem));
            Assert.Equal("Nota fiscal não encontrada.", exception.Message);
        }

        [Fact]
        public async Task AddAsync_ShouldThrowCreditLimitExceededException_WhenValueExceedsLimit()
        {
            var cartItem = new CartItem { Id = 1, CompanyId = 1, NfeId = 1 };
            var nfe = new Nfe { Id = 1, Value = 4000m };

            _nfeServiceMock.Setup(s => s.GetByIdAsync(cartItem.NfeId))
                .ReturnsAsync(nfe);

            _repositoryMock.Setup(r => r.GetTotalValorBrutoByCompanyidAsync(cartItem.CompanyId))
                .ReturnsAsync(2000m); 

            _companyServiceMock.Setup(s => s.GetCreditLimitByIdAsync(cartItem.CompanyId))
                .ReturnsAsync(5000m); 

            var exception = await Assert.ThrowsAsync<CreditLimitExceededException>(() => _cartItemService.AddAsync(cartItem));
            Assert.Equal("O valor ultrapassa o limite de crédito da empresa.", exception.Message);
        }
    }

}
