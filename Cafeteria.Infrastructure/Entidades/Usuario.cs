using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafeteria.Infrastructure.Entidades
{
    public class Usuario
    {
        public int UsuarioID { get; set; }
        public string Nombre { get; set; }
        public int RolID { get; set; }
        public Rol Rol { get; set; }
        // Otros campos de usuario aquí
       // public List<Pedido> Pedidos { get; set; }
    }
}
