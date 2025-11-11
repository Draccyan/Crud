namespace Crud.Services.Models
{
    public class ClienteModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime? FechaDeNacimiento { get; set; }
        public string Cuit { get; set; }
        public string Domicilio { get; set; }
        public string Celular { get; set; }
        public string Email { get; set; }
    }

}