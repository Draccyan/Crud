using Crud.Services.Commands.GetAll;
using Crud.Services.Models;
using Datos;

namespace Crud.Services.Commands.GetAll
{
    public class GetAllCommandHandler
    {
        private EfContext _ctx;
        private GetAllCommandResponse _response;
        public GetAllCommandHandler(EfContext ctx)
        {
            _ctx = ctx;
            _response = new GetAllCommandResponse();
        }

        public GetAllCommandResponse Handler(GetAllCommandRequest request)
        {
            var clientes = _ctx.ClienteRepo.GetAll();
            if (clientes != null)
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
