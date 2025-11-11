using Crud.Services.Models;
using Datos;
using Datos.Entidades;

namespace Crud.Services.Commands.Upsert
{
    public class UpsertCommandHandler
    {
        private EfContext _ctx;
        private UpsertCommandResponse _response;

        public UpsertCommandHandler(EfContext ctx)
        {
            _ctx = ctx;
            _response = new UpsertCommandResponse();

        }

        public UpsertCommandResponse Handler(UpsertCommandRequest request)
        {
            try
            {
                if (request.Data == null)
                    throw new InvalidOperationException("No recibi un cliente");

                if (string.IsNullOrWhiteSpace(request.Data.Nombre))
                    throw new InvalidOperationException("El campo Nombre es requerido");

                if (string.IsNullOrWhiteSpace(request.Data.Apellido))
                    throw new InvalidOperationException("El campo Apellido es requerido");

                if (string.IsNullOrWhiteSpace(request.Data.Celular))
                    throw new InvalidOperationException("El campo Celular es requerido");

                if (request.Data.FechaDeNacimiento == null)
                    throw new InvalidOperationException("El campo Fecha de nacimiento es requerido");

                if (string.IsNullOrWhiteSpace(request.Data.Cuit) || request.Data.Cuit.Length != 11)
                    throw new InvalidOperationException("El campo Cuit no tiene el formato correcto");

                if (!string.IsNullOrWhiteSpace(request.Data.Email) && !request.Data.Email.Contains("@"))
                    throw new InvalidOperationException("El campo Email no tiene el formato correcto");

                bool insertOk = false;

                if (request.Id == 0)
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

                    _ctx.ClienteRepo.Insert(model);
                    insertOk = true;
                }
                else
                {
                    var cliente = _ctx.ClienteRepo.Get(request.Id);
                    if (cliente == null)
                        throw new InvalidOperationException("No se encontró el cliente para actualizar.");

                    cliente.Nombre = request.Data.Nombre;
                    cliente.Apellido = request.Data.Apellido;
                    cliente.Domicilio = request.Data.Domicilio;
                    cliente.FechaDeNacimiento = request.Data.FechaDeNacimiento.GetValueOrDefault();
                    cliente.Cuit = request.Data.Cuit;
                    cliente.Celular = request.Data.Celular;
                    cliente.Email = request.Data.Email;

                    _ctx.ClienteRepo.Update(cliente);
                }

                _ctx.SaveChanges();

                _response.Result = insertOk
                    ? "Registro insertado con éxito"
                    : "Registro actualizado con éxito";
            }
            catch (InvalidOperationException ex)
            {
                // Captura errores de validación y los devuelve en el resultado
                _response.Result = $"Error: {ex.Message}";
            }
            catch (Exception ex)
            {
                // Captura cualquier otro error inesperado
                _response.Result = $"Error inesperado: {ex.Message}";
            }

            return _response;
        }

    }
}
