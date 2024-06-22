using System;

public class FuncionesTexto
{
    public void Logo()
    {
        Console.WriteLine(".---------------------------------------------.\r\n|                                             |\r\n|                                             |\r\n|   __  __    _    ______     _______ _       |\r\n|  |  \\/  |  / \\  |  _ \\ \\   / / ____| |      |\r\n|  | |\\/| | / _ \\ | |_) \\ \\ / /|  _| | |      |\r\n|  | |  | |/ ___ \\|  _ < \\ V / | |___| |___   |\r\n|  |_|  |_/_/   \\_\\_| \\_\\ \\_/  |_____|_____|  |\r\n|                                             |\r\n|                                             |\r\n'---------------------------------------------'");
    }

    public void MensajeCampeon()
    {
        Console.WriteLine("   ____     _      __  __    ____   U _____ u U  ___ u  _   _    \r\nU /\"___|U  /\"\\  uU|' \\/ '|uU|  _\"\\ u\\| ___\"|/  \\/\"_ \\/ | \\ |\"|   \r\n\\| | u   \\/ _ \\/ \\| |\\/| |/\\| |_) |/ |  _|\"    | | | |<|  \\| |>  \r\n | |/__  / ___ \\  | |  | |  |  __/   | |___.-,_| |_| |U| |\\  |u  \r\n  \\____|/_/   \\_\\ |_|  |_|  |_|      |_____|\\_)-\\___/  |_| \\_|   \r\n _// \\\\  \\\\    >><<,-,,-.   ||>>_    <<   >>     \\\\    ||   \\\\,-.\r\n(__)(__)(__)  (__)(./  \\.) (__)__)  (__) (__)   (__)   (_\")  (_/ ");
    }

    public void Menu()
    {
        CentrarTexto("+-----------------------+\r\n" +
                          "|     MENU PRINCIPAL    |\r\n" +
                          "|                       |\r\n" +
                          "|  1- Comenzar nuevo    |\r\n" +
                          "|     torneo            |\r\n" +
                          "|                       |\r\n" +
                          "|  2- Combate 1v1       |\r\n" +
                          "|                       |\r\n" +
                          "|  3- Salir             |\r\n" +
                          "+-----------------------+\r\n");

    }

    public void MostrarPersonaje(Personaje personaje, int opcion)
    {
        CentrarTexto($"Opcion: {opcion}");
        CentrarTexto($"───────────────────────────────────────────────── {personaje.DatosPersonaje.Nombre} ─────────────────────────────────────────────────");

        Console.WriteLine(  $"| Descripcion: {personaje.DatosPersonaje.Descripcion}\n" +
                            $"| Salud:    {personaje.CaracteristicasPersonaje.Salud}\n" +
                            $"| Mana:     {personaje.CaracteristicasPersonaje.Mana}\n" +
                            $"| Daño:     {personaje.CaracteristicasPersonaje.Daño}\n" +
                            $"| Defensa:  {personaje.CaracteristicasPersonaje.Defensa}");
        CentrarTexto($"└───────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────┘");

    }

    public void MostrarPersonajes(List<Personaje> ListaPersonajes)
    {
        int i = 1;
        foreach (var personaje in ListaPersonajes)
        {

            MostrarPersonaje(personaje, i);
            Console.WriteLine("\n");
            i++;
        }
    }

    public void CentrarTexto(string texto){
        Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (texto.Length / 2)) + "}", texto));
    }

}
