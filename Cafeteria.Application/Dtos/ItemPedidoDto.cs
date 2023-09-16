namespace Cafeteria.Application.Dtos
{
    public class ItemPedidoDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int CantidadRequerida { get; set; }
        public int TiempoRealizacion { get; set; }
        public decimal Precio { get; set; }
        public string NombreMateriaPrima { get; set; } // Nombre de la materia prima necesaria para este ítem
        public int MateriaPrimaId { get; set; } // Identificador de la materia prima necesaria para este ítem
        public MateriaPrimaDto MateriaPrima { get; set; } // Objeto MateriaPrimaDto relacionado
        public int Cantidad { get; set; }
    }
}
