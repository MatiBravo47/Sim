using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace SimulacroSolucionm.Repositories
{
    public class RepositorioJson<T> : IRepositorio<T>
    {
        private readonly string _ruta;
        private static readonly JsonSerializerOptions _opts = new()
        {
            WriteIndented = true
        };

        public RepositorioJson(string ruta)
        {
            _ruta = ruta;
        }

        public void Guardar(T entidad)
        {
            List<T> data = new();
            if (File.Exists(_ruta))
            {
                try
                {
                    var json = File.ReadAllText(_ruta);
                    var list = JsonSerializer.Deserialize<List<T>>(json, _opts);
                    if (list != null) data = list;
                }
                catch 
                {
                    Console.WriteLine("Error al abrir el archivo!");
                }
            }
            data.Add(entidad);
            File.WriteAllText(_ruta, JsonSerializer.Serialize(data, _opts));
        }

        public IEnumerable<T> ObtenerTodos()
        {
            if (!File.Exists(_ruta)) return Enumerable.Empty<T>();
            var json = File.ReadAllText(_ruta);
            return JsonSerializer.Deserialize<List<T>>(json, _opts) ?? Enumerable.Empty<T>();
        }
    }
}

