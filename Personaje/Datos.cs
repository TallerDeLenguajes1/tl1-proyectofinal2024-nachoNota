using System;

namespace DatosPersonaje
{
	public class Datos
	{
        public string Nombre { get; set; }
		public string Descripcion { get; set; }
        public DateTime FechaCampeon { get; set; }

        public Datos(string nombre, string descripcion){
			Nombre = nombre;
			Descripcion = descripcion;
		}

	}
}
