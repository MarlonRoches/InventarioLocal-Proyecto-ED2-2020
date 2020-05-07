using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace Desarrollo_Proyecto_ED_2
{
    class Data
    {
        class Nodo
        {
            public Nodo NodoIzq;
            public Nodo NodoDer;
            public double Probabilidad;
            public byte Caracter;
        }
        private static Data _instance = null;
        public static Data x
        {
            get
            {
                if (_instance == null) _instance = new Data();
                return _instance;
            }
        }
        string GlobalPath = string.Empty;
        Dictionary<string, Producto> Productos = new Dictionary<string, Producto>();
        Dictionary<string, Sucursal> Sucursales = new Dictionary<string, Sucursal>();
        Dictionary<string, Relacion> Relacion = new Dictionary<string, Relacion>();

        public void AgregarSucursal(Sucursal Sucursal)
        {
            CargarSucursales();
            if (!Sucursales.ContainsKey($"{Sucursal.Id}"))
            {
                Sucursales.Add($"{Sucursal.Id}", Sucursal);
                UpdateSucursales();
            }
            else
            {
                //ya existe la sucursal
            }

        }
        public void AgregarProducto(Producto ProductoNuevo)
        {
            CargarProductos();
            if (!Productos.ContainsKey($"{ProductoNuevo.Id}"))
            {
                Productos.Add($"{ProductoNuevo.Id}", ProductoNuevo);
                UpdateProductos();
            }
            else
            {
                //ya existe la sucursal
            }

        }
        public void AgregarProductoEnSucursal(int idSucursal, Producto Producto,int stock)
        {
            //existe el producto?
            CargarProductos();
            if (Productos.ContainsKey($"{Producto.Id}"))
            {
            //existe la sucursal?
                CargarSucursales();
                if (Sucursales.ContainsKey($"{idSucursal}"))
                {
            //existe la relacion?
                    CargarRelacion();
                    if (!Relacion.ContainsKey($"{idSucursal}^{Producto.Id}"))
                    {
                        var NuevaRelacion = new Relacion()
                        {
                            Id_Producto = Producto.Id,
                            Id_Sucursal = idSucursal,
                            Stock = stock
                        };
                        Relacion.Add($"{idSucursal}^{Producto.Id}", NuevaRelacion);
                        //agrega sucursal
                    }
                    else
                    {
                        Relacion[$"{idSucursal}^{Producto.Id}"].Stock++;
                        //aumenta el stock
                    }
                }
                else
                {
                    //no existe la sucursal

                }
                //comprimir y cifrar
                UpdateRelacion();
            }
            else
            {
                // no exisiste el producto
            }

        }
        public void ModificarProducto(int id,string nombrenuevo, double precionuevo)
        {
            CargarProductos();
            if (Productos.ContainsKey($"{id}"))
            {
                Productos[$"{id}"].Nombre = nombrenuevo;
                Productos[$"{id}"].Precio= precionuevo;
                UpdateProductos();
            }
            else
            {
                //no lo contiene
            }
        }

        public void LeerCSV(string path)
        {
            var file = new FileStream(path,FileMode.Open);
            var reader = new StreamReader(file);
            var linea = reader.ReadLine();
            CargarProductos();
            while (linea!=null)
            {
                var arrayAux = linea.Split(';');
                var Productonuevo = new Producto()
                {
                    Id = int.Parse(arrayAux[0]),
                    Nombre = arrayAux[1],
                    Precio = double.Parse(arrayAux[2])

                };
                AgregarProducto(Productonuevo);
                linea = reader.ReadLine();
            }
            UpdateProductos();
            reader.Close();
            file.Close();
        }

        public void ModificarRelacion(string NombreRelacional, int stockNuevo)
        {
            //existe el producto?
            CargarProductos();
            if (Productos.ContainsKey(NombreRelacional.Split('^')[1]))
            {
                //existe la sucursal?
                CargarSucursales();
                if (Sucursales.ContainsKey(NombreRelacional.Split('^')[0]))
                {
                    //existe la relacion?
                    CargarRelacion();
                    if (Relacion.ContainsKey(NombreRelacional))
                    {
                        Relacion[NombreRelacional].Stock = stockNuevo;
                        UpdateRelacion();
                        //cambia el stock
                    }
                    else
                    {
                        //no existe la relacion
                    }
                }
                else
                {
                    //no existe la sucursal

                }
                //comprimir y cifrar
                UpdateRelacion();
            }
            else
            {
                // no exisiste el producto
            }
        }
        public void ModificarSucursal(int id, string nombrenuevo, string NuevaDireccion)
        {
            CargarSucursales();
            if (Sucursales.ContainsKey($"{id}"))
            {
                Sucursales[$"{id}"].Nombre= nombrenuevo;
                Sucursales[$"{id}"].Direccion= NuevaDireccion;
                UpdateSucursales();
            }
            else
            {
                //no lo contiene
            }
        }

        public void Transferir(string idProducto,string idEmisior, string idReceptor, int cantidadDeTransferencia)
        {
            //existe el producto?
            CargarProductos();
            if (Productos.ContainsKey(idProducto))
            {
                //existe la sucursal?
                CargarSucursales();
                if (Sucursales.ContainsKey(idReceptor)&& Sucursales.ContainsKey(idEmisior))
                {
                    //existen ambas relaciones?
                    CargarRelacion();
                    if (Relacion.ContainsKey($"{idEmisior}^{idProducto}") && Relacion.ContainsKey($"{idReceptor}^{idProducto}"))
                    {//si existen ambos

                        for (int i = 0; i < cantidadDeTransferencia; i++)
                        {
                            Relacion[$"{idEmisior}^{idProducto}"].Stock--;
                            Relacion[$"{idReceptor}^{idProducto}"].Stock++;
                        }
                        UpdateRelacion();
                        //cambia el stock
                    }
                    else
                    {
                        //no existe la relacion
                    }
                }
                else
                {
                    //no existe la sucursal

                }
                //comprimir y cifrar
                UpdateRelacion();
            }
            else
            {
                // no exisiste el producto
            }
        }


        #region Tablas
        public void CrearTablas()
        {
            
                if (!File.Exists("Productos.txt"))
                {
                    ////cifrar

                    var cifrado = SDESCifrado("1010101100", "1100110111", JsonConvert.SerializeObject(Productos));
                    var File = new FileStream("Productos.txt", FileMode.Create);

                    var wrtr = new StreamWriter(File);
                    ////comprimir
                    // Huffman(cifrado);
                    wrtr.WriteLine(cifrado);
                    wrtr.Close();
                    File.Close();
                }
                if (!File.Exists("Sucursales.txt"))
                {
                    var cifrado = SDESCifrado("1010101100", "1100110111", JsonConvert.SerializeObject(Sucursales));
                    var File = new FileStream("Sucursales.txt", FileMode.Create);

                    var wrtr = new StreamWriter(File);
                    ////comprimir
                    // Huffman(cifrado);
                    wrtr.WriteLine(cifrado);
                    wrtr.Close();
                    File.Close();
               
                }
                if (!File.Exists("Relacion.txt"))
                {
                var cifrado = SDESCifrado("1010101100", "1100110111", JsonConvert.SerializeObject(Relacion));
                    var File = new FileStream("Relacion.txt", FileMode.Create);

                    var wrtr = new StreamWriter(File);
                    ////comprimir
                    // Huffman(cifrado);
                    wrtr.WriteLine(cifrado);
                    wrtr.Close();
                    File.Close();
                }
            
        }
         void CargarProductos()
        {
            var Raw = new StreamReader("Productos.txt");
            var json = Raw.ReadToEnd();
            Raw.Close();
            Sucursales = JsonConvert.DeserializeObject<Dictionary<string, Sucursal>>(SDESDecifrado("1010101100", "1100110111", json.Trim()));


        }
         void CargarSucursales()
        {
            var Raw = new StreamReader("Sucursales.txt");
            var json = Raw.ReadToEnd();
            Raw.Close();
            Sucursales = JsonConvert.DeserializeObject<Dictionary<string, Sucursal>>(SDESDecifrado("1010101100", "1100110111", json.Trim()));
        }
         void CargarRelacion()
        {
            var Raw = new StreamReader("Relacion.txt");
            var json = Raw.ReadToEnd();
            Raw.Close();
            Sucursales = JsonConvert.DeserializeObject<Dictionary<string, Sucursal>>(SDESDecifrado("1010101100", "1100110111", json.Trim()));


        }
        void UpdateProductos()
        {
            var file = new FileStream("Productos.txt", FileMode.Create);
            var lol = SDESCifrado("1010101100", "1100110111", JsonConvert.SerializeObject(Productos));
            var writer = new StreamWriter(file);
            writer.Write(lol); 
            writer.Close();
            file.Close();
        }
        void UpdateRelacion()
        {
            var file = new FileStream("Relacion.txt", FileMode.Create);
            var writer = new StreamWriter(file);
            var lol = SDESCifrado("1010101100", "1100110111", JsonConvert.SerializeObject(Relacion));
            writer.Write(lol); writer.Close();
            file.Close();
        }
        void UpdateSucursales()
        {
            var file = new FileStream("Sucursales.txt", FileMode.Create);
            var writer = new StreamWriter(file);
            var lol =SDESCifrado("1010101100", "1100110111", JsonConvert.SerializeObject(Sucursales));
            writer.Write(lol);
            writer.Close();
            file.Close();
        }

        #endregion
        #region VariablesGlobales
        string original_path = string.Empty;
        string index_p10 = "2416390875";
        //string Index_PermutacionSeleccionada = "39860127";
        string index_p8 = "52637498";
        string index_p4 = "0321";
        string index_Expand = "13023201";
        string index_inicial = "15203746";
        string index_IPinverse = "30246175";
        string index_leftshift1 = "12340";
        string[,] S0 = new string[4, 4];
        string[,] S1 = new string[4, 4];

        #endregion
        #region MetodosSDES

        //CIFRANDO
         string[] ReturnKeys(string KeyGet)
        {
            S0[0, 0] = "01";
            S0[0, 1] = "00";
            S0[0, 2] = "11";
            S0[0, 3] = "10";
            S0[1, 0] = "11";
            S0[1, 1] = "10";
            S0[1, 2] = "01";
            S0[1, 3] = "00";
            S0[2, 0] = "00";
            S0[2, 1] = "10";
            S0[2, 2] = "01";
            S0[2, 3] = "11";
            S0[3, 0] = "11";
            S0[3, 1] = "01";
            S0[3, 2] = "11";
            S0[3, 3] = "10";
            S1[0, 0] = "00";
            S1[0, 1] = "01";
            S1[0, 2] = "10";
            S1[0, 3] = "11";
            S1[1, 0] = "10";
            S1[1, 1] = "00";
            S1[1, 2] = "01";
            S1[1, 3] = "11";
            S1[2, 0] = "11";
            S1[2, 1] = "00";
            S1[2, 2] = "01";
            S1[2, 3] = "00";
            S1[3, 0] = "10";
            S1[3, 1] = "01";
            S1[3, 2] = "00";
            S1[3, 3] = "11";


            var originalkey = int.Parse(KeyGet);
            var KEYAR = Generarkeys(originalkey);
            return KEYAR;
        }
         string SDESCifrado(string llave1, string llave2,string path)
        {
            S0[0, 0] = "01";
            S0[0, 1] = "00";
            S0[0, 2] = "11";
            S0[0, 3] = "10";
            S0[1, 0] = "11";
            S0[1, 1] = "10";
            S0[1, 2] = "01";
            S0[1, 3] = "00";
            S0[2, 0] = "00";
            S0[2, 1] = "10";
            S0[2, 2] = "01";
            S0[2, 3] = "11";
            S0[3, 0] = "11";
            S0[3, 1] = "01";
            S0[3, 2] = "11";
            S0[3, 3] = "10";
            S1[0, 0] = "00";
            S1[0, 1] = "01";
            S1[0, 2] = "10";
            S1[0, 3] = "11";
            S1[1, 0] = "10";
            S1[1, 1] = "00";
            S1[1, 2] = "01";
            S1[1, 3] = "11";
            S1[2, 0] = "11";
            S1[2, 1] = "00";
            S1[2, 2] = "01";
            S1[2, 3] = "00";
            S1[3, 0] = "10";
            S1[3, 1] = "01";
            S1[3, 2] = "00";
            S1[3, 3] = "11";

            var Salida = "";
            foreach (var item in path)
            {
                var bin = Convert.ToString(item, 2).PadLeft(8, '0');
                var monitor = Convert.ToByte(BinarioADecimal(CifradoSDES(llave1, llave2, bin)));
                Salida += (char)monitor;
            }
            return Salida;
        }
        //decifrado
         string SDESDecifrado(string llave1, string llave2,string path)
        {
            S0[0, 0] = "01";
            S0[0, 1] = "00";
            S0[0, 2] = "11";
            S0[0, 3] = "10";
            S0[1, 0] = "11";
            S0[1, 1] = "10";
            S0[1, 2] = "01";
            S0[1, 3] = "00";
            S0[2, 0] = "00";
            S0[2, 1] = "10";
            S0[2, 2] = "01";
            S0[2, 3] = "11";
            S0[3, 0] = "11";
            S0[3, 1] = "01";
            S0[3, 2] = "11";
            S0[3, 3] = "10";
            S1[0, 0] = "00";
            S1[0, 1] = "01";
            S1[0, 2] = "10";
            S1[0, 3] = "11";
            S1[1, 0] = "10";
            S1[1, 1] = "00";
            S1[1, 2] = "01";
            S1[1, 3] = "11";
            S1[2, 0] = "11";
            S1[2, 1] = "00";
            S1[2, 2] = "01";
            S1[2, 3] = "00";
            S1[3, 0] = "10";
            S1[3, 1] = "01";
            S1[3, 2] = "00";
            S1[3, 3] = "11";

            var Salida = "";
            foreach (var item in path)
            {
                var bin = Convert.ToString(item, 2).PadLeft(8, '0');
                var monitor = Convert.ToByte(BinarioADecimal(CifradoSDES(llave2, llave1, bin)));
                Salida += (char)monitor;
            }
            return Salida;
        }

        string CifradoSDES(string key1, string key2, string actual)
        {
            //1 permutar 8
            var entrada = Inicial(actual);

            //2 tomar izquierda y derecha 
            var Mitadizquierda = entrada.Substring(0, 4);
            var MitadDerecha = entrada.Remove(0, 4);

            //3 expandir derecho
            var expandido = Expandir(MitadDerecha).PadLeft(10,'0');

            //4 xor key1 y lado derecho
            var xorResultado = XOR(key1, expandido);

            //5 separar en bloques de 4
            var xor1izquierda = xorResultado.Substring(0, 4);
            var xor1derecha = xorResultado.Remove(0, 4);

            //6 s0box para xorizq y s1box para xorder
            var Yaux = BinarioADecimal(($"{xor1izquierda[1]}{xor1izquierda[2]}"));
            var XAux = BinarioADecimal(($"{xor1izquierda[0]}{xor1izquierda[3]}"));

            var BoxResultL = S0[XAux, Yaux];//izquierda
            Yaux = BinarioADecimal(($"{xor1derecha[1]}{xor1derecha[2]}"));
            XAux = BinarioADecimal(($"{xor1derecha[0]}{xor1derecha[3]}"));
            var BoxResultD = S1[XAux, Yaux];//derecha

            //7 P4 a BoxResultL 
            var paso7 = P4($"{BoxResultL}{BoxResultD}");

            //8 XOR con mitarizquierda
            var paso8 = XOR(Mitadizquierda, paso7);

            //ppaso 9 y 10
            var juntosSwaped = MitadDerecha + paso8;

            //paso 11 EP bloque 2 del paso10 
            var segundoexpandido = Expandir(juntosSwaped.Remove(0, 4)).PadLeft(10,'0');
            var monico = segundoexpandido.Length;

            ////paso12 xor de segundo expandido con key 2
            var xorPaso12 = XOR(key2, segundoexpandido);
            var xorpaso12izq = xorPaso12.Substring(0, 4);
            var xorpaso12der = xorPaso12.Remove(0, 4);

            //13 s0box para xorpaso12izq y 1 para el derecho
            Yaux = BinarioADecimal(($"{xorpaso12izq[1]}{xorpaso12izq[2]}"));
            XAux = BinarioADecimal(($"{xorpaso12izq[0]}{xorpaso12izq[3]}"));
            var s0result = S0[XAux, Yaux];
            Yaux = BinarioADecimal(($"{xorpaso12der[1]}{xorPaso12.Remove(0, 4)[2]}"));
            XAux = BinarioADecimal(($"{xorpaso12der[0]}{xorpaso12der[3]}"));
            var s1result = S1[XAux, Yaux];

            //14 P4 para s0 + s1
            var Pas14 = P4(s0result + s1result);

            //15 XOR resultado paso14 con bloque1 del swap(paso10) 
            var paso15 = XOR(juntosSwaped.Substring(0, 4), Pas14);

            //16 union
            var paso16 = paso15 + juntosSwaped.Remove(0, 4);

            //17 Ip inverso
            var SalidaCifrada = IPReverse(paso16);
            return SalidaCifrada;
        }
        string[] Generarkeys(int llave)
        {
            var Devolver = new string[2];

            var binarikey = Convert.ToString(llave, 2); //1010000010
            binarikey = binarikey.PadLeft(10, '0');
            var binarikeyp10 = P10(binarikey);
            var subkey1 = binarikeyp10.Substring(0, 5);
            var subkey2 = binarikeyp10.Remove(0, 5);
            var shifedsubkey1 = LeftShift1(subkey1);
            var shifedsubkey2 = LeftShift1(subkey2);
            //primera key
            Devolver[0] = P8($"{shifedsubkey1}{shifedsubkey2}");
            //segunda key
            Devolver[1] = P8($"{LeftShift1(LeftShift1(shifedsubkey1))}{LeftShift1(LeftShift1(shifedsubkey2))}");

            return Devolver;
        }
        int BinarioADecimal(string Binario) //String binario a byte
        {

            int num, ValorBinario, ValorDecimal = 0, baseVal = 1, rem;
            num = int.Parse(Binario);
            ValorBinario = num;

            while (num > 0)
            {
                rem = num % 10;
                ValorDecimal = ValorDecimal + rem * baseVal;
                num = num / 10;

                baseVal = baseVal * 2;
            }
            return Convert.ToInt32(ValorDecimal);
        }
        string XOR(string Comparador, string AComparar)
        {
            var xorResult = string.Empty;
            for (int i = 0; i < Comparador.Length; i++)
            {
                if (AComparar[i] == Comparador[i])
                {
                    xorResult = $"{xorResult}{0}";
                }
                else
                {
                    xorResult = $"{xorResult}{1}";
                }
            }
            return xorResult;
        }
        string Expandir(string aExpandir)
        {
            var Expandido = string.Empty;
            foreach (var index in index_Expand)
            {
                Expandido = $"{Expandido}{aExpandir[int.Parse(index.ToString())]}";
            }
            return Expandido;
        }
        string IPReverse(string actual)
        {
            var IP8RevReturn = string.Empty;
            foreach (var index in index_IPinverse)
            {
                IP8RevReturn = $"{IP8RevReturn}{actual[int.Parse(index.ToString())]}";
            }
            return IP8RevReturn;
        }
        string Inicial(string actual)
        {
            var iniciaretl = string.Empty;
            foreach (var index in index_inicial)
            {
                iniciaretl = $"{iniciaretl}{actual[int.Parse(index.ToString())]}";
            }
            return iniciaretl;
        }
        string P8(string actual)
        {
            var P8return = string.Empty;
            foreach (var index in index_p8)
            {
                P8return = $"{P8return}{actual[int.Parse(index.ToString())]}";
            }
            return P8return;
        }
        string LeftShift1(string aShiftear)
        {
            var Shifted = string.Empty;
            foreach (var index in index_leftshift1)
            {
                Shifted = $"{Shifted}{aShiftear[int.Parse(index.ToString())]}";
            }
            return Shifted;
        }
        string P10(string Entrada10bits)
        {
            var P10return = string.Empty;
            foreach (var index in index_p10)
            {
                P10return = $"{P10return}{Entrada10bits[Convert.ToInt32(Convert.ToString(index))]}";
            }
            return P10return;
        }
        string P4(string aPermutar)
        {
            var permmuted = string.Empty;
            foreach (var index in index_p4)
            {
                permmuted = $"{permmuted}{aPermutar[Convert.ToInt32(Convert.ToString(index))]}";
            }
            return permmuted;
        }
        #endregion
        #region Lzw
         string CompresionLZW(string json)
        {
            int Iteracion;
            string salida = "";  //cambiar por escritura del archivo
            string W = "", K = "";
            var DiccionarioGeneral = ObetnerDiccionarioInicial();//poner como parametro el path global de data
            var DiccionarioWK = ObetnerDiccionarioInicial();
            var residuoEscritura = string.Empty;
            var diccionarioescrito = true;
            CompresionLZW();

            void CompresionLZW()
            {
                Iteracion--;
                foreach (var Caracter in json)
                {
                    var WK = "";
                    if (W == "")
                    {
                        WK = (Caracter).ToString();
                        Validacion_Diccionario(WK);
                    }
                    else
                    {
                        K = ((char)Caracter).ToString();
                        WK = W + K;
                        Validacion_Diccionario(WK);
                    }
                }
                Agregar_A_Salida(DiccionarioGeneral[W], false);
            }

            void Validacion_Diccionario(string WK)
            {
                if (diccionarioescrito)
                {
                    EscribirDiccionario();
                }
                if (DiccionarioGeneral.ContainsKey(WK))
                {
                    W = WK;
                }
                else
                {
                    Iteracion++;
                    DiccionarioGeneral.Add(WK, Iteracion); //generamos codigo

                    Agregar_A_Salida(DiccionarioGeneral[W], true);

                    W = K;

                }
            }

            void Agregar_A_Salida(int id, bool caso)
            {
                var carsito = (char)id;
                salida += carsito;
            }

            void EscribirDiccionario()
            {
                if (diccionarioescrito)
                {
                    foreach (var item in DiccionarioWK)
                    {
                        salida += ($"{item.Key}|{item.Value}♀");
                    }
                    salida += ("END");
                    diccionarioescrito = false;
                }
            }

            Dictionary<string, int> ObetnerDiccionarioInicial()
            {
                var Diccionario = new Dictionary<string, int>();
                Iteracion = 0;
                foreach (var Caracter in json) //Crear diccionario de letras
                {
                    if (!Diccionario.ContainsKey(Convert.ToString(Caracter)))
                    {
                        Diccionario.Add(Convert.ToString(Caracter), Iteracion);
                        Iteracion++;
                    }
                }
                return Diccionario;
            }
            return salida;
        }
         string DescompresionLZW(string json)
        {
            var fileTemp = new FileStream("temp.txt", FileMode.Create);
            var writer = new StreamWriter(fileTemp);
            writer.Write(json);
            writer.Close();
            fileTemp.Close();

            var DiccionarioDescompresion = new Dictionary<int, string>();
            var Iteracion = 0;
            var compreso = new FileStream("temp.txt", FileMode.Open);
            var lector = new StreamReader(compreso);
            var linea = string.Empty;
            linea += (char)lector.Read();
            int CodigoViejo = 0, CodigoNuevo = 0;
            string Cadena = string.Empty, Caracter = string.Empty;
            var Texto_Descompreso = string.Empty;

            while (!linea.Contains("END"))
            {

                linea += (char)lector.Read();
            }
            var caractrer = linea.Split('♀');
            foreach (var item in caractrer)
            {
                if (item == "END")
                {
                    break;
                }
                var temp = item.Split('|');
                if (temp.Length == 3)
                {
                    // tiene | incluido
                    DiccionarioDescompresion.Add(int.Parse(temp[2]), "|");
                }
                else
                {
                    DiccionarioDescompresion.Add(int.Parse(temp[1]), temp[0]);

                }

            }
            Iteracion = DiccionarioDescompresion.Count();
            CodigoViejo = lector.Read();
            Caracter = DiccionarioDescompresion[CodigoViejo];
            Texto_Descompreso += Caracter;

            //MIENTRAS (!EOF)
            while (true)
            {
                //...LEER cód_nuevo
                CodigoNuevo = lector.Read();
                if (CodigoNuevo == -1)
                {
                    break;
                }
                if (!DiccionarioDescompresion.ContainsKey(CodigoNuevo))
                {
                    Cadena = DiccionarioDescompresion[CodigoViejo] + DiccionarioDescompresion[CodigoViejo][0];
                    DiccionarioDescompresion.Add(Iteracion, Cadena);
                    Iteracion++;
                    Texto_Descompreso += CodigoNuevo;
                    CodigoViejo = CodigoNuevo;
                }
                //...SINO
                else
                {
                    Cadena = DiccionarioDescompresion[CodigoNuevo];
                    Texto_Descompreso += Cadena;
                    Caracter = Cadena[0].ToString();
                    DiccionarioDescompresion.Add(Iteracion, $"{DiccionarioDescompresion[CodigoViejo]}{Caracter}");
                    Iteracion++;
                    CodigoViejo = CodigoNuevo;
                }
            }
            File.Delete("D:\\Pry_ED2\\temp.txt");
            return Texto_Descompreso;
        }

        #endregion
    }
}
