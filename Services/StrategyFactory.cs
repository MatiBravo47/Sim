using SimulacroSolucionm.Strategies;
using System;

namespace SimulacroSolucionm.Services
{
    public class StrategyFactory
    {
        private readonly EnvioMoto _moto;
        private readonly EnvioCorreo _correo;
        private readonly EnvioRetiro _retiro;

        public StrategyFactory(EnvioMoto moto, EnvioCorreo correo, EnvioRetiro retiro)
        {
            _moto = moto; _correo = correo; _retiro = retiro;
        }

        public IEnvioStrategy FromAlias(string alias)
        {
            return alias switch
            {
                "moto" => _moto,
                "correo" => _correo,
                "retiro" => _retiro,
                _ => throw new ArgumentException("Tipo de envío no reconocido. Usá: moto | correo | retiro")
            };
        }
    }
}

