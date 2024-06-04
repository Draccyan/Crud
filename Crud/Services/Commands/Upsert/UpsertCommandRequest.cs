using Crud.Services.Models;

namespace Crud.Services.Commands.Upsert
{
    public class UpsertCommandRequest
    {
        public int Id { get; set; }
        public ClienteModel? Data { get; set; }
    }
}