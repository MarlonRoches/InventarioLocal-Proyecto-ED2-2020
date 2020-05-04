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
            //id unico Listo
            //Datos Guardados, cifrados Listo - Alejandra
            //Agregar Sucursal Listo
            //Crear Tablas - Marlon Listo
            Singleton.Ins.CrearTablas();
            //Crear sucursal - Marlon Listo
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
            //Agregar Un Producto - Marlon Listo
            var pPepsi = new Producto()
            {
                Id= 226,
                Nombre= "Pepsi",
                Precio = 56.3
            };
            Singleton.Ins.AgregarProducto(pPepsi);
            //Crear relacion - Marlon  Listo
            Singleton.Ins.AgregarProductoEnSucursal(5,pPepsi);
            Singleton.Ins.AgregarProductoEnSucursal(5,pPepsi);
            Singleton.Ins.AgregarProductoEnSucursal(6,pPepsi);
            Console.ReadKey();



            // Manejo De INventario:
            //Actualizar Producto Alejandra - nombre y precio
            Singleton.Ins.ModificarProducto(pPepsi.Id,"nombre", 66.26);




            //Actualizar Relacion Alejandra -                     stock
            Singleton.Ins.ModificarRelacion("idProducto^idsucursal", 5);

            //Actualizar Sucursal Alejandra nomrbre y direccion
            Singleton.Ins.ModificarSucursal(pPepsi.Id, "Nombre Sucursal","Direccion");

            //Agregar varios productos por medio de CSV Alejandra



            //Actualizar Datos Alejandra
            //Transferir Por sucursales Alejandra
            //Actualizar Inventario De Sucursal Alejandra

            //Listados: Alejandra
            //Sucursal
            //Producto
            //sucursal- Producto
            //


            //match exacto Listo
            //cambiar a archivos locales de bin/debug Listo


            //datos transportables Compresion Pendiente

            //documentacion

            //api net core Marlon

            // Interfaz grafica Marlon



        }
    }
}
