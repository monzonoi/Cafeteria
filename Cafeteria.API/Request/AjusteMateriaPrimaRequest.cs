using Cafeteria.Application.Dtos;

namespace Cafeteria.API.Request
{
    public class AjusteMateriaPrimaRequest
    {
        public UsuarioDto Usuario { get; set; }
        public MateriaPrimaDto MateriaPrima { get; set; }
        public int Cantidad { get; set; }
    }

}
