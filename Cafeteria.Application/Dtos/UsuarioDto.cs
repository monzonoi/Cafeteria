using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafeteria.Application.Dtos
{
    public class UsuarioDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public List<string> Roles { get; set; } // Una lista de roles como strings

        public UsuarioDto()
        {
            Roles = new List<string>();
        }

        public UsuarioDto(int id, string nombre)
        {
            Id = id;
            Nombre = nombre;
            Roles = new List<string>();
        }
    }
}
