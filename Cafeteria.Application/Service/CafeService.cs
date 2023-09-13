using Cafeteria.Application.Dtos;
using Cafeteria.Domain;
using SendGrid.Helpers.Errors.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafeteria.Application.Service
{

    public class CafeService : ICafeService
    {
        private readonly ICafeRepository _cafeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CafeService(ICafeRepository cafeRepository, IUnitOfWork unitOfWork)
        {
            _cafeRepository = cafeRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<CafeDto>> ObtenerTodosLosCafesAsync()
        {
            var cafes = await _cafeRepository.ObtenerTodosAsync();
            // Mapea los objetos Cafe a CafeDto según sea necesario
            var cafeDtos = MapToCafeDtos(cafes);
            return cafeDtos;
        }

        public async Task<CafeDto> ObtenerCafePorIdAsync(int cafeId)
        {
            var cafe = await _cafeRepository.ObtenerPorIdAsync(cafeId);
            if (cafe == null)
            {
                return null; // Puedes retornar null o lanzar una excepción según tus necesidades
            }
            // Mapea el objeto Cafe a CafeDto según sea necesario
            var cafeDto = MapToCafeDto(cafe);
            return cafeDto;
        }

        public async Task<int> CrearCafeAsync(CafeDto cafeDto)
        {
            // Mapea CafeDto a una entidad Cafe si es necesario
            var cafe = MapToCafe(cafeDto);

            await _cafeRepository.AgregarAsync(cafe);
            await _unitOfWork.CommitAsync(); // Guardar cambios en la base de datos

            return cafe.Id; // Devuelve el ID del café recién creado
        }

        public async Task ActualizarCafeAsync(int cafeId, CafeDto cafeDto)
        {
            var cafeExistente = await _cafeRepository.ObtenerPorIdAsync(cafeId);
            if (cafeExistente == null)
            {
                throw new NotFoundException("El café no existe.");
            }

            // Actualiza las propiedades del café existente con los valores de CafeDto
            // Mapea las propiedades si es necesario

            await _cafeRepository.ActualizarAsync(cafeExistente);
            await _unitOfWork.CommitAsync(); // Guardar cambios en la base de datos
        }

        public async Task EliminarCafeAsync(int cafeId)
        {
            var cafeExistente = await _cafeRepository.ObtenerPorIdAsync(cafeId);
            if (cafeExistente == null)
            {
                throw new NotFoundException("El café no existe.");
            }

            _cafeRepository.Eliminar(cafeExistente);
            await _unitOfWork.CommitAsync(); // Guardar cambios en la base de datos
        }

        // Métodos de mapeo entre entidades y DTOs si es necesario
        private CafeDto MapToCafeDto(Cafe cafe)
        {
            return new CafeDto
            {
                Id = cafe.Id,
                Nombre = cafe.Nombre,
                Descripcion = cafe.Descripcion,
                Precio = cafe.Precio,
                TiempoPreparacionMinutos = cafe.TiempoPreparacionMinutos,
                MateriasPrimasIds = cafe.MateriasPrimasIds
                // Mapea otras propiedades si es necesario
            };
        }

        private Cafe MapToCafe(CafeDto cafeDto)
        {
            return new Cafe
            {
                Id = cafeDto.Id,
                Nombre = cafeDto.Nombre,
                Descripcion = cafeDto.Descripcion,
                Precio = cafeDto.Precio,
                TiempoPreparacionMinutos = cafeDto.TiempoPreparacionMinutos,
                MateriasPrimasIds = cafeDto.MateriasPrimasIds
                // Mapea otras propiedades si es necesario
            };
        }

        private List<CafeDto> MapToCafeDtos(IEnumerable<Cafe> cafes)
        {
            return cafes.Select(MapToCafeDto).ToList();
        }
    }

}
