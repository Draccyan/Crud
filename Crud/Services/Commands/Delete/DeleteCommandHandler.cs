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

        public DeleteCommandHandler(IClienteRepo clienteRepo)
        {
            _clienteRepo = clienteRepo;
        }

        public BaseResponse<string> Handler(DeleteCommandRequest request)
        {
            try
            {
                var cliente = _clienteRepo.Get(request.Id);

                if (cliente == null)
                {
                    return new BaseResponse<string>
                    {
                        Success = false,
                        Message = "No se encontró el cliente que intenta eliminar.",
                        Data = null
                    };
                }

                _clienteRepo.Delete(cliente);
                _clienteRepo.SaveChanges();

                return new BaseResponse<string>
                {
                    Success = true,
                    Message = "Registro eliminado con éxito",
                    Data = request.Id.ToString() // Devolvemos el ID eliminado como dato
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<string>
                {
                    Success = false,
                    Message = $"Error al intentar eliminar: {ex.Message}"
                };
            }
        }
    }
}
