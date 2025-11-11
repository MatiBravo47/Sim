using SimulacroSolucionm.Models;

namespace SimulacroSolucionm.Observers
{
    public interface IPedidoObserver
    {
        void OnPedidoConfirmado(Pedido pedido);
    }
}

