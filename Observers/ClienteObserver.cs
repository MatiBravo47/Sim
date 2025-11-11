using SimulacroSolucionm.Models;
using System;

namespace SimulacroSolucionm.Observers
{
    public class ClienteObserver : IPedidoObserver
    {
        public void OnPedidoConfirmado(Pedido pedido)
        {
            Console.WriteLine($"[Notificación al Cliente] Gracias {pedido.ClienteNombre}! Tu pedido por ${pedido.Total} fue confirmado. Tipo de envío: {pedido.EnvioNombre}.");
        }
    }
}

