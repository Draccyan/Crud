using Crud.Services.Commands.Delete;
using Crud.Services.Commands.Get;
using Crud.Services.Commands.GetAll;
using Crud.Services.Commands.Search;
using Crud.Services.Commands.Upsert;
using Datos;
using Microsoft.AspNetCore.Mvc;

namespace Crud.Controllers
{
    [ApiController]
    [Route("Crud")]
    public class ClientesController : ControllerBase
    {
        private readonly EfContext _ctx;

        public ClientesController(EfContext ctx)
        {
            _ctx = ctx;
        }

        [HttpGet]
        [Route("getAll")]
        public GetAllCommandResponse GetAll([FromQuery] GetAllCommandRequest command)
        {
            var commandHandler = new GetAllCommandHandler(_ctx);
            var response = commandHandler.Handler(command);
            //
            return response;
        }

        [HttpGet]
        [Route("get")]
        public GetCommandResponse Get([FromQuery] GetCommandRequest command)
        {
            var commandHandler = new GetCommandHandler(_ctx);
            var response = commandHandler.Handler(command);
            //
            return response;
        }

        [HttpGet]
        [Route("search")]
        public SearchCommandResponse Search([FromQuery] SearchCommandRequest command)
        {
            var commandHandler = new SearchCommandHandler(_ctx);
            var response = commandHandler.Handler(command);
            //
            return response;
        }

        [HttpPost]
        [Route("upsert")]
        public UpsertCommandResponse Upsert([FromBody] UpsertCommandRequest command)
        {
            var commandHandler = new UpsertCommandHandler(_ctx);
            var response = commandHandler.Handler(command);
            //
            return response;
        }

        [HttpPost]
        [Route("delete")]
        public DeleteCommandResponse Delete([FromBody] DeleteCommandRequest command)
        {
            var commandHandler = new DeleteCommandHandler(_ctx);
            var response = commandHandler.Handler(command);
            //
            return response;
        }
    }
}