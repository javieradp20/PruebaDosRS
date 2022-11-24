using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaDos
{
    internal class GestionarProducto
    {
        //Propiedades y Varieables 

        static List<Cliente> listaClientes;
        static List<Producto> listaProductos;
        static List<Operacion> listaOperaciones;

        #region Direcciones 
        // ruta de javiera- felipe cambia la ruta para probar 
        static string rutaCliente = @"C:\Users\k_pab\source\repos\PruebaDos\PruebaDos\bin\Debug\clientes.txt";
        static string rutaProducto = @"C:\Users\k_pab\source\repos\PruebaDos\PruebaDos\bin\Debug\productos.txt";
        static string rutaOperacion = @"C:\Users\k_pab\source\repos\PruebaDos\PruebaDos\bin\Debug\Operacion.txt";
        #endregion

        #region Utilidades
        public static void ConteoGeneral()
        {
            int i = listaClientes.Count;
            int j = listaProductos.Count;
            int k = listaOperaciones.Count;

            Console.WriteLine(
                "Cantidad de clientes:" + i + Environment.NewLine +
                "Cantidad de Productos:" + j + Environment.NewLine +
                "Operacion Realizadas:" + k + Environment.NewLine);
        }
        #endregion

        public static void RellenarListasClientes()
        {
            string[] lines;
            listaClientes = new List<Cliente>();

            using (StreamReader sr = new StreamReader(rutaCliente))
            {
                try
                {
                    while (!sr.EndOfStream)
                    {
                        lines = sr.ReadLine().Split("|");
                        string nombre = lines[0];
                        string rut = lines[1];
                        int fono = int.Parse(lines[2]);
                        listaClientes.Add(new Cliente(nombre,rut,fono));
                    }
                }
                catch
                {
                    Console.WriteLine("No se encuntra cliente en la lista");
                }
                sr.Dispose();
                sr.Close();
            }
        }

        public static void RellenarlistaProducto()
        {
            string[] linea;
            listaProductos = new List<Producto>();
            using (StreamReader sr = new StreamReader(rutaProducto))
            {
                try
                {
                    while (!sr.EndOfStream)
                    {
                        linea = sr.ReadLine().Split("|");
                        string[] prefe = linea[5].Split("-");
                        listaProductos.Add(new Producto(int.Parse(linea[0]), linea[1], DateTime.Parse(linea[2]),
                            linea[3], int.Parse(linea[4]), prefe, Convert.ToBoolean(linea[6])));
                    }
                }
                catch
                {
                    Console.WriteLine("No se encuentran productos en la lista");
                }
                sr.Dispose();
                sr.Close();
            }

        }

        public static void RellenarListaOperaciones()
        {
            string[] linea;
            listaOperaciones = new List<Operacion>();
            using (StreamReader sr = new StreamReader(rutaOperacion))
            {
                try
                { 
                    while (!sr.EndOfStream)
                    {
                       
                        linea = sr.ReadLine().Split("|");
                        int idOperacion = int.Parse(linea[0]);
                        DateTime fechaOperacion = DateTime.Parse(linea[1]);
                        string tipoOperacion = linea[2];
                        string idCliente = linea[3];
                        int idProducto = int.Parse(linea[4]);
                        
                        listaOperaciones.Add(new Operacion(idOperacion,
                            fechaOperacion, tipoOperacion, idCliente, idProducto));
                    }
                }
                catch
                {
                    Console.WriteLine("No hay operaciones en lista.");
                }
                sr.Dispose();
                sr.Close();
            }
        }
    }

}
