using System.Collections.Generic;
using System;
using System.Text.Json.Serialization;

namespace SimulacroSolucionm.Models
{
    public class Pedido
    {
        public string ClienteNombre { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public List<Producto> Items { get; set; } = new();
        public string EnvioNombre { get; set; } = string.Empty;
        public decimal Subtotal { get; set; }
        public decimal CostoEnvio { get; set; }
        public decimal Total { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;
    }
}

