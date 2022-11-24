using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaDos
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            string resp = "0";
            do
            {
                GestionarProducto.RellenarlistaProducto();
                GestionarProducto.RellenarListaOperaciones();
                Console.ReadLine();
                Console.Clear();
                GestionarProducto.ConteoGeneral();
                Console.WriteLine(
                     "Ingrese su consulta seleccionando una opcion. \n" +
                    "(1)Agregar Cliente\n" +
                    "(2)Agregar Producto\n" +
                  
                    "(3)Listar Productos Disponibles\n" +
                    "(4)Listar Productos NO Disponibles\n" +
                    "(5)Listar Articulos Antiguos por fecha\n" +
                    "(6)listar Clientes\n" +
                  
                    "(7)ModificarProducto\n" +
                    "(8)Movimientos\n" +
                
                    "(9)Salir");
                resp = Console.ReadLine();

                switch (resp) {
                    case "1":
                        GestionarProducto.AgregarCliente();
                        break;
                    case "2":
                        GestionarProducto.AgregarProducto();
                        break;
                    case "3":
                        GestionarProducto.ProductosDisponibles();
                        break;
                    case "4":
                        GestionarProducto.ProductosNoDisponibles();
                        break;
                    case "5":
                        GestionarProducto.BuscarProductoPorFecha();
                        break;
                    case "6":
                        GestionarProducto.ListarClientes();
                        break;
                    case "7":
                        GestionarProducto.ModificarDisponible();
                        break;
                    case "8":
                        GestionarProducto.ListarOperaciones();
                        break;

                        //case "10":
                        //    GestionProducto.TesteoTexto();
                        //    break;

                }
            } while (resp != "9");
            Console.Write("Que tenga buen día");
        }
        
    }
}
