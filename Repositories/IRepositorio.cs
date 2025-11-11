using System.Collections.Generic;

namespace SimulacroSolucionm.Repositories
{
    public interface IRepositorio<T>
    {
        void Guardar(T entidad);
        IEnumerable<T> ObtenerTodos();
    }
}

