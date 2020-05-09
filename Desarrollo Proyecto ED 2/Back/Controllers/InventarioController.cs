using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Desarrollo_Proyecto_ED_2;
using Newtonsoft.Json;
using Front.Models;
namespace Back.Controllers

{
    [Route("[controller]")]
    [ApiController]
    public class InventarioController : ControllerBase
    {
        [HttpGet ("Load")]
        public string Inicio()
        {
            Data.x.CrearTablas();
            return "Ok";
        }
       [HttpPost ("AgregarSucursal")]
       public ObjectResult AgregarSucursal([FromBody]object json)
       {
            try
            {
            Data.x.AgregarSucursal(JsonConvert.DeserializeObject<Sucursal>(json.ToString()));
                return Ok("Exito");
            }
            catch (Exception)
            {

                return BadRequest("Error ");
                
            }
       }
        [HttpPost("AgregarProducto")]
        public ObjectResult AgregarProducto([FromBody]object json)
        {
            try
            {
                Data.x.AgregarProducto(JsonConvert.DeserializeObject<Producto>(json.ToString()));
                return Ok("Exito");
            }
            catch (Exception)
            {

                return BadRequest("Error ");

            }
        }
        [HttpPost("AgregarRelacion")]
        public ObjectResult AgregarRelacion([FromBody]object json)
        {
            var arrar = JsonConvert.DeserializeObject<Relacion>(json.ToString());
            try
            {

                Data.x.AgregarProductoEnSucursal(arrar.Id_Sucursal,arrar.Id_Producto,arrar.Stock);
                return Ok("Exito");
            }
            catch (Exception)
            {

                return BadRequest("Error ");

            }
        }

        [HttpPost("ModificarRelacion")]
        public ObjectResult ModificarRelacion([FromBody]object json)
        {
            var RelacionDeEntrada = JsonConvert.DeserializeObject<Relacion>(json.ToString());
            try
            {

                Data.x.ModificarRelacion($"{RelacionDeEntrada.Id_Sucursal}^{RelacionDeEntrada.Id_Producto}", RelacionDeEntrada.Stock);
                return Ok("Exito");
            }
            catch (Exception)
            {

                return BadRequest("Error ");

            }
        }
        [HttpPost("ModoficarSucursal")]
        public ObjectResult ModoficarSucursal([FromBody]object json)
        {
            var ObjetoDeModificacion = JsonConvert.DeserializeObject<Sucursal>(json.ToString());
            try
            {

                Data.x.ModificarSucursal(ObjetoDeModificacion.Id, ObjetoDeModificacion.Nombre, ObjetoDeModificacion.Direccion);
                return Ok("Exito");
            }
            catch (Exception)
            {

                return BadRequest("Error ");

            }
        }

        [HttpPost("ModificarProducto")]
        public ObjectResult ModificarProducto([FromBody]object json)
        {
            var arrar = JsonConvert.DeserializeObject<Producto>(json.ToString());
            try
            {

                Data.x.ModificarProducto(arrar.Id, arrar.Nombre, arrar.Precio);
                return Ok("Exito");
            }
            catch (Exception)
            {
                return BadRequest("Error ");
            }
        }

        [HttpPost("LeerCSV")]
        public ObjectResult LeerCSV([FromBody]object Ruta)
        {
            try
            {

                Data.x.LeerCSV(JsonConvert.DeserializeObject<Input>(Ruta.ToString()).Ruta);
                return Ok("Exito");
            }
            catch (Exception)
            {
                return BadRequest("Error ");
            }
        }
        [HttpPost("TransferirProductos")]
        public ObjectResult TransferirProductos([FromBody]object Json)
        {
            var Datos = JsonConvert.DeserializeObject<Transferencia>(Json.ToString());
            try
            {

                Data.x.Transferir(Datos.idProducto, Datos.idEmisior, Datos.idReceptor, Datos.cantidadDeTransferencia);
                return Ok("Exito");
            }
            catch (Exception)
            {
                return BadRequest("Error ");
            }
        }

        [HttpGet("ListaDeRelaciones")]
        public string ListaDeRelaciones()
        {
            return JsonConvert.SerializeObject(Data.x.ListaDeRelaciones());

        }

        [HttpGet("ListaDeProductos")]
        public string ListaDeProductos()
        {
            return JsonConvert.SerializeObject(Data.x.ListaDeProductos());

        }

        [HttpGet("ListaDeSucursales")]
        public string ListaDeSucursales()
        {
            return JsonConvert.SerializeObject(Data.x.ListaDeSucursales());

        }


        [HttpGet("ComprimirDatos")]
        public string ComprimirDatos([FromBody]object Json)
        {
            var entrada = JsonConvert.DeserializeObject<Comp>(Json.ToString());

            if (entrada.CifrarProductos)
            {

            }

            if (entrada.CifrarSucursales)
            {

            }

            if (entrada.CifrarRelaciones)
            {

            }
            return JsonConvert.SerializeObject(Data.x.ListaDeSucursales());
        }


    }
}