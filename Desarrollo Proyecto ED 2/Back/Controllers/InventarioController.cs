using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Desarrollo_Proyecto_ED_2;
using Newtonsoft.Json;
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
            var arrar = JsonConvert.DeserializeObject<string[]>(json.ToString());
            try
            {

                Data.x.ModificarRelacion(arrar[0], int.Parse(arrar[1]));
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
            var arrar = JsonConvert.DeserializeObject<string[]>(json.ToString());
            try
            {

                Data.x.ModificarSucursal(int.Parse(arrar[0]), arrar[1], arrar[2]);
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
            var arrar = JsonConvert.DeserializeObject<string[]>(json.ToString());
            try
            {

                Data.x.ModificarProducto(int.Parse(arrar[0]), arrar[1], double.Parse(arrar[2]));
                return Ok("Exito");
            }
            catch (Exception)
            {
                return BadRequest("Error ");
            }
        }

        [HttpPost("LeerCSV")]
        public ObjectResult LeerCSV([FromBody]string Ruta)
        {
            try
            {

                Data.x.LeerCSV(Ruta);
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



    }
}