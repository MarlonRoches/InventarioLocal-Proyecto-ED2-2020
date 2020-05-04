using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desarrollo_Proyecto_ED_2
{
    class Arbol_Producto
    {
        public class NodoProducto
        {
            public int Grado { get; set; }
            public int id { get; set; }
            public int Padre { get; set; }
            public int[] Hijos { get; set; }
            public Producto[] Datos { get; set; }
            public bool esHoja { get; set; }

            public NodoProducto(int _grado, bool Tipo)
            {
                if (Tipo)
                {//si es hoja
                    Datos = new Producto[_grado - 1];
                    Hijos = new int[_grado];
                    esHoja = Tipo;
                    Grado = _grado;
                }
                else
                { // es la raix
                    Grado = _grado;
                    var GradRaiz = Convert.ToInt32(1.33333 * (double)(_grado - 1));
                    Datos = new Producto[GradRaiz];
                    Hijos = new int[GradRaiz + 1];
                    esHoja = Tipo;
                }

            }

            public string WriteNodo()
            {
                var devolver = string.Empty;
                devolver += $"{id.ToString().PadLeft(3, '0')}|";
                devolver += $"{Padre.ToString().PadLeft(3, '0')}|";
                for (int i = 0; i < Hijos.Length; i++)
                {
                    devolver += $"{Hijos[i].ToString().PadLeft(5, '0')}|";

                }
                for (int i = 0; i < Datos.Length; i++)
                {
                    devolver += $"{JsonConvert.SerializeObject(Datos[i]).PadLeft(100, '0')}|";

                }
                return devolver;
            }
            public NodoProducto ReadNodo(string NodoSerializado)
            {
                //es sensible al tipo de json que le envie
                var devolver = new NodoProducto(7, false);
                var splited = NodoSerializado.Split('|');
                //padre o no
                if (int.Parse(splited[1]) == 0)
                { // es raiz
                    devolver = new NodoProducto(7, false);
                }
                else
                {//es hoja
                    devolver = new NodoProducto(7, true);
                }
                //poner id
                devolver.id = int.Parse(splited[0]);
                devolver.Padre = int.Parse(splited[1]);
                //poner Datos
                var contador = 2;
                for (int i = 0; i < devolver.Hijos.Length; i++)
                {
                    devolver.Hijos[i] = int.Parse(splited[contador]);
                    contador++;
                }
                //poner hijos
                for (int i = 0; i < devolver.Datos.Length; i++)
                {
                    var des = splited[contador].Replace("0", "");
                    devolver.Datos[i] = JsonConvert.DeserializeObject<Producto>(des);
                    contador++;
                }
                return devolver;
            }


        }
        public class ArbolStar
        {
            static bool primeraSeparecion = false;
            static string GlobalPath { get; set; }
            public int LargoPadre { get; set; }
            public int LargoHijo { get; set; }
            public int IdPAdre { get; set; }
            public int Siguiente { get; set; }
            public static int Grado { get; set; }
            public static NodoProducto Raiz { get; set; }
            //idpadre|grado|siguiente|tamañopadre|tamañohijo

            public ArbolStar(int _grado, string _path)
            {
                var file = new FileStream(_path, FileMode.OpenOrCreate);
                var lector = new StreamReader(file);
                var linea = lector.ReadLine();
                GlobalPath = _path;
                if (linea == null)
                {//nuevo arbol
                    LargoPadre = new NodoProducto(_grado, false).WriteNodo().Length;
                    LargoHijo = new NodoProducto(_grado, true).WriteNodo().Length;
                    IdPAdre = 1;
                    Siguiente = IdPAdre + 1;
                    Grado = _grado;
                    Raiz = new NodoProducto(Grado, false);
                    lector.Close();
                    var escritor = new StreamWriter(GlobalPath);
                    Raiz.id = IdPAdre;
                    Raiz.Grado = _grado;
                    Raiz.esHoja = false;
                    escritor.WriteLine($"{(IdPAdre).ToString().PadLeft(3, '0')}" +
                        $"|{Grado.ToString().PadLeft(3, '0')}|{Siguiente.ToString().PadLeft(3, '0')}" +
                        $"|{LargoPadre.ToString().PadLeft(3, '0')}|{LargoHijo.ToString().PadLeft(3, '0')}|");
                    escritor.WriteLine(Raiz.WriteNodo());
                    escritor.Close();
                }
                else
                {//arbol cargado
                    var aMetaData = linea.Split('|');
                    //0raiz    
                    IdPAdre = int.Parse(aMetaData[0]);
                    //1grado
                    Grado = int.Parse(aMetaData[1]);
                    //2siguiente
                    Siguiente = int.Parse(aMetaData[2]);
                    //3largo padre
                    LargoPadre = int.Parse(aMetaData[3]);
                    //4largohijo
                    LargoHijo = int.Parse(aMetaData[4]);
                    Raiz = new NodoProducto(_grado, false);
                }
                file.Close();
            }
            public void Insertar(Producto Nuevo)
            {
                var FILE = new FileStream(GlobalPath, FileMode.Open);
                var lector = new StreamReader(FILE);
                if (IdPAdre == 1 && !primeraSeparecion) // aun no se parte
                {
                    var linea = lector.ReadLine();
                    linea = lector.ReadLine();
                    FILE.Close();
                    Raiz = Raiz.ReadNodo(linea);
                    var contador = 0;
                    for (int i = 0; i <= Raiz.Datos.Length; i++)
                    {
                        if (i == Raiz.Datos.Length)
                        {

                            PrimeraSeparacion(Raiz, Nuevo);
                            break;
                        }
                        //insertando en la raiz
                        if (Raiz.Datos[i] == null)
                        {
                            Raiz.Datos[contador] = Nuevo;
                            SortDatos(Raiz.Datos);

                            var escritor = new StreamWriter(GlobalPath);
                            escritor.WriteLine($"{(IdPAdre).ToString().PadLeft(3, '0')}" +
                             $"|{Grado.ToString().PadLeft(3, '0')}|{Siguiente.ToString().PadLeft(3, '0')}" +
                             $"|{LargoPadre.ToString().PadLeft(3, '0')}|{LargoHijo.ToString().PadLeft(3, '0')}");
                            escritor.WriteLine(Raiz.WriteNodo());
                            escritor.Close();
                            break;
                        }
                        else
                        {
                            contador++;
                        }
                    }
                }
                else
                {
                    FILE.Close();
                    InsertarEnHoja(SeekPadre(IdPAdre), Nuevo);

                }
            }
            public int Indice(NodoProducto Actual, Producto Nuevo)
            {
                var iActualndice = 0;
                var listacomparar = new List<Producto>();
                foreach (var item in Actual.Datos)
                {
                    if (item != null)
                    {
                        listacomparar.Add(item);
                    }
                }


                for (iActualndice = 0; iActualndice <= listacomparar.Count; iActualndice++)
                {
                    if (iActualndice == listacomparar.Count)
                    {

                        break;

                    }
                    else if (String.Compare(Nuevo.Nombre.ToString(), listacomparar[iActualndice].Nombre.ToString()) == -1)
                    {

                        break;
                    }
                }

                return iActualndice;

            }
            public void SortDatos(Producto[] A_Arreglar)
            {
                var lista = new List<Producto>();
                foreach (var item in A_Arreglar)
                {
                    if (item != null)
                    {
                        lista.Add(item);
                    }
                }
                lista = lista.OrderBy(o => o.Nombre).ToList();
                var contador = 0;
                foreach (var item in lista)
                {
                    A_Arreglar[contador] = item;
                    contador++;
                }
            }
            public void Navegar()
            {

            }
            public void PrimeraSeparacion(NodoProducto Actual, Producto Nuevo)
            {
                var lista = new List<Producto>();
                foreach (var item in Actual.Datos)
                {
                    lista.Add(item);
                }
                lista.Add(Nuevo);

                // escribir en el archivo todos los nodos disponibles del padre e hijos
                var hijo1 = new NodoProducto(Grado, true)
                {
                    Grado = Grado
                };
                hijo1.id = IdPAdre;
                var indice = 0;

                for (int i = 0; i < lista.Count / 2; i++)
                {
                    hijo1.Datos[indice] = lista[i];
                    indice++;
                }


                indice = 0;
                var hijo2 = new NodoProducto(Grado, true);

                hijo2.id = Siguiente;
                Siguiente++;
                for (int i = (lista.Count / 2) + 1; i < lista.Count; i++)
                {
                    hijo2.Datos[indice] = lista[i];
                    indice++;
                }
                var raiznueva = new NodoProducto(Grado, false)
                {
                    id = Siguiente
                };
                lista.Sort((x, y) => x.Nombre.CompareTo(y.Nombre));
                raiznueva.Datos[0] = lista[(lista.Count / 2) + 1];
                IdPAdre = Siguiente;
                raiznueva.Hijos[0] = hijo1.id;
                raiznueva.Hijos[1] = hijo2.id;
                hijo1.Padre = raiznueva.id;
                hijo2.Padre = raiznueva.id;
                Siguiente++;
                SortDatos(raiznueva.Datos);
                SortDatos(hijo1.Datos);
                SortDatos(hijo2.Datos);

                var escritor = new StreamWriter(GlobalPath);
                //metadata
                escritor.WriteLine($"{(IdPAdre).ToString().PadLeft(3, '0')}" +
                $"|{Grado.ToString().PadLeft(3, '0')}|{Siguiente.ToString().PadLeft(3, '0')}" +
                $"|{LargoPadre.ToString().PadLeft(3, '0')}|{LargoHijo.ToString().PadLeft(3, '0')}|");
                // nodos
                escritor.WriteLine(hijo1.WriteNodo());
                escritor.WriteLine(hijo2.WriteNodo());
                escritor.WriteLine(raiznueva.WriteNodo());
                escritor.Close();


            }
            public NodoProducto SeekPadre(int id_padre)
            {
                var file = new FileStream(GlobalPath, FileMode.Open);
                var reader = new StreamReader(file);
                var linea = string.Empty;
                for (int i = 0; i <= id_padre; i++)
                {
                    linea = reader.ReadLine();
                }
                file.Close();
                return new NodoProducto(Grado, false).ReadNodo(linea);
            }
            public NodoProducto SeekHijo(int indicehijo)
            {
                var file = new FileStream(GlobalPath, FileMode.Open);
                var reader = new StreamReader(file);
                var linea = string.Empty;
                for (int i = 0; i <= indicehijo; i++)
                {
                    linea = reader.ReadLine();
                }
                file.Close();
                return new NodoProducto(Grado, true).ReadNodo(linea);
            }
            public void InsertarEnHoja(NodoProducto actual, Producto Nuevo)
            {
                var index = Indice(actual, Nuevo);
                if (actual.Hijos[index] == 0)
                {
                    // se inserta
                    var contador = 0;
                    foreach (var item in actual.Datos)
                    {
                        if (EstaLleno(actual))
                        {
                            CompartirDato(actual, Nuevo);
                            break;
                        }
                        else if (actual.Datos[contador] == null)
                        {
                            actual.Datos[contador] = Nuevo;
                            SortDatos(actual.Datos);
                            EscribirHijo(actual.id, actual);

                            break;
                        }
                        contador++;
                    }
                }
                else
                {
                    //recursivo
                    InsertarEnHoja(SeekHijo(actual.Hijos[index]), Nuevo);

                }



            }
            public void EscribirHijo(int indicehijo, NodoProducto HijoNueo)
            {
                var file = new FileStream(GlobalPath, FileMode.Open);
                var reader = new StreamReader(file);
                var linea = string.Empty;
                var index = file.Position;
                for (int i = 0; i < indicehijo; i++)
                {
                    linea = reader.ReadLine();
                    index += linea.Length + 1;
                }
                file.Position = index + 1;
                int indicearchivo = Convert.ToInt32(index);
                //sobre escribe el hijo
                SortDatos(HijoNueo.Datos);
                file.Write(Encoding.ASCII.GetBytes(HijoNueo.WriteNodo()), 0, (HijoNueo.WriteNodo()).Length);
                file.Close();
            }
            public void EscribirPadre(int _idPadre, NodoProducto PadreNuevo)
            {
                var file = new FileStream(GlobalPath, FileMode.Open);
                var reader = new StreamReader(file);
                var linea = string.Empty;
                var index = file.Position;
                for (int i = 0; i < _idPadre; i++)
                {
                    linea = reader.ReadLine();
                    index += linea.Length + 1;
                }
                file.Position = index + 2;
                int indicearchivo = Convert.ToInt32(index);
                //sobre escribe el hijo
                SortDatos(PadreNuevo.Datos);
                file.Write(Encoding.ASCII.GetBytes(PadreNuevo.WriteNodo()), 0, (PadreNuevo.WriteNodo()).Length);
                file.Close();
            }
            public void CompartirDato(NodoProducto Actual, Producto Nuevo)
            {
                var padre = SeekPadre(Actual.Padre);
                var IndicesHijos = new List<int>();
                var indiceDelHijoACompartir = 0;
                //llenar lista de hijos disponibles
                foreach (var indicehijo in padre.Hijos)
                {
                    if (indicehijo == 0)
                    {
                        break;
                    }
                    else
                    {
                        IndicesHijos.Add(indicehijo);
                    }
                }
                IndicesHijos.ToArray();
                // encuentra el indice en el arreglo de hijos
                foreach (var item in IndicesHijos)
                {
                    if (item == Actual.id)
                    {
                        break;
                    }
                    else
                    {
                        indiceDelHijoACompartir++;
                    }
                }
                // izquierda


                var indiceizquierda = indiceDelHijoACompartir - 1;
                var indiceDerecha = indiceDelHijoACompartir + 1;
                var caminoDerecha = new List<int>();
                var caminoIzquierda = new List<int>();
                var intercambiado = true;
                if (indiceizquierda >= 0 && intercambiado)
                {
                    var lool = IndicesHijos[indiceDerecha];
                    if (!EstaLleno(SeekHijo(lool)))
                    {
                        caminoIzquierda.Add(IndicesHijos[indiceizquierda]);

                        CompartirHaciaLaIzquierda(Actual, caminoIzquierda.ToArray(), Nuevo);
                        intercambiado = false;
                    }
                    else
                    {

                        //Partir Hacia la derecha
                        PartirHaciaLaDerecha(Actual, SeekHijo(lool), Nuevo);
                    }
                }

                if (indiceDerecha <= IndicesHijos.Count && intercambiado)
                {
                    var lool = IndicesHijos[indiceDerecha];
                    if (!EstaLleno(SeekHijo(lool)))
                    {
                        caminoDerecha.Add(IndicesHijos[indiceDerecha]);

                        CompartirHaciaLaDerecha(Actual, caminoDerecha.ToArray(), Nuevo);
                    }
                    else
                    {

                        //Partir Hacia la derecha
                        PartirHaciaLaDerecha(Actual, SeekHijo(lool), Nuevo);
                    }

                }


            }

            public void PartirHaciaLaDerecha(NodoProducto Hijo, NodoProducto Hermano, Producto Nuevo)
            {
                var padre = SeekPadre(Hijo.Padre);
                var NuevoHermano = new NodoProducto(Grado, true)
                {
                    Padre = Hijo.Padre,
                    id = Siguiente
                };

                Siguiente++;
                var lista = new List<Producto>();
                //llenamos la lista
                foreach (var item in padre.Datos)
                {
                    if (item == null)
                    {
                        break;
                    }
                    else
                    {
                        lista.Add(item);
                    }
                }
                foreach (var item in Hijo.Datos)
                {
                    if (item == null)
                    {
                        break;
                    }
                    else
                    {
                        lista.Add(item);
                    }
                }
                foreach (var item in Hermano.Datos)
                {
                    if (item == null)
                    {
                        break;
                    }
                    else
                    {
                        lista.Add(item);
                    }
                }
                lista.Add(Nuevo);
                var array = lista.ToArray();
                SortDatos(array);
                lista = array.ToList<Producto>();
                var minimo = ((2 * Grado) - 1) / 3;
                var listaPadre = new List<Producto>();
                var listaHijo = new List<Producto>();
                var listaHermano = new List<Producto>();
                var listaNuevo = new List<Producto>();
                for (int i = 0; i < minimo; i++)
                {
                    listaHijo.Add(lista[0]);
                    lista.Remove(lista[0]);
                }
                listaPadre.Add(lista[0]);
                lista.Remove(lista[0]);
                for (int i = 0; i < minimo; i++)
                {
                    listaHermano.Add(lista[0]);
                    lista.Remove(lista[0]);
                }
                listaPadre.Add(lista[0]);
                lista.Remove(lista[0]);
                for (int i = 0; i < minimo; i++)
                {
                    listaNuevo.Add(lista[0]);
                    lista.Remove(lista[0]);
                }
                for (int i = 0; i < Hijo.Datos.Length; i++)
                {
                    if (listaHijo.Count != 0)
                    {
                        Hijo.Datos[i] = listaHijo[0]; listaHijo.Remove(listaHijo[0]);
                    }
                    else
                    {
                        Hijo.Datos[i] = null;
                    }
                }
                for (int i = 0; i < Hermano.Datos.Length; i++)
                {
                    if (listaHermano.Count != 0)
                    {
                        Hermano.Datos[i] = listaHermano[0]; listaHermano.Remove(listaHermano[0]);
                    }
                    else
                    {
                        Hermano.Datos[i] = null;
                    }
                }
                for (int i = 0; i < NuevoHermano.Datos.Length; i++)
                {
                    if (listaNuevo.Count != 0)
                    {
                        NuevoHermano.Datos[i] = listaNuevo[0]; listaNuevo.Remove(listaNuevo[0]);
                    }
                    else
                    {
                        NuevoHermano.Datos[i] = null;
                    }
                }
                for (int i = 0; i < padre.Datos.Length; i++)
                {
                    if (listaPadre.Count != 0)
                    {
                        padre.Datos[i] = listaPadre[0]; listaPadre.Remove(listaPadre[0]);
                    }
                    else
                    {
                        padre.Datos[i] = null;
                    }
                }
                for (int i = 0; i < padre.Hijos.Length; i++)
                {
                    if (padre.Hijos[i] == 0)
                    {
                        padre.Hijos[i] = NuevoHermano.id;
                        break;
                    }
                }
                EscribirMetaData();
                EscribirPadre(padre.id, padre);
                EscribirHijo(Hijo.id, Hijo);
                EscribirHijo(Hermano.id, Hermano);
                EscribirHijo(NuevoHermano.id, NuevoHermano);
            }
            public void CompartirHaciaLaDerecha(NodoProducto Sharing, int[] Camino, Producto Nuevo)
            {
                var sube = Sharing.Datos[Sharing.Datos.Length - 1];
                Sharing.Datos[Sharing.Datos.Length - 1] = Nuevo;
                SortDatos(Sharing.Datos);

                EscribirHijo(Sharing.id, Sharing);

                var padre = SeekPadre(Sharing.Padre);
                var baja = padre.Datos[Indice(padre, Nuevo)];
                padre.Datos[Indice(padre, Nuevo)] = sube;
                SortDatos(padre.Datos);
                EscribirPadre(padre.id, padre);

                var Shared = SeekHijo(Camino[0]);

                if (!EstaLleno(Shared))
                {
                    for (int i = 0; i < Shared.Datos.Length; i++)
                    {
                        if (Shared.Datos[i] == null)
                        {
                            Shared.Datos[i] = baja;
                            SortDatos(Shared.Datos);
                            EscribirHijo(Shared.id, Shared);
                            break;
                        }
                        // si quedan por mover, recursivo
                    }
                }
            }

            public void CompartirHaciaLaIzquierda(NodoProducto Sharing, int[] IndiceHermano, Producto Nuevo)
            {

                var padre = SeekPadre(Sharing.Padre);
                var HermanoIzquierdo = SeekHijo(IndiceHermano[0]);

                var listaactual = Sharing.Datos.ToList<Producto>();
                listaactual.Add(Nuevo);
                listaactual.Sort((x, y) => x.Nombre.CompareTo(y.Nombre));
                var datoBaja = padre.Datos[Indice(padre, Nuevo) - 1];
                var sube = listaactual[0];
                listaactual.Remove(listaactual[0]);
                Sharing.Datos = listaactual.ToArray();
                padre.Datos[Indice(padre, Nuevo) - 1] = sube;
                for (int i = 0; i < HermanoIzquierdo.Datos.Length; i++)
                {
                    if (HermanoIzquierdo.Datos[i] == null)
                    {
                        HermanoIzquierdo.Datos[i] = datoBaja;
                        break;
                    }
                }
                EscribirHijo(Sharing.id, Sharing);
                EscribirHijo(HermanoIzquierdo.id, HermanoIzquierdo);
                EscribirPadre(padre.id, padre);
            }
            public bool EstaLleno(NodoProducto Actual)
            {
                for (int i = 0; i < Actual.Grado - 1; i++)
                {
                    if (Actual.Datos[i] == null)
                    {
                        return false;
                    }
                }
                SortDatos(Actual.Datos);

                return true;
            }
            public int PuedeRecibir(NodoProducto Prestamista)
            {
                var datos = new List<Producto>();
                for (int i = 0; i < Prestamista.Grado - 1; i++)
                {
                    if (Prestamista.Datos[i] != null)
                    {
                        datos.Add(Prestamista.Datos[i]);
                    }
                }
                if (datos.Count > ((2 * Grado) - 1) / 3)
                {
                    return Prestamista.id;

                }
                else
                {

                    return 99;
                }

            }
            public void EscribirMetaData()
            {
                var meta = $"{(IdPAdre).ToString().PadLeft(3, '0')}" +
                             $"|{Grado.ToString().PadLeft(3, '0')}|{Siguiente.ToString().PadLeft(3, '0')}" +
                             $"|{LargoPadre.ToString().PadLeft(3, '0')}|{LargoHijo.ToString().PadLeft(3, '0')}";
                var escritor = new FileStream(GlobalPath, FileMode.Open);
                escritor.Write(Encoding.ASCII.GetBytes(meta), 0, (meta).Length);
                escritor.Close();
            }
        }
    }
}
