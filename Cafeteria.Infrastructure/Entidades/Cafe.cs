using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafeteria.Infrastructure.Entidades
{
    public class Cafe
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        public decimal Precio { get; set; }

        //public List<ItemPedido> ItemsPedido { get; set; }

        // Agrega otras propiedades según tus necesidades
    }
}
