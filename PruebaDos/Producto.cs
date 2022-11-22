using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaDos
{
    internal class Producto
    {
        #region Propiedades
        public int CodigoProducto { get; set; }
        public DateTime FechaIngreso { get; set; }
        public string Descripcion { get; set; }
        public int ValorAproximado { get; set; }
        public string[] Preferencias { get; set; }
        public bool Disponibilidad { get; set; }
        public string ClienteId { get; set; }
        #endregion

        #region Costructor
        public Producto(int codigoProducto,string clinteId,DateTime fechaIngreso,string descripcion,
            int valorAproximado,string[] preferencias, bool disponibilidad)
        {
            CodigoProducto = codigoProducto;
            FechaIngreso = fechaIngreso;
            Descripcion = descripcion;
            ValorAproximado = valorAproximado;
            Preferencias = preferencias;
            Disponibilidad = disponibilidad;
            ClienteId = clinteId;
        }
        #endregion

        #region Metodo
        public void MostrarProducto()
        {

        }
        #endregion

    }

}
