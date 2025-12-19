using Crud.Services.Commands.Search;
using Crud.Services.Models;
using Datos;
using Datos.Repos;

namespace Crud.Services.Commands.Get
{
    public class GetCommandHandler
    {
        private IClienteRepo _clienteRepo;

        public GetCommandHandler(IClienteRepo clienteRepo)
        {
            _clienteRepo = clienteRepo;
        }

        public BaseResponse<ClienteModel> Handler(GetCommandRequest request)
        {
            var cliente = _clienteRepo.Get(request.Id);

            // Validación si no existe
            if (cliente == null)
            {
                return new BaseResponse<ClienteModel>
                {
                    Success = false,
                    Message = "El cliente solicitado no existe",
                    Data = null
                };
            }

            // Mapeo manual
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

            return new BaseResponse<ClienteModel>
            {
                Success = true,
                Message = "Cliente recuperado con éxito",
                Data = model
            };
        }
    }
}
