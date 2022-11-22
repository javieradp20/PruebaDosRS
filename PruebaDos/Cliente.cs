using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaDos
{
    internal class Cliente
    {
        #region Propiedades
        public string Nombre { get; set; }
        public string Rut { get; set;}
        public int Fono { get; set; }

        #endregion

        #region Constructor
        public Cliente(string nombre, string rut, int fono) { 
            Nombre = nombre;
            Rut = rut;
            Fono = fono;
        }
        #endregion

        #region Metodo
        #endregion
    }
}
