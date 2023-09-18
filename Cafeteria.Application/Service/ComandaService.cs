using Cafeteria.Domain.Entidades;
using Cafeteria.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cafeteria.Application.Dtos;
using SendGrid.Helpers.Errors.Model;


namespace Cafeteria.Application.Service
{
    public class ComandaService : IComandaService
    {
        private readonly IComandaRepository _comandaRepository;
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ComandaService(IComandaRepository comandaRepository, IPedidoRepository pedidoRepository,
                           IUsuarioRepository usuarioRepository, IUnitOfWork unitOfWork)
        {
            _comandaRepository = comandaRepository;
            _pedidoRepository = pedidoRepository;
            _usuarioRepository = usuarioRepository;
            _unitOfWork = unitOfWork;
        }


        public async Task CambiarEstadoComandaAsync(int comandaId, string nuevoEstado)
        {
            var comanda = await _comandaRepository.ObtenerPorIdAsync(comandaId);

            if (comanda == null)
            {
                throw new NotFoundException("La comanda no fue encontrada.");
            }

            // Realiza validaciones adicionales según tus reglas de negocio, por ejemplo, si un usuario tiene permiso para cambiar el estado.

            // Aplica el cambio de estado.
            comanda.Estado = nuevoEstado;

            await _comandaRepository.ActualizarAsync(comanda);
            // Guarda los cambios en el repositorio.
            await _comandaRepository.ActualizarAsync(comanda);
        }

        public async Task<IEnumerable<ComandaDto>> ObtenerTodasLasComandasAsync()
        {
            var comandas = await _comandaRepository.ObtenerTodasAsync();
            // Mapear las comandas a ComandaDto según sea necesario
            var comandaDtos = comandas.Select(MapToComandaDto);
            return comandaDtos;
        }

        public async Task<ComandaDto> ObtenerComandaPorIdAsync(int comandaId)
        {
            var comanda = await _comandaRepository.ObtenerPorIdAsync(comandaId);
            // Mapear la comanda a ComandaDto según sea necesario
            var comandaDto = MapToComandaDto(comanda);
            return comandaDto;
        }

        public async Task<int> CrearComandaAsync(ComandaDto comandaDto)
        {
            // Validar que el pedido existe y está en el estado adecuado para crear una comanda
            var pedido = await _pedidoRepository.ObtenerPorIdAsync(comandaDto.Pedidos.Select(s => s.Id).FirstOrDefault());
            if (pedido == null || pedido.Estado != "pendiente")
            {
                throw new ApplicationException("El pedido no es válido para crear una comanda.");
            }

            // Crea la comanda
            var comanda = new Comanda
            {
                Pedidos = (new Pedido[] { new Pedido { Id = pedido.Id} }).ToList(),
                FechaCreacion = DateTime.Now
                // Otras propiedades de la comanda si es necesario
            };

            await _comandaRepository.AgregarAsync(comanda);
            await _unitOfWork.CommitAsync(); // Guardar cambios en la base de datos

            return comanda.Id; // Devuelve el ID de la
        }

        // Métodos de mapeo entre entidades y DTOs si es necesario
        private ComandaDto MapToComandaDto(Comanda comanda)
        {
            return new ComandaDto
            {
                Id = comanda.Id,
                //Pedidos = comanda.Pedidos.Select(s => s.Id).ToList(),
                FechaCreacion = comanda.FechaCreacion
                // Mapear otras propiedades si es necesario
            };
        }

        public async Task<bool> ActualizarComandaAsync(int id, ComandaDto comandaDto)
        {
            var comandaExistente = await _comandaRepository.ObtenerPorIdAsync(id);

            if (comandaExistente == null)
            {
                return false;
                //throw new NotFoundException($"Comanda con ID {id} no encontrada.");                
            }

            // comandaDto.ActualizarComanda(comandaExistente); // Método para actualizar desde DTO
            await _comandaRepository.ActualizarAsync(comandaExistente);
            await _unitOfWork.CommitAsync();

            return true;
        }

   



        public Task<ComandaDto> ObtenerComandaAsync(int comandaId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ComandaDto>> ObtenerComandasPorUsuarioAsync(int usuarioId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PedidoDto>> ObtenerPedidosPorComandaAsync(int comandaId)
        {
            throw new NotImplementedException();
        }

        public Task<ComandaDto> AgregarPedidoAsync(int comandaId, PedidoDto pedido, UsuarioDto usuario)
        {
            throw new NotImplementedException();
        }

        public Task<ComandaDto> CambiarEstadoComandaAsync(int comandaId, string nuevoEstado, UsuarioDto usuario)
        {
            throw new NotImplementedException();
        }

        public Task<ComandaDto> FacturarComandaAsync(int comandaId, UsuarioDto supervisor)
        {
            throw new NotImplementedException();
        }

        public async Task<List<PedidoDto>> ObtenerTodosAsync(UsuarioDto usuario)
        {
            // Verificar si el usuario tiene permiso para ver los pedidos
            if (usuario.Rol.Nombre == "Supervisor" || usuario.Rol.Nombre == "Administrador")
            {
                // Obtener todas las comandas asociadas al usuario, utilizando el repositorio de comandas
                var comandasDelUsuario = await _comandaRepository.ObtenerComandasPorUsuarioAsync(usuario.Id);

                // Crear una lista para almacenar todos los pedidos de las comandas
                var todosLosPedidos = new List<PedidoDto>();

                // Iterar a través de las comandas del usuario
                foreach (var comanda in comandasDelUsuario)
                {
                    // Obtener los pedidos de la comanda actual, utilizando el repositorio de pedidos
                    var pedidosDeComanda = await _pedidoRepository.ObtenerPedidosPorComandaAsync(comanda.Id);

                    pedidosDeComanda.ForEach(x => 
                    {
                        todosLosPedidos.Add(ConvertirAPedidoDto(x));
                    });
                }

                return todosLosPedidos;
            }
            else
            {
                // El usuario no tiene permiso para ver los pedidos
                throw new UnauthorizedAccessException("El usuario no tiene permiso para ver los pedidos.");
            }
        }


        public async Task<List<PedidoDto>> ObtenerTrabajosRealizadosAsync(UsuarioDto usuario)
        {
            // Verificamos si el usuario tiene el rol de "Supervisor" o "Administrador"
            if (usuario.Rol.Nombre != "Supervisor" && usuario.Rol.Nombre != "Administrador")
            {
                // Si no tiene los roles permitidos, lanzamos una excepción
                throw new UnauthorizedAccessException("No tienes permisos para ver los trabajos realizados por empleados.");
            }

            // Utilizamos el repositorio para obtener todas las comandas no completadas
            var comandasCompletadas = await _comandaRepository.ObtenerComandasNoCompletadasAsync();

            // Creamos una lista para almacenar los trabajos realizados por los empleados
            var trabajosRealizados = new List<PedidoDto>();

            // Iteramos sobre todas las comandas completadas
            foreach (var comanda in comandasCompletadas)
            {
                // Utilizamos el repositorio para obtener los pedidos de la comanda
                var pedidosEmpleados = await _pedidoRepository.ObtenerPedidosPorComandaAsync(comanda.Id);

                // Filtramos los pedidos que están en estado no "Completado" y pertenecen a un usuario con rol "Empleado"
                pedidosEmpleados = pedidosEmpleados
                    .Where(p => p.Usuario.Rol.Nombre == "Empleado" && p.Estado != "Completado")
                    .ToList();

                // Agregamos los pedidos de empleados a la lista de trabajos realizados
                //trabajosRealizados.AddRange(pedidosEmpleados);
                pedidosEmpleados.ForEach(x =>
                {
                    trabajosRealizados.Add(ConvertirAPedidoDto(x));
                });
            }

            return trabajosRealizados;
        }


        
        public async Task CambiarEstadoPedidoAsync(PedidoDto pedidodto, string nuevoEstado)
        {
            // Aquí debes utilizar tu repositorio para buscar el pedido por su Id
            var pedido = await _pedidoRepository.ObtenerPorIdAsync(pedidodto.Id);
            var estadosTerminados = new string[] { "Terminado", "Cancelado" };
            if (pedido != null)
            {
                // Cambiar el estado del pedido
                pedido.Estado = nuevoEstado;

                // Actualizar el pedido en el repositorio
                await _pedidoRepository.ActualizarAsync(pedido);

                 // Verificar si todos los pedidos de la comanda están en estado "Finalizado"
                var todosPedidosFinalizados = (await _comandaRepository.ObtenerTodasAsync())
                    .Where(p => p.Id == pedido.ComandaId)
                    .All(p => estadosTerminados.Contains(p.Estado));


                if (todosPedidosFinalizados)
                {
                    // Cambiar el estado de la comanda a "PendienteFacturacion"
                    var comanda = await _comandaRepository.ObtenerPorIdAsync(pedido.ComandaId);

                    if (comanda != null)
                    {
                        comanda.Estado = "PendienteFacturacion";
                        await _comandaRepository.ActualizarAsync(comanda);
                    }
                }
            }
        }

        public async Task<bool> EditarComandaAsync(UsuarioDto usuario, ComandaDto comanda)
        {
            // Primero, debes cargar la comanda desde el repositorio
            var comandaExistente = await _comandaRepository.ObtenerPorIdAsync(comanda.Id);

            if (comandaExistente != null)
            {
                // Verifica si el usuario tiene permiso para editar la comanda (según tu lógica de permisos)
                if (TienePermisoEditarComanda(usuario, comanda))
                {
                    // Actualiza las propiedades de la comanda existente con las del DTO
                    comandaExistente.Estado = comanda.Estado;

                    // Llama al método de actualización en el repositorio (puedes implementarlo en tu interfaz IComandaRepository)
                    await _comandaRepository.ActualizarAsync(comandaExistente);

                    return true; // Actualización exitosa
                }
                else
                {
                    // El usuario no tiene permiso para editar la comanda
                    throw new UnauthorizedAccessException("El usuario no tiene permiso para editar la comanda.");
                }
            }

            return false; // La comanda no fue encontrada
        }



        #region Medodos Privados
        private static PedidoDto ConvertirAPedidoDto(Pedido pedido)
        {
            if (pedido == null)
            {
                throw new ArgumentNullException(nameof(pedido));
            }

            var pedidoDto = new PedidoDto
            {
                Id = pedido.Id,
                FechaPedido = pedido.FechaPedido,
                Estado = pedido.Estado,
                UsuarioId = pedido.UsuarioId, // Copiar el ID del usuario relacionado
                ComandaId = pedido.ComandaId, // Copiar el ID de la comanda relacionada
            };

            return pedidoDto;
        }


        private bool TienePermisoEditarComanda(UsuarioDto usuarioDto, ComandaDto comanda)
        {
            // Verificamos el rol del usuario
            if (usuarioDto.Rol != null)
            {
                // Si el usuario tiene el rol "Administrador", tiene permiso para editar cualquier comanda
                if (usuarioDto.Rol.Nombre == "Administrador")
                {
                    return true;
                }

                // Si el usuario tiene el rol "Supervisor", puede editar comandas siempre y cuando sean de un usuario diferente
                if (usuarioDto.Rol.Nombre == "Supervisor" && comanda.Usuario.Id != usuarioDto.Id)
                {
                    return true;
                }
            }

            // Por defecto, el usuario no tiene permiso para editar la comanda
            return false;
        }
                

        #endregion
    }
}
