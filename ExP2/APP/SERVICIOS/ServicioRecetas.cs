
using APP.INTERFACES;
using APP.MODELOS;
using APP.GESTORES;

namespace APP.SERVICIOS
{
    /*          CORRECCIONES
    / Se añadio using.APP.GESTORES
    / Se usa el gestor para aplicar QuickSort o MergeSort según el parámetro.
    / En OrdenarLibroYCalcularTiempo se ordena la lista del libro y se suma TiempoMinutos.
    / Se corrigieron lso metodos RegistrarUsuario, BuscarUsuario, contar usuarios
    / Se añadieron metodos faltantes: EliminarUsuario, OrdenarCatalogo, OrdenarLibroYCalcularTiempo
    / 
    */
    public class ServicioRecetas
    {
        // Atributos y Propiedades
        public IGestorRecetas Gestor { get; private set; }  // Propiedad publica
        public IExportador Exportador { get; private set; } // cualquiera lee, solo la clase puede modificarla
        public List<Usuario> Usuarios { get; private set; }

        // Constructor
        public ServicioRecetas(IGestorRecetas gestor, IExportador exportador)
        {
            Gestor = gestor;
            Exportador = exportador;
            Usuarios = new List<Usuario>();
        }

        // Metodos
        public Usuario RegistrarUsuario(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
            {
                throw new ArgumentException("El nombre no puede estar vacío.", "nombre");
            }
            Usuario nuevo = new Usuario(nombre);
            Usuarios.Add(nuevo);
            return nuevo;
        }

        public Usuario BuscarUsuario(string nombre)
        {
            foreach (var u in Usuarios)
            {
                if (string.Equals(u.Nombre, nombre, StringComparison.OrdinalIgnoreCase))
                {
                    return u;
                }
            }
            return null;
        }

        public bool EliminarUsuario(string nombre)
        {
            for (int i = 0; i < Usuarios.Count; i++)
            {
                if (string.Equals(Usuarios[i].Nombre, nombre, StringComparison.OrdinalIgnoreCase))
                {
                    Usuarios.RemoveAt(i);
                    return true;
                }
            }
            return false;
        }

        public int ContarUsuarios()
        {
            return Usuarios.Count();
        }

        public void OrdenarCatalogo(string algoritmo)
        {
            if (algoritmo == null)
            {
                return;
            }
            string alg = algoritmo.ToLowerInvariant();
            if (alg == "quick")
            {
                Gestor.QuickSort(Gestor.RecetasDisponibles);
                Console.WriteLine("Catálogo ordenado con QuickSort por tiempo.");
            }
            else if (alg == "merge")
            {
                // MergeSort regresa una nueva lista; se reemplaza la original
                List<Receta> nuevaLista = Gestor.MergeSort(Gestor.RecetasDisponibles);
                Gestor.RecetasDisponibles = nuevaLista;
                Console.WriteLine("Catálogo ordenado con MergeSort por tiempo.");
            }
            else
            {
                throw new ArgumentException("Algoritmo no reconocido. Use 'quick' o 'merge'.");
            }
        }

        public int OrdenarLibroYCalcularTiempo(Usuario usuario, string nombreLibro, string algoritmo)
        {
            if (usuario == null)
            {
                throw new ArgumentNullException("usuario");
            }

            // da una lista de recetas del libro
            List<Receta> libro = usuario.ObtenerLibro(nombreLibro);
            if (libro == null)
            {
                return 0;
            }

            // se usa el algoritmo de ordenamiento
            if (algoritmo == null)
            {
                return 0;
            }
            string alg = algoritmo.ToLowerInvariant();
            if (alg == "quick")
            {
                Gestor.QuickSort(libro);
            }
            else if (alg == "merge")
            {
                List<Receta> ordenada = Gestor.MergeSort(libro);
                // Reemplaza el libro original con la lista ordenada
                libro.Clear();
                foreach (var r in ordenada)
                {
                    libro.Add(r);
                }
            }
            else
            {
                throw new ArgumentException("Algoritmo no reconocido. Use 'quick' o 'merge'.");
            }
           
            // da la suma de tiempos
            int suma = 0;
            foreach (var receta in libro)
            {
                suma += receta.TiempoMinutos;
            }
            return suma;
        }
    }
}
