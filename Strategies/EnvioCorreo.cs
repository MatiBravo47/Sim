using SimulacroSolucionm.Models;
using System.Linq;
using System;

namespace SimulacroSolucionm.Strategies
{
    public class EnvioCorreo : IEnvioStrategy
    {
        public string Nombre => "Correo";

        public decimal CalcularCosto(Pedido pedido)
        {
            // Correo: costo en función del subtotal (porcentaje mínimo)
            var costo = Math.Max(2500m, pedido.Items.Sum(i => i.Total) * 0.06m);
            return decimal.Round(costo, 2);
        }
    }
}

