using Crud.Services.Commands.Search;
using Crud.Services.Models;
using Datos;

namespace Crud.Services.Commands.Get
{
    public class GetCommandHandler
    {
        private EfContext _ctx;
        private GetCommandResponse _response;

        public GetCommandHandler(EfContext ctx)
        {
            _ctx = ctx;
            _response = new GetCommandResponse();
        }

        public GetCommandResponse Handler(GetCommandRequest request)
        {
            var cliente = _ctx.ClienteRepo.Get(request.Id);
            
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


            _response.Cliente = model;

            return _response;
        }
    }
}
