using System.Linq;
using System;
using System.Text;
using SimulacroSolucionm.Builders;
using SimulacroSolucionm.Models;
using SimulacroSolucionm.Repositories;
using SimulacroSolucionm.Services;

namespace SimulacroSolucionm.Controllers
{
    public class CheckoutFacade
    {
        private readonly IPedidoBuilder _builder;
        private readonly StrategyFactory _factory;
        private readonly PedidoService _service;
        private readonly IRepositorio<Pedido> _repo;

        public CheckoutFacade(IPedidoBuilder builder, StrategyFactory factory, PedidoService service, IRepositorio<Pedido> repo)
        {
            _builder = builder;
            _factory = factory;
            _service = service;
            _repo = repo;
            _builder.Reset();
        }

        public void Reset() => _builder.Reset();

        public void SetDatosCliente(string nombre, string direccion)
        {
            if (string.IsNullOrWhiteSpace(nombre)) throw new ArgumentException("El nombre es obligatorio");
            if (string.IsNullOrWhiteSpace(direccion)) throw new ArgumentException("La dirección es obligatoria");
            _builder.SetCliente(nombre, direccion);
        }

        public void AgregarProducto(string nombre, decimal precio, int cantidad)
        {
            if (string.IsNullOrWhiteSpace(nombre)) throw new ArgumentException("Nombre de producto obligatorio");
            if (precio < 0) throw new ArgumentException("Precio inválido");
            if (cantidad <= 0) throw new ArgumentException("Cantidad inválida");
            _builder.AddProducto(nombre, precio, cantidad);
        }

        public void SeleccionarEnvio(string alias)
        {
            var strat = _factory.FromAlias(alias);
            _builder.SetEnvio(strat);
        }

        public string ObtenerResumen()
        {
            // Intentar calcular totales si ya hay envío
            decimal subtotal = 0;
            try
            {
                subtotal = (_builder as PedidoBuilder)!
                    .GetType()
                    .GetField("_pedido", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)!
                    .GetValue(_builder) is Pedido p
                    ? p.Items.Sum(i => i.Total)
                    : 0;
            }
            catch { }

            var sb = new StringBuilder();
            sb.AppendLine("--- Resumen actual ---");
            sb.AppendLine($"Subtotal: ${subtotal}");
            sb.AppendLine("(El total final se calcula al confirmar, según estrategia de envío)");
            return sb.ToString();
        }

        public void ConfirmarPedido()
        {
            var pedido = _builder.Build();
            _service.Confirmar(pedido);
            _repo.Guardar(pedido);
        }
    }
}

