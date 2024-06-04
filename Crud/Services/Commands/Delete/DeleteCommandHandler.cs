using Crud.Services.Commands.Delete;
using Crud.Services.Models;
using Datos;
using Datos.Entidades;

namespace Crud.Services.Commands.Upsert
{
    public class DeleteCommandHandler
    {
        private EfContext _ctx;
        private DeleteCommandResponse _response;

        public DeleteCommandHandler(EfContext ctx)
        {
            _ctx = ctx;
            _response = new DeleteCommandResponse();

        }

        public DeleteCommandResponse Handler(DeleteCommandRequest request)
        {

            var cliente = _ctx.ClienteRepo.Get(request.Id);
            if(cliente!= null)
            {
                _ctx.ClienteRepo.Delete(cliente);
            }

            _ctx.SaveChanges();

            _response.Result = "Registro eliminado con exito";

            return _response;
        }
    }
}
