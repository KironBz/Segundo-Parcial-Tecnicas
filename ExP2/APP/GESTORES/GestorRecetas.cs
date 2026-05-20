using APP.INTERFACES;
using APP.MODELOS;

namespace APP.GESTORES
{
    public class GestorRecetas : IGestorRecetas
    {
        // Propiedades
        public new List<Receta> RecetasDisponibles { get; set; }

        // Constructor
        public GestorRecetas()
        {
            RecetasDisponibles = new List<Receta>();
        }

        // Metodos 
        public void AgregarReceta(Receta receta)
        {
            if (receta == null) throw new ArgumentNullException(nameof(receta));

            // Evita duplicados por nombre
            if (RecetasDisponibles.Any(r => r.Nombre.Equals(receta.Nombre, StringComparison.OrdinalIgnoreCase)))
            {
                throw new InvalidOperationException("La receta ya existe en el catálogo global.");
            }

            RecetasDisponibles.Add(receta);
        }

        public void EliminarReceta(string nombre)
        {
            var receta = RecetasDisponibles.FirstOrDefault(r => r.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase));
            if (receta != null)
            {
                RecetasDisponibles.Remove(receta);
            }
        }

        public void EliminarPorIndice(int indice)
        {
            /*
            if (indice < 0 || indice >= RecetasDisponibles.Count)
                throw new IndexOutOfRangeException("Índice fuera de los límites del catálogo.");
            */

            RecetasDisponibles.RemoveAt(indice);
        }

        public List<Receta> BusquedaPorNombre(string nombre)
        {
            return RecetasDisponibles.Where(r => r.Nombre.Contains(nombre, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public void LimpiarCatalogo()
        {
            RecetasDisponibles.Clear();
        }

        // Algoritmos
        // 1ero - QuiclSort  -  Segun el tiempo se ordena de forma ascendente



        // 2do - Regresa la lista ordenada?


        // 3ro
        public int BusquedaBinaria(string nombre)
        {
            // Copia por nombre (sin modificar el original)
            var copiaOrdenada = RecetasDisponibles.OrderBy(r => r.Nombre.ToLowerInvariant()).ToList();

            int bajo = 0, alto = copiaOrdenada.Count - 1;
            while (bajo <= alto)
            {
                int medio = (bajo + alto) / 2;
                int comparacion = string.Compare(copiaOrdenada[medio].Nombre, nombre, StringComparison.OrdinalIgnoreCase);
                if (comparacion == 0) return medio;  // retorna índice en la copia
                if (comparacion < 0) bajo = medio + 1;
                else alto = medio - 1;
            }
            return -1;
        }
    }
}