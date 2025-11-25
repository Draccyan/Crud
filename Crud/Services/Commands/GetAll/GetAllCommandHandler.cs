using Crud.Services.Commands.GetAll;
using Crud.Services.Models;
using Datos;
using Datos.Repos;

namespace Crud.Services.Commands.GetAll
{
    public class GetAllCommandHandler
    {
        private IClienteRepo _clienteRepo;
        private GetAllCommandResponse _response;
        public GetAllCommandHandler(IClienteRepo clienteRepo)
        {
            _clienteRepo = clienteRepo;
            _response = new GetAllCommandResponse();
        }

        public GetAllCommandResponse Handler(GetAllCommandRequest request)
        {
            var clientes = _clienteRepo.GetAll();
            if (clientes != null)
            {
                var clientesModel = new List<ClienteModel>();
                foreach (var cliente in clientes)
                {
                    var model = new ClienteModel
                    {
                        Id = cliente.Id,
                        Nombre = cliente.Nombre,
                        Apellido = cliente.Apellido,
                        FechaDeNacimiento = cliente.FechaDeNacimiento,
                        Celular = cliente.Celular,
                        Email = cliente.Email,
                        Cuit = cliente.Cuit,
                        Domicilio = cliente.Domicilio
                    };

                    clientesModel.Add(model);
                }

                _response.Clientes = clientesModel;
            }

            return _response;
        }
    }
}
