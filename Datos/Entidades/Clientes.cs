using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Datos.Entidades
{
    [Table("Clientes")]
    public class Clientes
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [MaxLength(100)]
        public string Nombre { get; set; }
        [MaxLength(100)]
        public string Apellido { get; set; }
        public DateTime FechaDeNacimiento { get; set; }
        [MaxLength(11)]
        public string Cuit { get; set; }
        [MaxLength(100)]
        public string Domicilio { get; set; }
        [MaxLength(15)]
        public string Celular { get; set; }
        [MaxLength(100)]
        public string Email { get; set; }
    }

}
