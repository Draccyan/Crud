using Crud.Services.Models;
using Datos;
using Datos.Repos;

namespace Crud.Services.Commands.Search
{
    public class SearchCommandHandler
    {
        private IClienteRepo _clienteRepo;
        private SearchCommandResponse _response;

        public SearchCommandHandler(IClienteRepo clienteRepo)
        {
            _clienteRepo = clienteRepo;
            _response = new SearchCommandResponse();

        }

        public SearchCommandResponse Handler(SearchCommandRequest request)
        {
            var clientes = _clienteRepo.Search(request.Nombre);
            if(clientes != null)
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
