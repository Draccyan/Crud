using Crud.Services.Models;

namespace Crud.Services.Commands.Search
{
    public class SearchCommandResponse
    {
        public List<ClienteModel> Clientes { get; set; }
    }
}