using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafeteria.Domain.Entidad
{
    public class MateriaPrima
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }

        // Otras propiedades relacionadas con la materia prima

        // Propiedad de navegación inversa para representar la relación con Café
        public List<Cafe> Cafes { get; set; }
        public decimal Cantidad { get; set; }
    }
}
