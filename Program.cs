
using SimulacroSolucionm.Builders;
using SimulacroSolucionm.Controllers;
using SimulacroSolucionm.Models;
using SimulacroSolucionm.Observers;
using SimulacroSolucionm.Repositories;
using SimulacroSolucionm.Services;
using SimulacroSolucionm.Strategies;
using System;

namespace ParcialPedidos
{
    class Program
    {
        static void Main(string[] args)
        {
            
            var repo = new RepositorioJson<Pedido>("pedidos.json");
            var builder = new PedidoBuilder();

            // Estrategias y fábric
            var envioMoto = new EnvioMoto();
            var envioCorreo = new EnvioCorreo();
            var envioRetiro = new EnvioRetiro();
            var factory = new StrategyFactory(envioMoto, envioCorreo, envioRetiro);

            // Servicio y observers
            var pedidoService = new PedidoService();
            var clienteObs = new ClienteObserver();
            var logisticaObs = new LogisticaObserver();
            pedidoService.Suscribir(clienteObs);
            pedidoService.Suscribir(logisticaObs);

            // Facade/Controller
            var facade = new CheckoutFacade(builder, factory, pedidoService, repo);

            MostrarMenu(facade);
        }

        static void MostrarMenu(CheckoutFacade facade)
        {
            var op = "9";
            while (op != "0")
            {
                Console.Clear();
                Console.WriteLine("=== Checkout – ParcialPedidos ===");
                Console.WriteLine("1) Cargar datos del cliente");
                Console.WriteLine("2) Agregar producto");
                Console.WriteLine("3) Seleccionar tipo de envío (moto/correo/retiro)");
                Console.WriteLine("4) Ver resumen actual");
                Console.WriteLine("5) Confirmar pedido");
                Console.WriteLine("0) Salir");
                Console.Write("Opción: ");
                op = Console.ReadLine()?.Trim();

                try
                {
                    switch (op)
                    {
                        case "1": 
                            CargarCliente(facade); 
                            break;
                        case "2": 
                            AgregarProducto(facade); 
                            break;
                        case "3": 
                            SeleccionarEnvio(facade); 
                            break;
                        case "4": 
                            Console.WriteLine(facade.ObtenerResumen()); 
                            Pausa(); 
                            break;
                        case "5":
                            facade.ConfirmarPedido();
                            Console.WriteLine("Pedido confirmado y guardado en pedidos.json");
                            Pausa();
                            facade.Reset();
                            break;
                        case "0":
                            Console.WriteLine("Adios!!");
                            break;
                        default: 
                            Console.WriteLine("Opción inválida"); 
                            Pausa(); 
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    Pausa();
                }
            }
        }

        static void CargarCliente(CheckoutFacade facade)
        {
            Console.Write("Nombre del cliente: ");
            var nombre = Console.ReadLine() ?? string.Empty;
            Console.Write("Dirección: ");
            var direccion = Console.ReadLine() ?? string.Empty;
            facade.SetDatosCliente(nombre, direccion);
            Console.WriteLine("Datos cargados.");
            Pausa();
        }

        static void AgregarProducto(CheckoutFacade facade)
        {
            Console.Write("Nombre del producto: ");
            var nombre = Console.ReadLine() ?? string.Empty;

            Console.Write("Precio (ej 1999,99): ");
            if (!decimal.TryParse(Console.ReadLine(), out var precio) || precio < 0)
                throw new ArgumentException("Precio inválido");

            Console.Write("Cantidad: ");
            if (!int.TryParse(Console.ReadLine(), out var cantidad) || cantidad <= 0)
                throw new ArgumentException("Cantidad inválida");

            facade.AgregarProducto(nombre, precio, cantidad);
            Console.WriteLine("Producto agregado.");
            Pausa();
        }

        static void SeleccionarEnvio(CheckoutFacade facade)
        {
            Console.Write("Tipo de envío (moto/correo/retiro): ");
            var tipo = Console.ReadLine()?.Trim().ToLowerInvariant() ?? "";
            facade.SeleccionarEnvio(tipo);
            Console.WriteLine("Estrategia de envío aplicada.");
            Pausa();
        }

        static void Pausa()
        {
            Console.WriteLine("Presioná una tecla para continuar...");
            Console.ReadKey();
        }
    }
}

