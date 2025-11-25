using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Front.Models
{
    public class ClienteVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo Nombre es obligatorio.")]
        [StringLength(50, ErrorMessage = "El Nombre no puede superar los 50 caracteres.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El campo Apellido es obligatorio.")]
        [StringLength(50, ErrorMessage = "El Apellido no puede superar los 50 caracteres.")]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "La Fecha de nacimiento es obligatoria.")]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de Nacimiento")]
        public DateTime FechaDeNacimiento { get; set; }

        [Required(ErrorMessage = "El campo CUIT es obligatorio.")]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "El CUIT debe tener 11 dígitos numéricos.")]
        public string Cuit { get; set; }

        [Required(ErrorMessage = "El campo Domicilio es obligatorio.")]
        [StringLength(100, ErrorMessage = "El Domicilio no puede superar los 100 caracteres.")]
        public string Domicilio { get; set; }

        [Required(ErrorMessage = "El campo Celular es obligatorio.")]
        [Phone(ErrorMessage = "El número de celular no tiene un formato válido.")]
        [StringLength(20, ErrorMessage = "El número de celular no puede superar los 20 caracteres.")]
        public string Celular { get; set; }

        [EmailAddress(ErrorMessage = "El Email no tiene un formato válido.")]
        [StringLength(100, ErrorMessage = "El Email no puede superar los 100 caracteres.")]
        public string? Email { get; set; }
    }
}
