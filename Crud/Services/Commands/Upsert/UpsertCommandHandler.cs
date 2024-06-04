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
            if(request.Data == null)
            {
                throw new InvalidOperationException("No recibi un cliente");
            }
            if(request.Data.Nombre == null || request.Data.Nombre == "")
            {
                throw new InvalidOperationException("El campo nombre es requerido");
            }
            if (request.Data.Apellido == null || request.Data.Apellido == "")
            {
                throw new InvalidOperationException("El campo Apellido es requerido");
            }
            if (request.Data.Celular == null)
            {
                throw new InvalidOperationException("El campo Celular es requerido");
            }
            if (request.Data.FechaDeNacimiento == null)
            {
                throw new InvalidOperationException("El campo Fecha de nacimiento es requerido");
            }
            if (request.Data.Cuit == null || (request.Data.Cuit != null && request.Data.Cuit.Length != 11 ))
            {
                throw new InvalidOperationException("El campo Cuit no tiene el formato correcto");
            }
            if (request.Data.Email != null)
            {
                if (!request.Data.Email.Contains("@"))
                {
                    throw new InvalidOperationException("El campo mail no tiene el formato correcto");
                }
            }

            var InsertOk = false;

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
                InsertOk = true;
            }
            else
            {
                var cliente = _ctx.ClienteRepo.Get(request.Id);
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

            if (InsertOk)
            {
                _response.Result = "Registro insertado con exito";
            }
            else
            {
                _response.Result = "Registro actualizado con exito";
            }

            return _response;
        }
    }
}
