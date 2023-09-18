using Cafeteria.Domain;
using Cafeteria.Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafeteria.Intraestructura.Repository
{
    public class MateriaPrimaRepository : IMateriaPrimaRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public MateriaPrimaRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<MateriaPrima>> ObtenerTodasAsync()
        {
            return await _dbContext.MateriasPrimas.ToListAsync();
        }

        public async Task<MateriaPrima> ObtenerPorIdAsync(int materiaPrimaId)
        {
            return await _dbContext.MateriasPrimas.FirstOrDefaultAsync(mp => mp.Id == materiaPrimaId);
        }

        public async Task AgregarAsync(MateriaPrima materiaPrima)
        {
            _dbContext.MateriasPrimas.Add(materiaPrima);
            await _dbContext.SaveChangesAsync();
        }

        public async Task ActualizarAsync(MateriaPrima materiaPrima)
        {
            _dbContext.Entry(materiaPrima).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task EliminarAsync(int materiaPrimaId)
        {
            var materiaPrima = await _dbContext.MateriasPrimas.FindAsync(materiaPrimaId);
            if (materiaPrima != null)
            {
                _dbContext.MateriasPrimas.Remove(materiaPrima);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<MateriaPrima> ObtenerPorNombreAsync(string nombre)
        {
            return await _dbContext.MateriasPrimas
                .FirstOrDefaultAsync(mp => mp.Nombre == nombre);
        }


        public async Task ReducirStockAsync(int materiaPrimaId, int cantidad)
        {
            var materiaPrima = await _dbContext.MateriasPrimas.FindAsync(materiaPrimaId);

            if (materiaPrima != null)
            {
                if (materiaPrima.CantidadDisponible >= cantidad)
                {
                    materiaPrima.CantidadDisponible -= cantidad;
                    await _dbContext.SaveChangesAsync(); // Guardar los cambios en la base de datos
                }
                else
                {
                    throw new InvalidOperationException($"Stock insuficiente de '{materiaPrima.Nombre}' para reducir en {cantidad} unidades.");
                }
            }
            else
            {
                throw new KeyNotFoundException($"No se encontró la materia prima con ID '{materiaPrimaId}'.");
            }
        }


    }
}
