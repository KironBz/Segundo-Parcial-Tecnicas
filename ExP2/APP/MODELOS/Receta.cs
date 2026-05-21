using APP.INTERFACES;
using System; // Para lanzar excepciones sin .system

namespace APP.MODELOS
{
    public class Receta : IReceta
    {
        // Atributos y Propiedades
        private readonly string _nombre;
        private readonly string _chef;
        private readonly int _tiempoMinutos;

        public string Nombre { get { return _nombre; } }
        public string Chef { get { return _chef; } }
        public int TiempoMinutos { get { return _tiempoMinutos; } }

        // Constructor
        public Receta(string nombre, string chef, int tiempoMinutos)
        {
            if (tiempoMinutos <= 0)
                throw new ArgumentException("El tiempo de preparación debe ser mayor a 0.");

            Nombre = nombre;
            Chef = chef;
            TiempoMinutos = tiempoMinutos;
        }

        // Metodos
        public override string ToString()
        {
            return $"{Nombre} - {Chef} ({TiempoMinutos} min)";
        }
    }
}
