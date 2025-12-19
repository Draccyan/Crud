using Crud.Services.Commands.GetAll;
using Crud.Services.Models;
using Datos;
using Datos.Repos;

namespace Crud.Services.Commands.GetAll
{
    public class GetAllCommandHandler 
    {
        private readonly IClienteRepo _clienteRepo;

        public GetAllCommandHandler(IClienteRepo clienteRepo)
        {
            _clienteRepo = clienteRepo;
        }

        public BaseResponse<List<ClienteModel>> Handler(GetAllCommandRequest request)
        {
            var clientes = _clienteRepo.GetAll();

            var listaMapeada = clientes?.Select(c => new ClienteModel
            {
                Id = c.Id,
                Nombre = c.Nombre,
                Apellido = c.Apellido,
                FechaDeNacimiento = c.FechaDeNacimiento,
                Cuit = c.Cuit,
                Domicilio = c.Domicilio,
                Celular = c.Celular,
                Email = c.Email
            }).ToList() ?? new List<ClienteModel>();

            return new BaseResponse<List<ClienteModel>>
            {
                Success = true,
                Message = "Clientes listados con éxito",
                Data = listaMapeada
            };
        }
    }
}
