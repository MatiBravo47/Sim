using SimulacroSolucionm.Models;
using System;

namespace SimulacroSolucionm.Observers
{
    public class LogisticaObserver : IPedidoObserver
    {
        public void OnPedidoConfirmado(Pedido pedido)
        {
            Console.WriteLine("[Logística] Nuevo pedido confirmado. Preparar packing list:");
            foreach (var it in pedido.Items)
                Console.WriteLine($" - {it.Cantidad} x {it.Nombre} (${it.Precio} c/u)");
            Console.WriteLine($" Envío: {pedido.EnvioNombre} | Total: ${pedido.Total}");
        }
    }
}

