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
    public class FacturacionService : IFacturacionService
    {
        private readonly IFacturaRepository _facturaRepository;
        private readonly IUnitOfWork _unitOfWork;

        public FacturacionService(IFacturaRepository facturaRepository, IUnitOfWork unitOfWork)
        {
            _facturaRepository = facturaRepository ?? throw new ArgumentNullException(nameof(facturaRepository));
            _unitOfWork = unitOfWork;
        }

        

        public async Task<IEnumerable<FacturaDto>> ObtenerTodasLasFacturasAsync()
        {
            var facturas = await _facturaRepository.ObtenerTodasAsync();
            return facturas.Select(f => new FacturaDto
            {
                Id = f.Id,
                // Mapear otras propiedades según sea necesario
            });
        }

        public async Task<FacturaDto> ObtenerFacturaPorIdAsync(int id)
        {
            var factura = await _facturaRepository.ObtenerPorIdAsync(id);
            if (factura == null)
            {
                throw new NotFoundException($"Factura con ID {id} no encontrada.");
            }

            return new FacturaDto
            {
                Id = factura.Id,
                // Mapear otras propiedades según sea necesario
            };
        }

        public async Task<int> CrearFacturaAsync(FacturaDto facturaDto)
        {
            var nuevaFactura = new Factura
            {
                // Mapear propiedades desde el DTO
            };

            await _facturaRepository.AgregarAsync(nuevaFactura);            
            await _unitOfWork.CommitAsync(); // Guardar cambios en la base de datos

            return nuevaFactura.Id;
        }

        public async Task ActualizarFacturaAsync(int id, FacturaDto facturaDto)
        {
            var facturaExistente = await _facturaRepository.ObtenerPorIdAsync(id);

            if (facturaExistente == null)
            {
                throw new NotFoundException($"Factura con ID {id} no encontrada.");
            }

            // Actualizar propiedades desde el DTO
            // facturaExistente.Propiedad = facturaDto.Propiedad;

            _facturaRepository.Actualizar(facturaExistente);
            await _unitOfWork.CommitAsync(); // Guardar cambios en la base de datos
        }

        public async Task EliminarFacturaAsync(int id)
        {
            var facturaExistente = await _facturaRepository.ObtenerPorIdAsync(id);

            if (facturaExistente == null)
            {
                throw new NotFoundException($"Factura con ID {id} no encontrada.");
            }

            _facturaRepository.Eliminar(facturaExistente);
            await _unitOfWork.CommitAsync(); // Guardar cambios en la base de datos
        }        
      
    }
}
