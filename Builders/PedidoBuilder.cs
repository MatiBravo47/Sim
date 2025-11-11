using SimulacroSolucionm.Models;
using SimulacroSolucionm.Strategies;
using System.Linq;
using System;

namespace SimulacroSolucionm.Builders
{
    public class PedidoBuilder : IPedidoBuilder
    {
        private Pedido _pedido = new();
        private IEnvioStrategy? _envio;

        public void Reset()
        {
            _pedido = new Pedido();
            _envio = null;
        }

        public void SetCliente(string nombre, string direccion)
        {
            _pedido.ClienteNombre = nombre.Trim();
            _pedido.Direccion = direccion.Trim();
        }

        public void AddProducto(string nombre, decimal precio, int cantidad)
        {
            _pedido.Items.Add(new Producto { Nombre = nombre.Trim(), Precio = precio, Cantidad = cantidad });
        }

        public void SetEnvio(IEnvioStrategy strategy)
        {
            _envio = strategy;
            _pedido.EnvioNombre = strategy.Nombre;
        }

        public Pedido Build()
        {
            if (_pedido.Items.Count == 0) throw new InvalidOperationException("El pedido no tiene productos.");
            if (string.IsNullOrWhiteSpace(_pedido.ClienteNombre)) throw new InvalidOperationException("Falta el nombre del cliente.");
            if (string.IsNullOrWhiteSpace(_pedido.Direccion)) throw new InvalidOperationException("Falta la dirección.");
            if (_envio is null) throw new InvalidOperationException("No se seleccionó un tipo de envío.");

            _pedido.Subtotal = _pedido.Items.Sum(p => p.Total);
            _pedido.CostoEnvio = _envio.CalcularCosto(_pedido);
            _pedido.Total = _pedido.Subtotal + _pedido.CostoEnvio;

            return _pedido;
        }
    }
}

