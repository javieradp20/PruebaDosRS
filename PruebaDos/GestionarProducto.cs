using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
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
        static string rutaCliente = @"C:\ProgramacionAvansada2\PruebaDos\ruta\clientes.txt";
        static string rutaProducto = @"C:\ProgramacionAvansada2\PruebaDos\ruta\productos.txt";
        static string rutaOperacion = @"C:\ProgramacionAvansada2\PruebaDos\rutas\operacionnes.txt";
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
        private static byte[] Random(int size) {
            using (var rand = RandomNumberGenerator.Create()) {
                var id = new byte[size];
                rand.GetBytes(id);
                return id;
            }
        }
        #endregion

        public static void RellenarListasClientes()
        {
            string[] linea;
            listaClientes = new List<Cliente>();

            using (StreamReader sr = new StreamReader(rutaCliente))
            {
                try
                {
                    //si no es el final del stream, loop
                    while (!sr.EndOfStream)
                    {
                        //divide la linea leida por "|" ingresado al momento de guardar
                        //Inicia desde 1 ya que el 0 es la fecha en la cual se ingreso solo queda registrada en el txt.
                        linea = sr.ReadLine().Split('|');
                        string nombre = linea[0];
                        string rut = linea[1];
                        int fono = int.Parse(linea[2]);
                        //se ingresan los valores leidos del string a cada parametro del nuevo cliente, en la lista.
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
                        linea = sr.ReadLine().Split('|');
                        string[] prefe = linea[5].Split('-');
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
                { //si no es el final del stream, loop
                    while (!sr.EndOfStream)
                    {
                        //divide la linea leida por "|" ingresado al momento de guardar
                        linea = sr.ReadLine().Split('|');
                        int idOperacion = int.Parse(linea[0]);
                        DateTime fechaOperacion = DateTime.Parse(linea[1]);
                        string tipoOperacion = linea[2];
                        string idCliente = linea[3];
                        int idProducto = int.Parse(linea[4]);
                        //se ingresan los valores leidos del string a cada parametro del nuevo cliente, en la lista.
                        listaOperaciones.Add(new Operacion(idOperacion,
                            idCliente, idProducto.ToString(), tipoOperacion, fechaOperacion));
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
        #region MostrarListas
        public static void ListarOperaciones() {
            if (listaOperaciones.Count != 0) {
                //Consulta la lista, muestra todas las operaciones listadas.
                IEnumerable<Operacion> busquedaFecha = from Operacion in listaOperaciones orderby Operacion.FechaOperacion descending select Operacion;
                Console.WriteLine("Operaciones realizadas:");
                foreach (Operacion busqueda in busquedaFecha) {
                    busqueda.MostrarOperacion();
                }
            }
            else {
                Console.WriteLine("Lista sin Operaciones");
            }
            Console.ReadLine();
        }
        public static void ListarClientes() {
            if (listaClientes.Count != 0) {
                Console.Clear();
                foreach (Cliente cliente in listaClientes) {
                    int posicion = listaClientes.IndexOf(cliente) + 1;

                    Console.Write("(" + posicion + ")");
                    cliente.MostrarCliente();

                }
                Console.WriteLine("Listado Exitoso");
                Console.ReadLine();
            }
            else { Console.WriteLine("Listado sin clientes "); }
        }
        public static void ProductosDisponibles() {
            Console.Clear();
            Console.WriteLine("----PRODUCTOS DISPONIBLES-----");
            IEnumerable<Producto> estadoDisponible = from Producto in listaProductos where Producto.Disponible.Equals(true) select Producto;//Cada producto "true" se agrega a estadoDisponible

            foreach (Producto disponibles in estadoDisponible) {
                disponibles.MostrarProducto();
            }
            Console.ReadLine();
        }
        public static void ProductosNoDisponibles() {
            Console.Clear();

            Console.WriteLine("----PRODUCTOS NO DISPONIBLES-----");
            IEnumerable<Producto> estadoNoDisponible = from Producto in listaProductos where Producto.Disponible == false select Producto;//Cada producto "false" se agrega a estadoDisponible

            foreach (Producto noDisponibles in estadoNoDisponible) {
                noDisponibles.MostrarProducto();
            }
            Console.ReadLine();
        }
        public static void BuscarProductoPorFecha() {
            IEnumerable<Producto> busquedaFecha = from Producto in listaProductos orderby Producto.FechaIngreso descending select Producto;

            //VALIDAMOS DÍA, MES Y AÑO NO SUPERIOR A LA FECHA ACTUAL
            string d, m, a;
            string fecha;
            int r = 0;
            Console.Clear();
            Console.WriteLine("Busqueda por fecha\n" +
                "Ingrese el limite de la busqueda por fecha de los productos.\n");
            do {
                Console.WriteLine("Dia:00");
                d = Console.ReadLine();
            } while (!(int.TryParse(d, out r)) || r > 31);

            do {
                Console.WriteLine("Mes:00");
                m = Console.ReadLine();
            } while (!(int.TryParse(m, out r) || r > 12));

            do {
                Console.WriteLine("Año: 0000");
                a = Console.ReadLine();
            } while (a.Length != 4 || !(int.TryParse(a, out r)) || r < 1990 & r < int.Parse(DateTime.Today.Year.ToString()));


            //Asignamos la fecha de hoy a Fecha Final
            DateTime fechaFinal = DateTime.Today;

            try {
                fecha = d + "/" + m + "/" + a;
                fechaFinal = DateTime.Parse(fecha);
                //Compara dos fechas dando como resultado  -1,0 o 1, si la ingresada es mayor que la actual, se debe volver a ingresar una fecha.
                int resultado = DateTime.Compare(fechaFinal, DateTime.Today.AddDays(1));

                if (resultado <= 0) {
                    Console.Clear();
                    //Indica las dos fechas al usuario que se están verificando.
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("Fecha Hoy:{0}\n" +
                        "Fecha Límite:{1}", DateTime.Now, fechaFinal);


                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine("-----PRODUCTOS EN FECHA-----");

                    foreach (Producto busquedaProducto in busquedaFecha) {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        resultado = DateTime.Compare(fechaFinal, busquedaProducto.FechaIngreso);
                        if (resultado <= 0) {
                            busquedaProducto.MostrarProducto();
                        }
                    }
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine("-----PRODUCTOS FUERA DE FECHA  (ANTIGUOS)-----");
                    foreach (Producto busquedaProducto in busquedaFecha) {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        resultado = DateTime.Compare(fechaFinal, busquedaProducto.FechaIngreso);
                        if (resultado > 0) {
                            busquedaProducto.MostrarProducto();
                        }
                    }
                }
                else {
                    Console.WriteLine("Debe ingresar una fecha anterior al día de hoy");
                }

            }
            catch (Exception ex) {
                Console.WriteLine("Error ->" + ex.ToString());
            }
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.ReadLine();
        }
        #endregion

        #region Agregar
        public static void AgregarCliente() {
            Console.Clear();
            //VALIDA DATOS INGRESADOS.
            string strRut, nombre, strfono = "";
            string[] rut;
            int fono;
            DateTime fecha = DateTime.Now;
            Console.WriteLine("--NUEVO CLIENTE--");
            //Se valida solo que sea 1 nombre.
            do {
                Console.WriteLine("Ingrese Nombre:");
                nombre = Console.ReadLine().Trim();
            } while (nombre == string.Empty);
            do {
                Console.WriteLine("Ingrese Rut: 11111111-1");
                strRut = Console.ReadLine().Trim().ToString();
                rut = strRut.Split('-');
                //Se valida que sea de un largo maximo de 10 y no menor a 9,con dos string, run + digito verificador separado por "-"
            } while (strRut.Length > 10 || strRut.Length < 9 || rut.Length != 2);
            do {//La misma idea que el anterior, se verifica que sea un número y que sea de largo 9
                Console.WriteLine("Ingrese Fono: 988887777");
                strfono = Console.ReadLine();

            } while (int.TryParse(strfono, out fono) != true || strfono.Length != 9);

            Console.Clear();
            //-------------------------------------

            //VALIDA RUT DISPONIBLE PARA USO.
            //Con cliente rut, se verifica que no hay otro rut igual en el listado.
            IEnumerable<Cliente> busqueda = from Cliente in listaClientes where Cliente.Rut == strRut select Cliente;
            if (busqueda.Count() == 0) {
                listaClientes.Add(new Cliente(nombre, strRut, fono));
                GestionarProducto.insertarTxtCliente();
                GestionarProducto.GuardarOperacion(fecha, 2, strRut, 0);
                Console.WriteLine("Cliente Agregado exitosamente");
                Console.ReadLine();
            }
            else {
                Console.WriteLine("Rut ya existe en sistema.");
                Console.Read();
            }
        }
        public static void AgregarProducto() {
            Console.Clear();
            string descripcion, strFecha, strValor, strOpc1, strOpc2, idCliente = "";
            int valor, opc2;
            bool desactivado = false;
            //USUARIO NUEVO O USUARIO ANTIGUO
            do {
                //Se consulta si el usuario ya está ingresado a Sistema,
                //para obtener el rut, que es utilizado como codigoCliente (Clave foranea) en el producto.

                Console.WriteLine("TIPO DE CLIENTE:\n" +
                    "(1)Nuevo.");
                //Verifica que haya clientes ingresados.
                if (listaClientes.Count() != 0) {
                    Console.WriteLine("(2)Antiguo.");
                    desactivado = true;
                }
                strOpc1 = Console.ReadLine();
                //----------------------------------------------
                //Si es nuevo cliente, se gestionara el siguiente método.
                if (strOpc1 == "1") {
                    AgregarCliente();
                    //Por medio de LastOrDefault()en el listado general de clientes,se obtendrá el rut.
                    idCliente = listaClientes.LastOrDefault().Rut;
                }
                else if (strOpc1 == "2" && desactivado) {
                    if (listaClientes.Count() != 0) {
                        do {
                            Console.Clear();
                            ListarClientes();
                            Console.WriteLine("Indique un numero de cliente.");
                            strOpc2 = Console.ReadLine();

                        } while (!int.TryParse(strOpc2, out opc2) || opc2 < 0 || opc2 > listaClientes.Count());
                        idCliente = listaClientes[opc2 - 1].Rut;
                    }
                }
                //---------------------------------------------
                //Mientras no se haya elegido alguna de las dos opciones, o en caso de elegir, siga siendo 0 la cantidad de clientes.
            } while (!int.TryParse(strOpc1, out int opc1) || opc1 < 1 || opc1 > 2 || listaClientes.Count == 0);

            Console.Clear();
            DateTime fecha = DateTime.Now;
            strFecha = fecha.ToString();
            Console.WriteLine("Producto Nuevo:");
            Console.WriteLine("\nFecha Ingreso Producto: " + fecha + "\n");

            //Se valida el resto de la información que puede ser ingresada por el usuario.
            do {
                Console.WriteLine("Ingrese Descripcion:");
                descripcion = Console.ReadLine();
            } while (descripcion == string.Empty);

            do {
                Console.WriteLine("\nValor Aproximado:");
                strValor = Console.ReadLine().Trim();
            } while (!int.TryParse(strValor, out valor));
            string[] preferencias;
            string strPref;
            bool validador = true;
            do {
                Console.WriteLine("Ingrese sus preferencias separadas por un guion, mínimo 3");
                strPref = Console.ReadLine();
                preferencias = strPref.Split('-');

                foreach (string prefBlanco in preferencias) {
                    if (prefBlanco == "") {
                        validador = false;
                    }
                }

            } while (preferencias.Length < 3 && !validador);
            int codigo;
            bool valido = false;
            int i = 1;
            do {
                codigo = 1;
                foreach (byte rnd in Random(i)) {
                    codigo = codigo * rnd;
                }
                IEnumerable<Producto> busquedaCodigo = from Producto in listaProductos where Producto.CodigoProducto == codigo select Producto;

                if (busquedaCodigo.Count() == 0) {
                    listaProductos.Add(new Producto(codigo, idCliente, fecha, descripcion, valor, preferencias, true));
                    insertarTxtProducto();
                    GestionarProducto.GuardarOperacion(fecha, 1, idCliente, codigo);
                    Console.WriteLine("Producto Agregado exitosamente");
                    valido = true;
                }
                i++;
            } while (!valido);
            Console.ReadLine();

        }
        public static void GuardarOperacion(DateTime fecha, int tipo, string nCliente, int nProducto) {
            //Guarda en un txt la operacion, idOperacion, fecha, TIPO OPERACION, ID CLIENTE, ID PRODUCTO
            //
            //Tipo de Operacion (Y)
            string tipoOperacion = "N/N";
            switch (tipo) {
                case 1:
                    tipoOperacion = "Agregar Producto";
                    break;
                case 2:
                    tipoOperacion = "ClienteAgregado";
                    break;
                case 3:
                    tipoOperacion = "Modificar Producto";
                    break;
                case 4:
                    tipoOperacion = "Eliminar Producto";
                    break;
            }
            //Asigna un numero de operacion (Y)
            listaOperaciones = new List<Operacion>();
            int i = 1;
            bool valido = false;
            do {
                int nuevoId = 1;
                foreach (byte rnd in Random(i)) {
                    nuevoId = nuevoId * rnd;
                }
                IEnumerable<Operacion> filtroOperaciones = from Operacion in listaOperaciones where Operacion.IdOperacion == nuevoId select Operacion;
//aca voy
                if (filtroOperaciones.Count() == 0) {
                    listaOperaciones.Add(new Operacion(nuevoId, fecha, tipoOperacion, nCliente, nProducto));
                    GestionarProducto.insertarTxtOperacion();
                    valido = true;
                }
                i++;
            } while (!valido);
        }

        #endregion

        #region ModificarDisponible
        internal static void ModificarDisponible() {
            DateTime fechaModificacion = DateTime.Now;
            Console.Clear();
            //Mostrar Productos
            int contador = 0;
            IEnumerable<Producto> listadoProductos = from Producto in listaProductos select Producto;
            foreach (Producto seleccion in listaProductos) {
                contador++;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("Producto:" + contador + "--->");
                seleccion.MostrarProducto();
            }
            //Seleccionar Producto
            Console.WriteLine("Indique una valor de la lista(amarillo)");
            string codigoElegido = Console.ReadLine();
            Console.Clear();

            try {
                if (int.TryParse(codigoElegido, out int opc) || opc < listadoProductos.Count() && opc != 0) {
                    int i = opc - 1;
                    if (listaProductos[i].Disponible) {
                        listaProductos[i].Disponible = false;
                    }
                    else {
                        listaProductos[i].Disponible = true;
                    }
                    insertarTxtProducto();
                    Console.WriteLine("Producto Modificado exitosamente");
                    GestionarProducto.GuardarOperacion(fechaModificacion, 3, listaProductos[i].ClienteId, listaProductos[i].CodigoProducto);
                }
            }
            catch (Exception ex) {
                Console.WriteLine(ex);
                Console.ReadLine();
            }

        }
        #endregion

        #region InsertarTxt
        public static void insertarTxtCliente() {
            string texto = "";
            //Insertará texto en base solo a lo listado
            using (StreamWriter sw = new StreamWriter(rutaCliente)) {
                try {
                    string c = "";

                    foreach (Cliente cliente in listaClientes) {
                        c = cliente.Nombre + "|" + cliente.Rut + "|" + cliente.Fono;

                        //En caso de que no sea el último producto agrega una nueva linea a escribir.
                        if (listaClientes.Last<Cliente>().Rut != cliente.Rut) {
                            c = c + Environment.NewLine;
                        }
                        texto = texto + c;
                    }

                    //Envía al txt la información guardada.
                    sw.WriteLine(texto);
                }
                catch (Exception ex) {
                    Console.WriteLine(ex);
                    Console.ReadLine();
                }
                finally {
                    sw.Dispose();
                    sw.Close();
                }
            }
        }
        public static void insertarTxtProducto() {
            string texto = "";
            //Insertará texto en base solo a lo listado
            using (StreamWriter sw = new StreamWriter(rutaProducto)) {
                try {
                    int cont = 0;
                    string p = "";
                    foreach (Producto producto in listaProductos) {
                        string preferencias = "";
                        string[] pref = producto.Preferencias;
                        cont++;
                        for (int i = 1; i < pref.Length; i++)// Por cada preferencia concatena texto
                        {
                            preferencias = preferencias + pref[i];
                            if (pref.Length != (i))//agrega el guion siempre que no sea el último string
                            {
                                preferencias = preferencias + "-";
                            }
                            p = producto.CodigoProducto + "|" + producto.ClienteId + "|" + producto.FechaIngreso + "|" +
                            producto.Descripcion + "|" + producto.ValorAproximado + "|" + preferencias + "|" + producto.Disponible;//Agrega uno a uno cada producto.
                        }
                        if (listaProductos.Last<Producto>().CodigoProducto != producto.CodigoProducto)//En caso de que no sea el último producto agrega una nueva linea a escribir.
                        {
                            p = p + Environment.NewLine;
                        }
                        texto = texto + p;
                    }
                    sw.WriteLine(texto);//Envía al txt la información guardada.

                }
                catch (Exception ex) {
                    Console.WriteLine(ex);
                    Console.ReadLine();
                }
                finally {
                    sw.Dispose();
                    sw.Close();
                }
            }
        }
        public static void insertarTxtOperacion() {
            string texto = "";
            //Insertará texto en base solo a lo listado
            using (StreamWriter sw = new StreamWriter(rutaOperacion)) {
                try {
                    string o = "";

                    foreach (Operacion operacion in listaOperaciones) {
                        o = operacion.IdOperacion + "|" + operacion.FechaOperacion + "|" + operacion.TipoOperacion + "|" + operacion.IdCliente + "|" + operacion.IdProducto;

                        //En caso de que no sea el último producto agrega una nueva linea a escribir.
                        if (listaOperaciones.Last<Operacion>().IdOperacion != operacion.IdOperacion) {
                            o = o + Environment.NewLine;
                        }
                        texto = texto + o;
                    }

                    //Envía al txt la información guardada.
                    sw.WriteLine(texto);
                }
                catch (Exception ex) {
                    Console.WriteLine(ex);
                    Console.ReadLine();
                }
                finally {
                    sw.Dispose();
                    sw.Close();
                }
            }
        }
        #endregion
    }

}
