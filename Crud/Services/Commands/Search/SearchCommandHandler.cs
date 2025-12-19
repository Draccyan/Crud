using Crud.Services.Models;
using Datos;
using Datos.Repos;

namespace Crud.Services.Commands.Search
{
    public class SearchCommandHandler
    {
        private IClienteRepo _clienteRepo;

        public SearchCommandHandler(IClienteRepo clienteRepo)
        {
            _clienteRepo = clienteRepo;
        }

        public BaseResponse<List<ClienteModel>> Handler(SearchCommandRequest request)
        {
            // Buscamos en el repositorio por nombre
            var clientes = _clienteRepo.Search(request.Nombre);

            // Si clientes es null, devolvemos una lista vacía para no romper nada
            var data = clientes?.Select(c => new ClienteModel
            {
                Id = c.Id,
                Nombre = c.Nombre,
                Apellido = c.Apellido,
                FechaDeNacimiento = c.FechaDeNacimiento,
                Celular = c.Celular,
                Email = c.Email,
                Cuit = c.Cuit,
                Domicilio = c.Domicilio
            }).ToList() ?? new List<ClienteModel>();

            return new BaseResponse<List<ClienteModel>>
            {
                Success = true,
                Message = $"Se encontraron {data.Count} clientes",
                Data = data
            };
        }
    }
}
