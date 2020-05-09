using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Desarrollo_Proyecto_ED_2;
using Newtonsoft.Json;
using Front.Models;
namespace Front.Controllers
{
    public class InventarioController : Controller
    {
        // GET: Inventario
        public async Task<ActionResult> Index()
        {
            var cliente = new HttpClient();
            var respose = await cliente.GetAsync("https://localhost:44383/Inventario/Load");

            return View();
        }



        public async Task<ActionResult> BorrarProducto(int id)
        {
            var nuevo = new Producto()
            {
                Id = id
            };
            var json = JsonConvert.SerializeObject(nuevo);
            var cliente = new HttpClient();

            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var respose = await cliente.PutAsync("https://localhost:44383/Inventario/BorrarProducto", content);
            var ol = respose.Content.ReadAsStringAsync();

            return RedirectToAction("ListaDeProductos");
        }

        public async Task<ActionResult> BorrarSucursal(int id)
        {
            var nuevo = new Sucursal()
            {
                Id = id
            };
            var json = JsonConvert.SerializeObject(nuevo);
            var cliente = new HttpClient();

            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var respose = await cliente.PutAsync("https://localhost:44383/Inventario/BorrarSucursal", content);

            return RedirectToAction("ListaDeSucursales");
        }

        public async Task<ActionResult> BorrarRelacion(int idSucursal, int idProducto)
        {
            var nuevo = new Relacion()
            {
                Id_Producto = idProducto,
                Id_Sucursal = idSucursal
            };
            var json = JsonConvert.SerializeObject(nuevo);
            var cliente = new HttpClient();

            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var respose = await cliente.PutAsync("https://localhost:44383/Inventario/BorrarRelacion", content);

            return RedirectToAction("ListaDeRelaciones");
        }

        public ActionResult AgregarSucursal()
        {

            return View();
        }
        [HttpPost]
        public async Task<ActionResult> AgregarSucursal(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                var nuevo = new Sucursal()
                {
                    Id = int.Parse(collection["Id"]),
                    Direccion = (collection["Direccion"]),
                    Nombre = (collection["Nombre"])


                };
                var json = JsonConvert.SerializeObject(nuevo);
                var cliente = new HttpClient();

                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var respose = await cliente.PostAsync("https://localhost:44383/Inventario/AgregarSucursal", content);
                var ol = respose.Content.ReadAsStringAsync();

                return RedirectToAction("ListaDeSucursales");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult AgregarProducto()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> AgregarProducto(FormCollection collection)
        {
            try
            {
                var nuevo = new Producto()
                {
                    Id = int.Parse(collection["Id"]),
                    Precio = double.Parse(collection["Precio"]),
                    Nombre = (collection["Nombre"])


                };
                var json = JsonConvert.SerializeObject(nuevo);
                var cliente = new HttpClient();

                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var respose = await cliente.PostAsync("https://localhost:44383/Inventario/AgregarProducto", content);
                var ol = respose.Content.ReadAsStringAsync();

                return RedirectToAction("ListaDeProductos");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult AgregarProductoEnSucursal()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> AgregarProductoEnSucursal(FormCollection collection)
        {
            try
            {
                var nuevo = new Relacion()
                {
                    Stock = int.Parse(collection["Stock"]),
                    Id_Producto = int.Parse(collection["Id_Producto"]),
                    Id_Sucursal = int.Parse(collection["Id_Sucursal"])


                };
                var json = JsonConvert.SerializeObject(nuevo);
                var cliente = new HttpClient();

                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var respose = await cliente.PostAsync("https://localhost:44383/Inventario/AgregarRelacion", content);
                var ol = respose.Content.ReadAsStringAsync();

                return RedirectToAction("ListaDeRelaciones");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult ModificarRelacion()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> ModificarRelacion(FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                var nuevo = new Relacion()
                {
                    Stock = int.Parse(collection["Stock"]),
                    Id_Producto = int.Parse(collection["Id_Producto"]),
                    Id_Sucursal = int.Parse(collection["Id_Sucursal"])


                };
                var json = JsonConvert.SerializeObject(nuevo);
                var cliente = new HttpClient();

                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var respose = await cliente.PutAsync("https://localhost:44383/Inventario/ModificarRelacion", content);
                var ol = respose.Content.ReadAsStringAsync();

                return RedirectToAction("ListaDeRelaciones");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult ModificarSucursal()
        {
            return View();
        }

        // POST: Inventario/Edit/5
        [HttpPost]
        public async Task<ActionResult> ModificarSucursal(FormCollection collection)
        {
            try
            {
                var nuevo = new Sucursal()
                {
                    Id = int.Parse(collection["Id"]),
                    Direccion = (collection["Direccion"]),
                    Nombre = (collection["Nombre"])


                };
                var json = JsonConvert.SerializeObject(nuevo);
                var cliente = new HttpClient();

                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var respose = await cliente.PutAsync("https://localhost:44383/Inventario/ModoficarSucursal", content);
                var ol = respose.Content.ReadAsStringAsync();

                return RedirectToAction("ListaDeSucursales");
            }
            catch
            {
                return View();
            }
        }

        // GET: Inventario/Edit/5
        public ActionResult ModificarProducto()
        {
            return View();
        }

        // POST: Inventario/Edit/5
        [HttpPost]
        public async Task<ActionResult> ModificarProducto(FormCollection collection)
        {
            try
            {
                var nuevo = new Producto()
                {
                    Id = int.Parse(collection["Id"]),
                    Precio = double.Parse(collection["Precio"]),
                    Nombre = (collection["Nombre"])


                };
                var json = JsonConvert.SerializeObject(nuevo);
                var cliente = new HttpClient();

                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var respose = await cliente.PutAsync("https://localhost:44383/Inventario/ModificarProducto", content);
                var ol = respose.Content.ReadAsStringAsync();

                return RedirectToAction("ListaDeProductos");
            }
            catch
            {
                return View();
            }
        }


        public async Task<ActionResult> ListaDeRelaciones()
        {
            var cliente = new HttpClient();
            var respose = await cliente.GetAsync("https://localhost:44383/Inventario/ListaDeRelaciones");
            respose.EnsureSuccessStatusCode();
            string responseBody = await respose.Content.ReadAsStringAsync();

            var lista = JsonConvert.DeserializeObject<List<Relacion>>(responseBody); return View(lista);

        }

        public async Task<ActionResult> ListaDeProductos()
        {
            var cliente = new HttpClient();
            var respose = await cliente.GetAsync("https://localhost:44383/Inventario/ListaDeProductos");
            respose.EnsureSuccessStatusCode();
            string responseBody = await respose.Content.ReadAsStringAsync();

            var lista = JsonConvert.DeserializeObject<List<Producto>>(responseBody);
            return View(lista);

        }

        public async Task<ActionResult> ListaDeSucursales()
        {
            var cliente = new HttpClient();
            var respose = await cliente.GetAsync("https://localhost:44383/Inventario/ListaDeSucursales");
            respose.EnsureSuccessStatusCode();
            string responseBody = await respose.Content.ReadAsStringAsync();

            var lista = JsonConvert.DeserializeObject<List<Sucursal>>(responseBody);
            return View(lista);
        }

        public ActionResult Transferir()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Transferir(FormCollection collection)
        {

            var nuevo = new Transferencia()
            {
                idProducto = collection["idProducto"],
                idEmisior = collection["idEmisior"],
                idReceptor = collection["idReceptor"],
                cantidadDeTransferencia = int.Parse(collection["cantidadDeTransferencia"])
            };
            // TODO: Add update logic here
            var json = JsonConvert.SerializeObject(nuevo);
            var cliente = new HttpClient();

            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var respose = await cliente.PostAsync("https://localhost:44383/Inventario/TransferirProductos", content);

            return RedirectToAction("ListaDeRelaciones");

        }

        public ActionResult Importar_CSV()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Importar_CSV(FormCollection collection)
        {
            var Nuevo = new Input()
            {
                Ruta = collection["Ruta"]
            };
            var cliente = new HttpClient();
            var json = JsonConvert.SerializeObject(Nuevo);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var respose = await cliente.PostAsync("https://localhost:44383/Inventario/LeerCSV", content);

            return RedirectToAction("ListaDeProductos");
        }
    }
}
