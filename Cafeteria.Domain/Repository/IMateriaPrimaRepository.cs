﻿using Cafeteria.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafeteria.Domain
{
    public interface IMateriaPrimaRepository
    {
        Task<IEnumerable<MateriaPrima>> ObtenerTodasAsync();
        Task<MateriaPrima> ObtenerPorIdAsync(int materiaPrimaId);
        Task AgregarAsync(MateriaPrima materiaPrima);
        Task ActualizarAsync(MateriaPrima materiaPrima);
        Task EliminarAsync(int materiaPrimaId);        
        Task<MateriaPrima> ObtenerPorNombreAsync(string nombre);
        Task ReducirStockAsync(int materiaPrimaId, int cantidad);
    }

}
