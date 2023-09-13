using System.ComponentModel.DataAnnotations;

namespace Cafeteria.Application.Dtos
{
    public class MateriaPrimaDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre de la materia prima es requerido.")]
        [MaxLength(50, ErrorMessage = "El nombre de la materia prima no puede tener más de 50 caracteres.")]
        public string Nombre { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "La cantidad debe ser un valor positivo.")]
        public decimal Cantidad { get; set; }
        // Otras propiedades relacionadas con la materia prima si es necesario
    }
}
