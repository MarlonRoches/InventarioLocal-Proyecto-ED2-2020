using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace Desarrollo_Proyecto_ED_2
{
    class Singleton
    {
        class Nodo
        {
            public Nodo NodoIzq;
            public Nodo NodoDer;
            public double Probabilidad;
            public byte Caracter;
        }
        private static Singleton _instance = null;
        public static Singleton Ins
        {
            get
            {
                if (_instance == null) _instance = new Singleton();
                return _instance;
            }
        }
        string GlobalPath = string.Empty;
        Dictionary<string, Producto> Productos = new Dictionary<string, Producto>();
        Dictionary<string, Sucursal> Sucursales = new Dictionary<string, Sucursal>();
        Dictionary<string, Relacion> Relacion = new Dictionary<string, Relacion>();
        #region Tablas
        public void CrearTablas()
        {
            if (!Directory.Exists("D:\\Pry_ED2"))
            {
                DirectoryInfo di = Directory.CreateDirectory("D:\\Pry_ED2");
            var File = new FileStream("D:\\Pry_ED2\\Productos.txt", FileMode.Create);
                var wrtr = new StreamWriter(File);
                //cifrar
                wrtr.WriteLine("");
                wrtr.Close();
                File.Close();
            File = new FileStream("D:\\Pry_ED2\\Sucursales.txt", FileMode.Create);
                wrtr = new StreamWriter(File);
                wrtr.WriteLine(JsonConvert.SerializeObject(Productos));
                wrtr.Close();
                File.Close();
            File = new FileStream("D:\\Pry_ED2\\Relacion.txt", FileMode.Create);
                wrtr = new StreamWriter(File);
                wrtr.WriteLine(JsonConvert.SerializeObject(Productos));
                wrtr.Close();
                File.Close();
            }
        }

        public void CargarProductos()
        {
            var Raw = new StreamReader("D:\\Pry_ED2\\Productos.txt").ReadToEnd();
            //descomprimir 
            var descompreso = HuffmanDescompresion("ruta del  original");
             //Descifrar//Descifrar
             var descifrado = SDESDecifrado("1010101100", "1100110111","ruta");
            Productos = JsonConvert.DeserializeObject<Dictionary<string, Producto>>(Raw);
            
        }
        public void CargarSucursales()
        {
            var Raw = new StreamReader("D:\\Pry_ED2\\Sucursales.txt").ReadToEnd();
            //descomprimir 
            var descompreso =HuffmanDescompresion("D:\\Pry_ED2\\Sucursales.txt");
            //Descifrar
            var descifrado = SDESDecifrado("1011011001", "1101101101","ruta");
            Sucursales = JsonConvert.DeserializeObject<Dictionary<string, Sucursal>>(Raw);

        }
        public void CargarRelacion()
        {
            var Raw = new StreamReader("D:\\Pry_ED2\\Relacion.txt").ReadToEnd();
            //descomprimir 
            var descompreso = HuffmanDescompresion("ruta del  original");
            //Descifrar //Descifrar
            var descifrado =  SDESDecifrado("1001101010","0110010101","ruta");
            Relacion = JsonConvert.DeserializeObject<Dictionary<string, Relacion>>(Raw);

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
        public string[] ReturnKeys(string KeyGet)
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
        public string SDESCifrado(string llave1, string llave2,string _path)
        {

            var Original = new FileStream(_path, FileMode.Open);
            var lector = new BinaryReader(Original);
            var buffer = new byte[100000];
            var salida = "";
            while (lector.BaseStream.Position != lector.BaseStream.Length)
            {
                buffer = lector.ReadBytes(100000);
                foreach (var item in buffer)
                {
                    var caracter = (char)item;
                    var bin = Convert.ToString(item, 2).PadLeft(8, '0');
                   salida+=(char)Convert.ToByte(BinarioADecimal(ProcesoSDES(llave1, llave2, bin)));
                    
                }
            }
            Original.Close();
            return salida;
        }
        //decifrado
        public string SDESDecifrado(string llave1, string llave2, string _path)
        {
            var Cifrado = new FileStream(_path, FileMode.Open);
            var lector = new BinaryReader(Cifrado);
            var nombrearchivo = $"{Path.GetFileName(Cifrado.Name).Split('.')[0]}_.{"txt"}";
            var salida = "";
            var buffer = new byte[100000];
            while (lector.BaseStream.Position != lector.BaseStream.Length)
            {
                buffer = lector.ReadBytes(100000);
                foreach (var item in buffer)
                {
                    var bin = Convert.ToString(item, 2).PadLeft(8, '0');
                    var monitor =(char) Convert.ToByte(BinarioADecimal(ProcesoSDES(llave2, llave1, bin)));
                    salida +=monitor;
                }
            }
            Cifrado.Close();
            return salida;
        }

        string ProcesoSDES(string key1, string key2, string actual)
        {
            //1 permutar 8
            var entrada = inicial(actual);

            //2 tomar izquierda y derecha 
            var Mitadizquierda = entrada.Substring(0, 4);
            var MitadDerecha = entrada.Remove(0, 4);

            //3 expandir derecho
            var expandido = Expandir(MitadDerecha);

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
            var segundoexpandido = Expandir(juntosSwaped.Remove(0, 4));
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
        string inicial(string actual)
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

        #region Huffman
      
            Nodo ArbolHuffman = new Nodo();

            const int bufferLenght = 500;
            Dictionary<byte, double> DiccionarioOriginal = new Dictionary<byte, double>();
            Dictionary<byte, string> dicRecorridos = new Dictionary<byte, string>();
            public Dictionary<byte, double> dicOr = new Dictionary<byte, double>();
            Dictionary<string, byte> dicRecorridosDesc = new Dictionary<string, byte>();
            string Remanente = "";

            public void Huffman(string ruta, string rutaCreacion)
            {

                Leer(ruta);
                var TotalDeCaracteres = totalDeCaracteres();
                var CreacionDeNodosYProbabilidad = AgregarNodos(TotalDeCaracteres);
                var Arbol = ArmarArbol(CreacionDeNodosYProbabilidad);
                ArmarDiccionarioDeRecorrido(Arbol[0], "");
                CreacionDeArchivo(TotalDeCaracteres, rutaCreacion);
                LeerArchivoParaComprimir(ruta, rutaCreacion);
                clear();

            }
            public string HuffmanDescompresion(string ruta)
            {
                lecturaDescomprimir(ruta);
                var totalDeCaractres = totalDeCaracteres();
                var CreacionDeNodosYProbabilidad = AgregarNodos(totalDeCaractres);
                var Arbol = ArmarArbol(CreacionDeNodosYProbabilidad);
                ArmarRecorridoInverso(Arbol[0], "");
              return  LeerParaDescomprimir(ruta);

            }

            private void Leer(string Ruta)
            {




                var buffer = new byte[bufferLenght];
                using (var file = new FileStream(Ruta, FileMode.Open))
                {
                    using (var reader = new BinaryReader(file))
                    {
                        while (reader.BaseStream.Position != reader.BaseStream.Length)
                        {
                            buffer = reader.ReadBytes(bufferLenght);
                            foreach (var item in buffer)
                            {
                                ArmarDiccionarios(item);
                            }


                        }
                    }
                }

            }
            private void ArmarDiccionarios(byte item)
            {

                if (DiccionarioOriginal.ContainsKey(item))
                {
                    DiccionarioOriginal[item] += 1;
                }
                else
                {
                    DiccionarioOriginal.Add(item, 1);
                }

            }
            private double totalDeCaracteres()
            {
                double aux = 0;
                foreach (var item in DiccionarioOriginal)
                {
                    aux += item.Value;
                }
                return aux;
            }
            private List<Nodo> AgregarNodos(double TotalDeCaracteres)
            {

                List<Nodo> lista = new List<Nodo>();
                Dictionary<byte, double> auxiliarParaPorcentaje = new Dictionary<byte, double>();
                DiccionarioOriginal = DiccionarioOriginal.OrderBy(x => x.Value).ToDictionary(x => x.Key, x => x.Value);

                //Nodo padre = new Nodo();
                foreach (var item in DiccionarioOriginal)
                {
                    dicOr.Add(item.Key, item.Value);
                    auxiliarParaPorcentaje.Add(item.Key, item.Value / TotalDeCaracteres);
                    Nodo nodo = new Nodo();
                    nodo.Caracter = item.Key;
                    nodo.Probabilidad = (item.Value / TotalDeCaracteres);
                    lista.Add(nodo);
                }
                DiccionarioOriginal.Clear();
                DiccionarioOriginal = auxiliarParaPorcentaje;
                return lista;
            }

            private List<Nodo> ArmarArbol(List<Nodo> lista)
            {
                Nodo tmp;
                while (lista.Count > 1)
                {

                    tmp = new Nodo
                    {
                        NodoIzq = lista[0],
                        NodoDer = lista[1]
                    };
                    tmp.Probabilidad = tmp.NodoIzq.Probabilidad + tmp.NodoDer.Probabilidad;
                    lista.RemoveRange(0, 2);
                    lista.Add(tmp);
                    tmp = null;
                    lista = lista.OrderBy(p => p.Probabilidad).ToList();
                }
                return lista;
            }



            private void ArmarDiccionarioDeRecorrido(Nodo temp, string recorrido)
            {
                if (temp.NodoDer == null && temp.NodoIzq == null)
                {
                    dicRecorridos.Add(temp.Caracter, recorrido);
                }
                else
                {
                    if (temp.NodoDer != null)
                    {
                        ArmarDiccionarioDeRecorrido(temp.NodoDer, recorrido + 1);

                    }
                    if (temp.NodoIzq != null)
                    {
                        ArmarDiccionarioDeRecorrido(temp.NodoIzq, recorrido + 0);
                    }

                }
            }
            private void ArmarRecorridoInverso(Nodo temp, string recorrido)
            {
                if (temp.NodoDer == null && temp.NodoIzq == null)
                {
                    dicRecorridosDesc.Add(recorrido, temp.Caracter);
                }
                else
                {
                    if (temp.NodoDer != null)
                    {
                        ArmarRecorridoInverso(temp.NodoDer, recorrido + 1);

                    }
                    if (temp.NodoIzq != null)
                    {
                        ArmarRecorridoInverso(temp.NodoIzq, recorrido + 0);
                    }

                }
            }

            private void clear()
            {
                dicRecorridos.Clear();
                DiccionarioOriginal.Clear();
                Remanente = "";
                dicOr.Clear();
            }






            private void LeerArchivoParaComprimir(string Ruta, string rutacCreacion)
            {

                string AuxCadenaParaEscribir = "";
                var buffer = new byte[bufferLenght];
                List<string> retorno = new List<string>();
                string cadena;
                string BuscarCadenaExacta = "";

                int BuscarModularExacto = 0;


                using (var file = new FileStream(Ruta, FileMode.Open))
                {
                    using (var reader = new BinaryReader(file))
                    {
                        while (reader.BaseStream.Position != reader.BaseStream.Length)
                        {
                            buffer = reader.ReadBytes(bufferLenght);

                            AuxCadenaParaEscribir = "";
                            foreach (var item in buffer)
                            {

                                AuxCadenaParaEscribir += dicRecorridos[item];

                            }


                            cadena = Remanente + AuxCadenaParaEscribir;
                            if (cadena.Length % 8 == 0)
                            {
                                Remanente = "";
                                CreacionDeArchivoComprimido(cadena, rutacCreacion);


                            }
                            else
                            {
                                Remanente = "";
                                BuscarModularExacto = cadena.Length;
                                int x;
                                while (BuscarModularExacto % 8 != 0)
                                {
                                    BuscarModularExacto -= 1;
                                }
                                BuscarCadenaExacta = cadena.Substring(0, BuscarModularExacto);
                                Remanente = cadena.Substring(BuscarModularExacto, cadena.Length - BuscarModularExacto);
                                CreacionDeArchivoComprimido(BuscarCadenaExacta, rutacCreacion);




                            }




                        }


                    }
                }

            }


            private void CreacionDeArchivo(double CantidadDeCaracteres, string Ruta)
            {

                using (var file = new FileStream(Ruta, FileMode.OpenOrCreate))
                {
                    using (var texto = new StreamWriter(file))
                    {

                        foreach (var item in dicOr)
                        {
                            texto.Write(item.Key + "|" + item.Value + "|");
                        }
                        texto.Write("£");
                    }
                }
            }

            private void CreacionDeArchivoComprimido(string CadenaAComprimir, string Ruta)
            {
                string x;

                using (var file = new FileStream(Ruta, FileMode.Append))
                {

                    using (var writer = new BinaryWriter(file))
                    {

                        for (int i = 0; i < CadenaAComprimir.Length; i += 8)
                        {
                            x = CadenaAComprimir.Substring(i, 8);
                            writer.Write(Convert.ToByte(CadenaAComprimir.Substring(i, 8), 2));


                        }


                    }
                }
            }
            private void lecturaDescomprimir(string Ruta)
            {
                var buffer = new byte[bufferLenght];
                string cadena = "";
                string auxiliar = "";
                bool bandera = false;
                string[] cadenaSplit;
                using (var file = new FileStream(Ruta, FileMode.Open))
                {
                    using (var reader = new BinaryReader(file))
                    {
                        while (reader.BaseStream.Position != reader.BaseStream.Length)
                        {
                            buffer = reader.ReadBytes(bufferLenght);
                            foreach (var item in buffer)
                            {
                                cadena += Convert.ToString(Convert.ToChar(item));
                            }
                            auxiliar += cadena;
                            if (cadena.Contains("£") == true || bandera == false)
                            {

                                bandera = false;
                                if (cadena.Contains("£"))
                                {
                                    cadenaSplit = auxiliar.Split('£');
                                    string[] diccionario;
                                    diccionario = cadenaSplit[0].Split('|');
                                    for (int i = 0; i <= diccionario.Length - 2; i += 2)
                                    {
                                        DiccionarioOriginal.Add(Convert.ToByte(diccionario[i]), Convert.ToDouble(diccionario[i + 1]));
                                    }


                                    auxiliar = "";
                                }

                            }
                        }
                    }
                }
            }
            private string LeerParaDescomprimir(string Ruta)
            {//163
             //194
            var salida = "";
                var buffer = new byte[bufferLenght];
                string cadena = "";
                string conversor = "";
                bool Inicio = false;
                bool bandera = false;
                List<byte> palabra = new List<byte>();
                string aux = "";
                string[] cadenaSplit;
            using (var file = new FileStream(Ruta, FileMode.Open))
            {
                using (var reader = new BinaryReader(file))
                {
                    while (reader.BaseStream.Position != reader.BaseStream.Length)
                    {
                        buffer = reader.ReadBytes(bufferLenght);
                        foreach (var item in buffer)
                        {
                            cadena += Convert.ToString(Convert.ToChar(item));
                        }

                        if (cadena.Contains("£") == true || bandera == false)
                        {

                            bandera = false;
                            if (cadena.Contains("£"))
                            {

                                Inicio = true;
                                cadenaSplit = cadena.Split('£');
                                cadena = cadenaSplit[1];


                            }


                        }
                        if (Inicio == true)
                        {
                            cadena = Remanente + cadena;
                            Remanente = "";
                            string binario = "";


                            foreach (var item in cadena)
                            {

                                conversor = Convert.ToString(Convert.ToByte(item), 2);
                                if (conversor.Length != 8)
                                {
                                    conversor = conversor.PadLeft(8, '0');
                                }
                                binario += conversor;
                            }
                            foreach (var item in binario)
                            {
                                aux += item;
                                if (dicRecorridosDesc.ContainsKey(aux))
                                {
                                    palabra.Add(dicRecorridosDesc[aux]);
                                    aux = "";
                                }


                            }
                            Remanente = aux;
                            salida += palabra;                          
                            palabra.Clear();

                        }
                    }
                }
                return salida;
            }
                
            }
            private void EscrituraDescomprimir(string Ruta, List<byte> cadena)
            {
                using (var file = new FileStream(Ruta, FileMode.Append))
                {

                    using (var writer = new StreamWriter(file))
                    {

                        foreach (var item in cadena)
                        {
                            writer.Write(Convert.ToString(Convert.ToChar(item)));
                        }

                    }
                }
            }
            private void RetornarCaracter(string item, ref string palabra)
            {
                if (dicRecorridosDesc.ContainsKey(item))
                {

                }

            }


        

        #endregion

    }
}
