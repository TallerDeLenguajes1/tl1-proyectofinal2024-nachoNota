using System;
namespace MovimientosCombate
{
    public class Movimientos
    {
        private string nombre { get; set; }
        private string descripcion { get; set; }
        private string categoria { get; set; }
        private int costoMana { get; set; }
        private int cantidadAumento { get; set; }

        public string Nombre { get => nombre; set => nombre = value; }
        public string Descripcion { get => descripcion; set => descripcion = value; }
        public string Categoria { get => categoria; set => categoria = value; }
        public int CostoMana { get => costoMana; set => costoMana = value; }
        public int CantidadAumento { get => cantidadAumento; set => cantidadAumento = value; }

        public Movimientos(string nombre, string descripcion, string categoria, int cantidadAumento, int costoMana)
        {
            this.nombre = nombre;
            this.descripcion = descripcion;
            this.categoria = categoria;
            this.cantidadAumento = cantidadAumento;
            this.costoMana = costoMana;
        }
    }
}