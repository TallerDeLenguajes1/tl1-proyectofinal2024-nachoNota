using System;

namespace DatosPersonaje
{
	public class Datos
	{
		private string nombre { get; set; }
		private string descripcion { get; set; }
        private DateTime fechaCampeon { get; set; }

        public string Nombre { get => nombre; set => nombre = value; }
		public string Descripcion { get => descripcion; set => descripcion = value; }
        public DateTime FechaCampeon { get => fechaCampeon; set => fechaCampeon = value; }

        public Datos(string nombre, string descripcion){
			this.nombre = nombre;
			this.descripcion = descripcion;
		}

	}
}
