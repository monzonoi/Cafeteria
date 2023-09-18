using Cafeteria.Domain.Entidades;
using Cafeteria.Domain;
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


        public async Task<bool> ValidarStockDisponibleAsync(ItemPedidoDto itemPedido)
        {
            // Obtener la materia prima desde el repositorio
            var materiaPrima = await _materiaPrimaRepository.ObtenerPorNombreAsync(itemPedido.NombreMateriaPrima);

            if (materiaPrima == null || materiaPrima.CantidadDisponible < itemPedido.CantidadRequerida)
            {
                return false; // No hay suficiente materia prima disponible
            }

            return true;
        }



        public async Task<bool> ReducirStockAsync(MateriaPrimaDto materiaprima, int cantidad)
        {
            var materiaPrima = await _materiaPrimaRepository.ObtenerPorIdAsync(materiaprima.Id);

            if (materiaPrima != null)
            {
                if (materiaPrima.CantidadDisponible >= cantidad)
                {
                    await _materiaPrimaRepository.ReducirStockAsync(materiaPrima.Id, cantidad);
                    return true; // Stock reducido con éxito
                }
                else
                {
                    return false; // Stock insuficiente
                }
            }
            else
            {
                throw new KeyNotFoundException($"No se encontró la materia prima '{materiaprima.Nombre}' en el repositorio.");
            }
        }

        public async Task<bool> AjustarMateriaPrimaAsync(UsuarioDto usuario, MateriaPrimaDto materiaPrima, int cantidad)
        {
            if (usuario == null || materiaPrima == null)
            {
                throw new ArgumentNullException("Usuario y materia prima no pueden ser nulos");
            }

            // Verifica si el usuario tiene permiso para ajustar la materia prima (rol Supervisor o Administrador)
            if (!EsSupervisorOAdministrador(usuario))
            {
                throw new UnauthorizedAccessException("El usuario no tiene permiso para ajustar la materia prima");
            }

            // Busca la materia prima en el repositorio por su ID
            var materiaPrimaExistente = await _materiaPrimaRepository.ObtenerPorIdAsync(materiaPrima.Id);

            if (materiaPrimaExistente == null)
            {
                throw new InvalidOperationException("La materia prima no existe");
            }

            // Realiza el ajuste de stock según la cantidad especificada
            materiaPrimaExistente.CantidadDisponible += cantidad;

            // Actualiza la materia prima en el repositorio
            await _materiaPrimaRepository.ActualizarAsync(materiaPrimaExistente);

            return true;
        }


        #region Metodos Privados
        private bool EsSupervisorOAdministrador(UsuarioDto usuario)
        {
            // Verificar si el usuario tiene el rol de Supervisor o Administrador
            return usuario.Rol.Nombre == "Supervisor" || usuario.Rol.Nombre == "Administrador";
        }

        #endregion

    }

}
