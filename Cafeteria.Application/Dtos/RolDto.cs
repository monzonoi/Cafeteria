using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafeteria.Application.Dtos
{
    public class RolDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        // Otras propiedades del DTO, si las hubiera

        public List<UsuarioDto> Usuarios { get; set; }
    }

}
