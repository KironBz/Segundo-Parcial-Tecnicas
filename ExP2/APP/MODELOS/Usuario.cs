namespace APP.MODELOS
{
    public class Usuario
    {
        // Propiedades
        public string Nombre { get; set; }
        public Dictionary<string, List<Receta>> LibrosRecetas { get; set; }

        // Constructor
        public Usuario(string nombre)
        {
            Nombre = nombre;
            LibrosRecetas = new Dictionary<string, List<Receta>>();
        }

        // Metodos
        public void CrearLibroRecetas(string nombreLibro)
        {
            if (LibrosRecetas.ContainsKey(nombreLibro))
                throw new InvalidOperationException("Ya existe un libro con este nombre.");

            LibrosRecetas.Add(nombreLibro, new List<Receta>());
        }

        public void AgregarRecetaALibro(string nombreLibro, Receta receta)
        {
            if (!LibrosRecetas.ContainsKey(nombreLibro))
                throw new KeyNotFoundException($"El libro '{nombreLibro}' no existe.");

            LibrosRecetas[nombreLibro].Add(receta);
        }

        public void EliminarLibro(string nombreLibro)
        {
            /* NO LO PIDE, pero evitaria que elimine algo que no existe
            if (!LibrosRecetas.ContainsKey(nombreLibro))
                throw new KeyNotFoundException($"El libro '{nombreLibro}' no existe.");
            */

            LibrosRecetas.Remove(nombreLibro);
        }

        public List<Receta> ObtenerLibro(string nombreLibro)
        {
            /* NO LO PIDE, pero lanzaria una excepcion si la clave no existiera
            if (!LibrosRecetas.ContainsKey(nombreLibro))
                throw new KeyNotFoundException($"El libro '{nombreLibro}' no existe.");
             */

            return LibrosRecetas[nombreLibro];
        }

        /*
        /    El metodo antes implementado buscaba en un solo libro, este busca todas las recetas
        */
        public int ContarRecetas() // va libro a libro contando las recetas
        {
            int total = 0;
            foreach (var lista in LibrosRecetas.Values)
            {
                total += lista.Count;
            }
            return total;
        }
        public void MostrarLibros()
        {
            if (LibrosRecetas.Count == 0) // Revisa el numero de libros, si es 0 imprime y termina ahí
            {
                Console.WriteLine("No hay libros registrados.");
                return;
            }

            foreach (var libro in LibrosRecetas)        // Recorre los libros en el diccionario
            {
                Console.WriteLine($"Libro: {libro.Key}");

                foreach (var receta in libro.Value)     // Recorre las recetas de los libros
                {
                    Console.WriteLine("   - " + receta.ToString());
                }
            }
        }
    }
}