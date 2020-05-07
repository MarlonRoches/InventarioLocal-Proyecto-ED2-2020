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
            //match exacto Listo
            //cambiar a archivos locales de bin/debug Listo
            Data.x.CrearTablas();
            //Crear sucursal - Marlon Listo
            var EbenEzer = new Sucursal
            {
                Nombre = "EbenEzer",
                Id = 5,
                Direccion= "Zona 5"
            };

            var VerduraDura = new Sucursal
            {
                Nombre = "VerduraDura",
                Id = 15,
                Direccion = "Fraijanes"
            };
            var PolloFeliz = new Sucursal
            {
                Nombre = "Casa De Dios",
                Id = 11,
                Direccion = "Fraijanes"
            };
            var VacaLoca= new Sucursal
            {
                Nombre = "VacaLoca",
                Id = 92,
                Direccion = "Finca la Potra"
            };
            var COVID19= new Sucursal
            {
                Nombre = "COVID19",
                Id = 48,
                Direccion = "Mundo"
            };
            Data.x.AgregarSucursal(EbenEzer);
            Data.x.AgregarSucursal(VerduraDura);
            Data.x.AgregarSucursal(PolloFeliz);
            Data.x.AgregarSucursal(VacaLoca);
            Data.x.AgregarSucursal(COVID19);

            //Agregar Un Producto - Marlon Listo
            var pPepsi = new Producto()
            {
                Id= 852,
                Nombre= "Pepsi",
                Precio = 56.3
            };
            var Carne = new Producto()
            {
                Id= 48,
                Nombre= "Carne",
                Precio = 56.3
            };
            var Pollo = new Producto()
            {
                Id= 618,
                Nombre= "Pollo",
                Precio = 56.3
            };
            var Virus = new Producto()
            {
                Id= 735,
                Nombre= "Virus",
                Precio = 56.3
            };
            Data.x.AgregarProducto(pPepsi);
            Data.x.AgregarProducto(Carne);
            Data.x.AgregarProducto(Pollo);
            Data.x.AgregarProducto(Virus);
            //Crear relacion - Marlon  Listo
            Data.x.AgregarProductoEnSucursal(EbenEzer.Id,pPepsi,20);
            Data.x.AgregarProductoEnSucursal(EbenEzer.Id,Carne,20);
            Data.x.AgregarProductoEnSucursal(EbenEzer.Id,Pollo,20);
            Data.x.AgregarProductoEnSucursal(PolloFeliz.Id,Pollo,6);

            Data.x.Transferir(Pollo.Id.ToString(), EbenEzer.Id.ToString(), PolloFeliz.Id.ToString(), 10);
            // Manejo De INventario: Listo
            //Actualizar Producto Alejandra - nombre y precio
            Data.x.ModificarProducto(pPepsi.Id,"SuperCola", 66.26);
            //Actualizar Relacion Alejandra  Listo-               stock
            Data.x.ModificarRelacion($"{EbenEzer.Id}^{pPepsi.Id}", 5);
            //Actualizar Sucursal Alejandra nomrbre y direccion Listo
            Data.x.ModificarSucursal(EbenEzer.Id, "Nombre Sucursal","Direccion");
            //Transferir Por sucursales Alejandra


            //Agregar varios productos por medio de CSV Alejandra

            //Listados: Alejandra
            //Sucursal
            //Producto
            //sucursal- Producto



            //datos transportables Compresion Lista, falta implementar  -  Marlon


            //documentacion

            //api net core Marlon

            // Interfaz grafica Marlon



        }
    }
}
