using Cafeteria.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafeteria.Application.Dtos
{
    public class FacturaDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "La fecha de emisión es obligatoria.")]
        public DateTime FechaFacturacion { get; set; }

        [Required(ErrorMessage = "El total es obligatorio.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El total debe ser mayor que cero.")]
        public decimal Total { get; set; }

        // Otras propiedades según sea necesario

        public FacturaDto()
        {
            // Inicialización de propiedades si es necesario
        }

        public FacturaDto(Factura factura)
        {
            Id = factura.Id;
            FechaFacturacion = factura.FechaFacturacion;
            Total = factura.Total;
            // Mapear otras propiedades desde la entidad Factura según sea necesario
        }

        public Factura ToFactura()
        {
            return new Factura
            {
                Id = Id,
                FechaFacturacion = FechaFacturacion,
                Total = Total,
                // Mapear otras propiedades desde el DTO a la entidad Factura según sea necesario
            };
        }
    }
}
