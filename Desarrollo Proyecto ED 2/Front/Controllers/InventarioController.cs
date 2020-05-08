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

namespace Front.Controllers
{
    public class InventarioController : Controller
    {
        HttpClient ClienteHttp = new HttpClient();

        // GET: Inventario
        public async Task<ActionResult> Index()
        {
            var cliente = new HttpClient();
            var respose = await cliente.GetAsync("https://localhost:44383/Inventario/Load");

            return View();
        }

        // GET: Inventario/Create
        public ActionResult AgregarSucursal()
        {

            return View();
        }

        // POST: Inventario/Create
        [HttpPost]
        public async Task<ActionResult> AgregarSucursal(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                var nuevo = new Sucursal()
                {
                    Id = int.Parse(collection["Id"]),
                    Direccion= (collection["Direccion"]),
                    Nombre=    (collection["Nombre"])
                    

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

        // GET: Inventario/Create
        public ActionResult AgregarProducto()
        {
            return View();
        }
        // POST: Inventario/Create
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
        // GET: Inventario/Create

        public ActionResult AgregarProductoEnSucursal()
        {
            return View();
        }
        // POST: Inventario/Create
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

        // GET: Inventario/Edit/5
        public ActionResult ModificarRelacion()
        {
            return View();
        }

        // POST: Inventario/Edit/5
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
                var respose = await cliente.PostAsync("https://localhost:44383/Inventario/AgregarRelacion", content);
                var ol = respose.Content.ReadAsStringAsync();

                return RedirectToAction("ListaDeRelaciones");
            }
            catch
            {
                return View();
            }
        }

        // GET: Inventario/Edit/5
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
                var respose = await cliente.PostAsync("https://localhost:44383/Inventario/AgregarSucursal", content);
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
        public ActionResult ModificarProducto(FormCollection collection)
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
        public ActionResult LeerCSV()
        {
            return View();
        }
    }
}
