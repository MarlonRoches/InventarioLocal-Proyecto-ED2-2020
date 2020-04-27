using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desarrollo_Proyecto_ED_2
{
    class Program
    {
        static void Main(string[] args)
        {
            //Datos Guardados, cifrados PENDIENTE
            
                //Agregar Sucursal
            Singleton.Ins.CrearTablas();
            var sucur = new Sucursal
            {
                Nombre = "Hola",
                Id = 5,
                Direccion= "Casita"
            };
            Singleton.Ins.AgregarSucursal(sucur);
            Singleton.Ins.AgregarSucursal(sucur);
            var sucur2 = new Sucursal
            {
                Nombre = "Hola",
                Id = 6,
                Direccion = "Casita"
            };
            Singleton.Ins.AgregarSucursal(sucur2);

            //Agregar Un Producto
            var pPepsi = new Producto()
            {
                Id= 226,
                Nombre= "Pepsi",
                Precio = 56.3
            };
            Singleton.Ins.AgregarProducto(pPepsi);
            
            Singleton.Ins.AgregarProductoEnSucursal(5,pPepsi);


            Singleton.Ins.AgregarProductoEnSucursal(5,pPepsi);
            Singleton.Ins.AgregarProductoEnSucursal(6,pPepsi);

            Console.ReadKey();
            //match exacto Listo
            
            //id unico Listo

            //datos transportables Pendiente
            //Arbol Compreso
            // documentacion
            //api net core
            // Interfaz grafica

            // Manejo De INventario:
            
            //Actualizar Datos
            //Agregar varios productos por medio de CSV
            //Actualizar Datos
            //Transferir Por sucursales
            //Actualizar Inventario De Sucursal

            //Listados:
            //Sucursal
            //Producto
            //sucursal- Producto
            //
        }
    }
}
