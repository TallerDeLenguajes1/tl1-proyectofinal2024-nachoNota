using System;
namespace MovimientosCombate
{
    public class Movimientos
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Categoria { get; set; }
        public int CostoMana { get; set; }
        public int CantidadAumento { get; set; }

        public Movimientos(string nombre, string descripcion, string categoria, int cantidadAumento, int costoMana)
        {
            Nombre = nombre;
            Descripcion = descripcion;
            Categoria = categoria;
            CantidadAumento = cantidadAumento;
            CostoMana = costoMana;
        }
    }
}