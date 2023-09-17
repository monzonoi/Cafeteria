namespace Cafeteria.Application.Dtos
{
    public class ComandaDto
    {
        public int Id { get; set; }
        public List<PedidoDto> Pedidos { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string Estado { get; set; }
        public UsuarioDto Usuario { get; set; }

        // Otras propiedades si es necesario
    }

}
