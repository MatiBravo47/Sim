using SimulacroSolucionm.Models;
using System;

namespace SimulacroSolucionm.Strategies
{
    public class EnvioMoto : IEnvioStrategy
    {
        public string Nombre => "Moto";

        public decimal CalcularCosto(Pedido pedido)
        {
            // Tarifa base + variable por ítems (ejemplo simple)
            var baseCost = 1500m;
            var variable = 200m * Math.Max(1, pedido.Items.Count);
            return baseCost + variable;
        }
    }
}

