using Crud.Services.Commands.Search;
using Crud.Services.Models;
using Datos;
using Datos.Repos;

namespace Crud.Services.Commands.Get
{
    public class GetCommandHandler
    {
        private GetCommandResponse _response;
        private IClienteRepo _clienteRepo;

        public GetCommandHandler(IClienteRepo clienteRepo)
        {
            _response = new GetCommandResponse();
            _clienteRepo = clienteRepo;
        }

        public GetCommandResponse Handler(GetCommandRequest request)
        {
            var cliente = _clienteRepo.Get(request.Id);
            
                var model = new ClienteModel
                {
                    Id = request.Id,
                    Nombre = cliente.Nombre,
                    Apellido = cliente.Apellido,
                    FechaDeNacimiento = cliente.FechaDeNacimiento,
                    Celular = cliente.Celular,
                    Email = cliente.Email,
                    Cuit = cliente.Cuit,
                    Domicilio = cliente.Domicilio
                };


            _response.Cliente = model;

            return _response;
        }
    }
}
