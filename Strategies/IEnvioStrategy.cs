using SimulacroSolucionm.Models;

namespace SimulacroSolucionm.Strategies
{
    public interface IEnvioStrategy
    {
        string Nombre { get; }
        decimal CalcularCosto(Pedido pedido);
    }
}

