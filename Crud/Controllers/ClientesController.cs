using Crud.Services.Commands.Delete;
using Crud.Services.Commands.Get;
using Crud.Services.Commands.GetAll;
using Crud.Services.Commands.Search;
using Crud.Services.Commands.Upsert;
using Crud.Services.Models;
using Datos;
using Datos.Repos;
using Microsoft.AspNetCore.Mvc;

namespace Crud.Controllers
{
    [ApiController]
    [Route("Crud")]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteRepo _clienteRepo;

        public ClientesController(IClienteRepo clienteRepo)
        {
            _clienteRepo = clienteRepo;
        }

        [HttpGet("getAll")]
        public BaseResponse<List<ClienteModel>> GetAll([FromQuery] GetAllCommandRequest request)
        {
            var commandHandler = new GetAllCommandHandler(_clienteRepo);
            var response = commandHandler.Handler(request);
            return response;
        }

        [HttpGet]
        [Route("get")]
        public BaseResponse<ClienteModel> Get([FromQuery] GetCommandRequest request)
        {
            var commandHandler = new GetCommandHandler(_clienteRepo);
            return commandHandler.Handler(request);
        }

        [HttpGet]
        [Route("search")]
        public BaseResponse<List<ClienteModel>> Search([FromQuery] SearchCommandRequest request)
        {
            var commandHandler = new SearchCommandHandler(_clienteRepo);
            return commandHandler.Handler(request);
        }

        [HttpPost]
        [Route("upsert")]
        public BaseResponse<string> Upsert([FromBody] UpsertCommandRequest request)
        {
            var commandHandler = new UpsertCommandHandler(_clienteRepo);
            return commandHandler.Handler(request);
        }

        [HttpPost]
        [Route("delete")]
        public BaseResponse<string> Delete([FromBody] DeleteCommandRequest request)
        {
            var commandHandler = new DeleteCommandHandler(_clienteRepo);
            return commandHandler.Handler(request);
        }
    }
}