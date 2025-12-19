using Crud.Services.Models;
using Datos;
using Datos.Entidades;
using Datos.Repos;

namespace Crud.Services.Commands.Upsert
{
    public class UpsertCommandHandler
    {
        private IClienteRepo _clienteRepo;

        public UpsertCommandHandler(IClienteRepo clienteRepo)
        {
            _clienteRepo = clienteRepo;
        }

        public BaseResponse<string> Handler(UpsertCommandRequest request)
        {
            try
            {
                if (request.Data == null)
                    return new BaseResponse<string> { Success = false, Message = "No se recibieron datos del cliente." };

                if (string.IsNullOrWhiteSpace(request.Data.Nombre))
                    return new BaseResponse<string> { Success = false, Message = "El campo Nombre es requerido." };

                if (string.IsNullOrWhiteSpace(request.Data.Apellido))
                    return new BaseResponse<string> { Success = false, Message = "El campo Apellido es requerido." };

                if (string.IsNullOrWhiteSpace(request.Data.Celular))
                    return new BaseResponse<string> { Success = false, Message = "El campo Celular es requerido." };

                if (request.Data.FechaDeNacimiento == null)
                    return new BaseResponse<string> { Success = false, Message = "El campo Fecha de nacimiento es requerido." };

                if (string.IsNullOrWhiteSpace(request.Data.Cuit) || request.Data.Cuit.Length != 11)
                    return new BaseResponse<string> { Success = false, Message = "El campo Cuit no tiene el formato correcto (debe tener 11 dígitos)." };

                if (!string.IsNullOrWhiteSpace(request.Data.Email) && !request.Data.Email.Contains("@"))
                    return new BaseResponse<string> { Success = false, Message = "El campo Email no tiene el formato correcto." };

                bool isInsert = request.Id == 0;

                if (isInsert)
                {
                    var model = new Clientes
                    {
                        Nombre = request.Data.Nombre,
                        Apellido = request.Data.Apellido,
                        FechaDeNacimiento = request.Data.FechaDeNacimiento.GetValueOrDefault(),
                        Celular = request.Data.Celular,
                        Email = request.Data.Email,
                        Cuit = request.Data.Cuit,
                        Domicilio = request.Data.Domicilio
                    };
                    _clienteRepo.Insert(model);
                }
                else
                {
                    var cliente = _clienteRepo.Get(request.Id);
                    if (cliente == null)
                        return new BaseResponse<string> { Success = false, Message = "No se encontró el cliente para actualizar." };

                    cliente.Nombre = request.Data.Nombre;
                    cliente.Apellido = request.Data.Apellido;
                    cliente.Domicilio = request.Data.Domicilio;
                    cliente.FechaDeNacimiento = request.Data.FechaDeNacimiento.GetValueOrDefault();
                    cliente.Cuit = request.Data.Cuit;
                    cliente.Celular = request.Data.Celular;
                    cliente.Email = request.Data.Email;

                    _clienteRepo.Update(cliente);
                }

                _clienteRepo.SaveChanges();

                return new BaseResponse<string>
                {
                    Success = true,
                    Message = isInsert ? "Registro insertado con éxito" : "Registro actualizado con éxito",
                    Data = isInsert ? "INSERT_OK" : "UPDATE_OK"
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<string>
                {
                    Success = false,
                    Message = $"Error crítico: {ex.Message}"
                };
            }
        }
    }
}
