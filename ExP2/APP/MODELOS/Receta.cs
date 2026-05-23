using APP.INTERFACES;
namespace APP.MODELOS
{
    public class Receta : IReceta
    {
        /*
        /   Corregimos las propiedaes de lectura y escritura (como en su interfaz)
        /   Añadimos el metodo ToString faltante
        */

        // Atributos y Propiedades
        private readonly string nnombre;    // pr --> solo es modificable detnro de la clase
        private readonly string cchef;      // readonly solo asigna el valor en el constructor
        private readonly int ttiempoMinutos;

        public string Nombre { get { return nnombre; } }    // public cualquiera las puede ver 
        public string Chef { get { return cchef; } }        // set solo eprmite lectura, como la interfaz
        public int TiempoMinutos { get { return ttiempoMinutos; } }

        // Constructor
        public Receta(string nombre, string chef, int tiempoMinutos)
        {
            if (tiempoMinutos <= 0)
                throw new ArgumentException("El tiempo de preparación debe ser mayor a 0.");

            nnombre = nombre;
            cchef = chef;
            ttiempoMinutos = tiempoMinutos;
        }

        // Metodos
        public override string ToString()
        {
            return $"{Nombre} - {Chef} ({TiempoMinutos} min)";
        }
    }
}
