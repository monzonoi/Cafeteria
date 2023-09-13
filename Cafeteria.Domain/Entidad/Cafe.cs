﻿using Cafeteria.Domain.Entidad;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafeteria.Domain
{
    public class Cafe
    {
        [Key]
        public int Id { get; set; }

        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public int TiempoPreparacionMinutos { get; set; }

        // Propiedad de navegación para representar la relación con MateriasPrimas
        public List<MateriaPrima> MateriasPrimas { get; set; }

        [NotMapped]
        public List<int> MateriasPrimasIds { get; set; }
    }
}
