using SimulacroSolucionm.Models;

namespace SimulacroSolucionm.Strategies
{
    public class EnvioRetiro : IEnvioStrategy
    {
        public string Nombre => "Retiro en local";

        public decimal CalcularCosto(Pedido pedido)
        {
            return 0m; // Sin costo
        }
    }
}

