using System;

public class Datos
{
	private string nombre { get; set; }
	private string descripcion { get; set; }

	public string Nombre { get => nombre; set => nombre = value; }
    public string Descripcion { get => descripcion; set => descripcion = value; }

	public Datos(string nombre, string descripcion){
		this.nombre = nombre;
		this.descripcion = descripcion;
	}

}

