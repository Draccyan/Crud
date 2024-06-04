using Crud.Services.Models;
using Datos;

namespace Crud.Services.Commands.Search
{
    public class SearchCommandHandler
    {
        private EfContext _ctx;
        private SearchCommandResponse _response;

        public SearchCommandHandler(EfContext ctx)
        {
            _ctx = ctx;
            _response = new SearchCommandResponse();

        }

        public SearchCommandResponse Handler(SearchCommandRequest request)
        {
            var clientes = _ctx.ClienteRepo.Search(request.Nombre);
            if(clientes != null)
            {
                var clientesModel = new List<ClienteModel>();
                foreach (var cliente in clientes)
                {
                    var model = new ClienteModel
                    {
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
