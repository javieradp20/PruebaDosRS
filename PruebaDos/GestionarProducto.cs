using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaDos
{
    internal class GestionarProducto
    {
        //Propiedades y Varieables 

        static List<Cliente> listaClientes;
        static List<Producto> listaProdutos;
        static List<Operacion> listaOperaciones;

        #region Direcciones 
        #endregion

        #region Utilidades
        public static void ConteoGeneral()
        {
            int i = listaClientes.Count;
            int j = listaProductos.Count;
            int k = listaOperaciones.Count;


        }
        #endregion
    }
}
