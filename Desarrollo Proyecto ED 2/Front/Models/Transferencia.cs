using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Front.Models
{
    public class Transferencia
    {

        public string idProducto { get; set; }
        public string idEmisior           { get; set; }
        public string idReceptor          { get; set; }
        public int cantidadDeTransferencia{ get; set; }
    }
}