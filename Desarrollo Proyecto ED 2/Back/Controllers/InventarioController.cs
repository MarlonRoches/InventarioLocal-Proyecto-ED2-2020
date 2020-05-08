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
    [Route("Manejo/[controller]")]
    [ApiController]
    public class InventarioController : ControllerBase
    {
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
            var arrar = JsonConvert.DeserializeObject<int[]>(json.ToString());
            try
            {

                Data.x.AgregarProductoEnSucursal(arrar[0],arrar[1],arrar[2]);
                return Ok("Exito");
            }
            catch (Exception)
            {

                return BadRequest("Error ");

            }
        }
    }
}