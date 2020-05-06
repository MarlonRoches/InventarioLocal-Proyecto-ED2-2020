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
            Data.x.CrearTablas();
            //Crear sucursal - Marlon Listo
            var sucur = new Sucursal
            {
                Nombre = "EbenEzer",
                Id = 5,
                Direccion= "Zona 5"
            };
            Data.x.AgregarSucursal(sucur);
            Data.x.AgregarSucursal(sucur);
            var sucur2 = new Sucursal
            {
                Nombre = "Casa De Dios",
                Id = 9,
                Direccion = "Fraijanes"
            };
            Data.x.AgregarSucursal(sucur2);
            //Agregar Un Producto - Marlon Listo
            var pPepsi = new Producto()
            {
                Id= 226,
                Nombre= "Pepsi",
                Precio = 56.3
            };
            Data.x.AgregarProducto(pPepsi);
            //Crear relacion - Marlon  Listo
            Data.x.AgregarProductoEnSucursal(5,pPepsi);
            Data.x.AgregarProductoEnSucursal(5,pPepsi);
            Data.x.AgregarProductoEnSucursal(9,pPepsi);
    



            // Manejo De INventario: Listo
            //Actualizar Producto Alejandra - nombre y precio
            Data.x.ModificarProducto(pPepsi.Id,"SuperCola", 66.26);


            //Actualizar Relacion Alejandra  Listo-               stock
            Data.x.ModificarRelacion($"{sucur.Id}^{pPepsi.Id}", 5);

            //Actualizar Sucursal Alejandra nomrbre y direccion Listo
            Data.x.ModificarSucursal(sucur.Id, "Nombre Sucursal","Direccion");



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
