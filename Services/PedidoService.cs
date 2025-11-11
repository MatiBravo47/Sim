using SimulacroSolucionm.Models;
using SimulacroSolucionm.Observers;
using System;
using System.Collections.Generic;

namespace SimulacroSolucionm.Services
{
    public class PedidoService
    {
        private readonly List<IPedidoObserver> _observers = new();

        public void Suscribir(IPedidoObserver observer) => _observers.Add(observer);
        public void Desuscribir(IPedidoObserver observer) => _observers.Remove(observer);

        public void Confirmar(Pedido pedido)
        {
            // En un caso real: pagos, validaciones extra, etc. Aca solo notifico
            Notificar(pedido);
        }

        private void Notificar(Pedido pedido)
        {
            foreach (var obs in _observers)
            {
                try { obs.OnPedidoConfirmado(pedido); }
                catch 
                {
                    Console.WriteLine("No se pudo notificar!");
                }
            }
        }
    }
}

