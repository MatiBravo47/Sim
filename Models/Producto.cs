namespace SimulacroSolucionm.Models
{
    public class Producto
    {
        public string Nombre { get; set; } = string.Empty;
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }

        public decimal Total => Precio * Cantidad;
    }
}

