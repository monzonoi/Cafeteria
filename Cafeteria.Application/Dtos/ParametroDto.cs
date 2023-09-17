using Cafeteria.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafeteria.Application.Dtos
{
    public class ParametroDto
    {
        public int Id { get; set; } // Identificador del parámetro
        public string Nombre { get; set; } // Nombre del parámetro
        public string Valor { get; set; } // Valor del parámetro
        public string Descripcion { get; set; } // Descripción del parámetro

        // Otras propiedades según las necesidades de tu aplicación

        public TipoParametro Tipo { get; set; } // Tipo del parámetro (puedes agregar este campo si lo necesitas)
    }
}
