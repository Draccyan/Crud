using Crud.Services.Models;

namespace Crud.Services.Commands.GetAll
{
    public class GetAllCommandResponse
    {
        public List<ClienteModel> Clientes { get; set; }
    }
}
