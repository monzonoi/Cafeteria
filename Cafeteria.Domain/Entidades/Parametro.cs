using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafeteria.Domain.Entidades
{
    public enum TipoParametro
    {
        Texto,
        Numero,
        Fecha,
        // Agrega más tipos según tus necesidades
    }

    public class Parametro
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Valor { get; set; }
        public string Descripcion { get; set; }

        // Otras propiedades según las necesidades de tu empresa

        // Por ejemplo, puedes agregar un campo para identificar el tipo de parámetro:
        public TipoParametro Tipo { get; set; }
    }
}
