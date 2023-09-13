namespace Cafeteria.Application.Dtos
{
    public class ComandaDto
    {
        public int Id { get; set; }
        public List<int> Pedidos { get; set; }
        public DateTime FechaCreacion { get; set; }

        // Otras propiedades si es necesario
    }

}
