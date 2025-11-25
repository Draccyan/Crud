using Crud.Services.Commands.Delete;
using Crud.Services.Models;
using Datos;
using Datos.Entidades;
using Datos.Repos;

namespace Crud.Services.Commands.Upsert
{
    public class DeleteCommandHandler
    {
        private IClienteRepo _clienteRepo;
        private DeleteCommandResponse _response;

        public DeleteCommandHandler(IClienteRepo clienteRepo)
        {
            _clienteRepo = clienteRepo;
            _response = new DeleteCommandResponse();

        }

        public DeleteCommandResponse Handler(DeleteCommandRequest request)
        {

            var cliente = _clienteRepo.Get(request.Id);
            if(cliente!= null)
            {
                _clienteRepo.Delete(cliente);
            }

            _clienteRepo.SaveChanges();

            _response.Result = "Registro eliminado con exito";

            return _response;
        }
    }
}
