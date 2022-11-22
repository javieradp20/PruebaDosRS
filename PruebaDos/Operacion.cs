using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaDos
{
    internal class Operacion
    {
        #region Propiedades
        public int IdOperacion { get; set; }
        public string IdCliente { get; set; }
        public string IdProducto { get; set; }
        public string TipoOperacion {get; set; }
        public DateTime FechaOperacion { get; set; }
        #endregion

        #region Constructor
        public Operacion(int idOperacion,string idCliente,string idProducto,
            string tipoOperacon,DateTime fechaOperacion) {

            IdOperacion = idOperacion;
            IdCliente = idCliente;  
            IdProducto = idProducto;
            TipoOperacion = tipoOperacon;
            FechaOperacion = fechaOperacion;    
        }
        #endregion

        #region Metodos
        #endregion

    }
}
