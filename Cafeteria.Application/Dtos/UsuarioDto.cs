using Cafeteria.Domain.Entidades;
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
        public int RolId { get; set; }
        public RolDto Rol { get; set; }
        public List<PedidoDto> Pedidos { get; set; }

        public UsuarioDto()
        {
            Pedidos = new List<PedidoDto>();
        }
        
    }
}
