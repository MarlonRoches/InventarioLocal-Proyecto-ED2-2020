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
        public ActionResult Index()
        {
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


                var cliente = new HttpClient();
                var json = JsonConvert.SerializeObject(new object());

                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var respose = await cliente.PostAsync("https://localhost:44383/api/Cuenta/Crear", content);
                return RedirectToAction("Index");
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
        public ActionResult AgregarProducto(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
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
        public ActionResult AgregarProductoEnSucursal(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
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
        public ActionResult ModificarRelacion(FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
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
        public ActionResult ModificarSucursal(FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
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
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        public ActionResult ListaDeRelaciones()
        {
            return View(new List<Relacion>());
        }
        
        public ActionResult ListaDeProductos()
        {
            return View(new List<Producto>());
        }
        
        public ActionResult ListaDeSucursales()
        {
            return View(new List<Sucursal>());
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
