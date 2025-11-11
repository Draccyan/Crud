using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Front.Models
{
    public class ClienteVM
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }
        public required string Apellido { get; set; }
        public DateTime FechaDeNacimiento { get; set; }
        public required string Cuit { get; set; }
        public required string Domicilio { get; set; }
        public string? Celular { get; set; }
        public string? Email { get; set; }
    }
}
