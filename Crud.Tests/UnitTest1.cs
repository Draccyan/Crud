using Crud.Services.Commands.Get;
using Datos.Entidades;
using Datos.Repos;
using Moq;

namespace Crud.Tests
{
    public class GetClienteTests
    {
        [Fact]
        public void Handler_WhenIdIsZero_ShouldReturnNullOrNotFound()
        {
            // ARRANGE
            var repoMock = new Mock<IClienteRepo>();
            // Configuramos el mock para que devuelva null si el ID es 0
            repoMock.Setup(r => r.Get(0)).Returns((Clientes)null);

            var handler = new GetCommandHandler(repoMock.Object);
            var request = new GetCommandRequest { Id = 0 };

            // ACT
            var result = handler.Handler(request);

            // ASSERT
            Assert.False(result.Success);
            Assert.Equal("El cliente solicitado no existe", result.Message);
        }

        [Fact]
        public void Handler_WhenIdIsValid_ShouldReturnMappedClient()
        {
            // ARRANGE
            var repoMock = new Mock<IClienteRepo>();
            var clienteFake = new Clientes
            {
                Id = 10,
                Nombre = "Zair",
                Email = "zair@test.com"
            };

            repoMock.Setup(r => r.Get(10)).Returns(clienteFake);

            var handler = new GetCommandHandler(repoMock.Object);
            var request = new GetCommandRequest { Id = 10 };

            // ACT
            var result = handler.Handler(request);

            // ASSERT
            Assert.True(result.Success);
            Assert.NotNull(result.Data);
            Assert.Equal("Zair", result.Data.Nombre);
            Assert.Equal("zair@test.com", result.Data.Email);
            Assert.Equal(10, result.Data.Id);
        }
    }
}