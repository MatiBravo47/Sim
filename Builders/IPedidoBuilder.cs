using SimulacroSolucionm.Models;
using SimulacroSolucionm.Strategies;

namespace SimulacroSolucionm.Builders
{
    public interface IPedidoBuilder
    {
        void Reset();
        void SetCliente(string nombre, string direccion);
        void AddProducto(string nombre, decimal precio, int cantidad);
        void SetEnvio(IEnvioStrategy strategy);
        Pedido Build();
    }
}

