using Cafeteria.Domain.Entidades;
using Cafeteria.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cafeteria.Application.Dtos;

namespace Cafeteria.Application.Service
{
    public class MateriaPrimaService : IMateriaPrimaService
    {
        private readonly IMateriaPrimaRepository _materiaPrimaRepository;
        private readonly IUnitOfWork _unitOfWork;

        public MateriaPrimaService(IMateriaPrimaRepository materiaPrimaRepository, IUnitOfWork unitOfWork)
        {
            _materiaPrimaRepository = materiaPrimaRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<MateriaPrimaDto>> ObtenerTodasLasMateriasPrimasAsync()
        {
            var materiasPrimas = await _materiaPrimaRepository.ObtenerTodasAsync();
            // Mapear las materias primas a MateriaPrimaDto según sea necesario
            //var materiaPrimaDtos = materiasPrimas.Select(MapToMateriaPrimaDto);

            var materiaPrimaDtos = from c in materiasPrimas
                               select MapToMateriaPrimaDto(c);



            return materiaPrimaDtos;
        }

        public async Task<MateriaPrimaDto> ObtenerMateriaPrimaPorIdAsync(int materiaPrimaId)
        {
            var materiaPrima = await _materiaPrimaRepository.ObtenerPorIdAsync(materiaPrimaId);
            // Mapear la materia prima a MateriaPrimaDto según sea necesario
            var materiaPrimaDto = MapToMateriaPrimaDto(materiaPrima);
            return materiaPrimaDto;
        }

        public async Task<int> CrearMateriaPrimaAsync(MateriaPrimaDto materiaPrimadto)
        {
            // Mapear MateriaPrimaDto a una entidad MateriaPrima si es necesario
            var materiaPrima = MapToMateriaPrima(materiaPrimadto);

            await _materiaPrimaRepository.AgregarAsync(materiaPrima);
            await _unitOfWork.CommitAsync(); // Guardar cambios en la base de datos

            return materiaPrima.Id; // Devuelve el ID de la materia prima recién creada
        }

        public async Task ActualizarMateriaPrimaAsync(int materiaPrimaId, MateriaPrimaDto materiaPrima)
        {
            var materiaPrimaExistente = await _materiaPrimaRepository.ObtenerPorIdAsync(materiaPrimaId);
            if (materiaPrimaExistente == null)
            {
                throw new ApplicationException("La materia prima no existe.");
            }

            // Actualiza las propiedades de la materia prima existente con los valores de MateriaPrimaDto
            // Mapea las propiedades si es necesario

            await _materiaPrimaRepository.ActualizarAsync(materiaPrimaExistente);
            await _unitOfWork.CommitAsync(); // Guardar cambios en la base de datos
        }

        public async Task EliminarMateriaPrimaAsync(int materiaPrimaId)
        {
            await _materiaPrimaRepository.EliminarAsync(materiaPrimaId);
            await _unitOfWork.CommitAsync(); // Guardar cambios en la base de datos
        }

        // Métodos de mapeo entre entidades y DTOs si es necesario
        private MateriaPrimaDto MapToMateriaPrimaDto(MateriaPrima materiaPrima)
        {
            return new MateriaPrimaDto
            {
                Id = materiaPrima.Id,
                Nombre = materiaPrima.Nombre,
                // Mapear otras propiedades si es necesario
            };
        }

        private MateriaPrima MapToMateriaPrima(MateriaPrimaDto materiaPrimaDto)
        {
            return new MateriaPrima
            {
                Id = materiaPrimaDto.Id,
                Nombre = materiaPrimaDto.Nombre,
                // Mapear otras propiedades si es necesario
            };
        }

        public Task<int> ObtenerStockAsync(string nombre)
        {
            throw new NotImplementedException();
        }

        public Task ReducirStockAsync(string nombre, int cantidad)
        {
            throw new NotImplementedException();
        }
    }

}
