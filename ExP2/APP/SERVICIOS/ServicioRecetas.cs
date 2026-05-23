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
        // Propiedades
        public IGestorRecetas Gestor { get; private set; }
        public IExportador Exportador { get; private set; }
        public List<Usuario> Usuarios { get; private set; }

        // Constructor
        public ServicioRecetas(IGestorRecetas gestor, IExportador exportador)
        {
            Gestor = gestor;
            Exportador = exportador;
            Usuarios = new List<Usuario>();
        }

        // Registrar usuario
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

        // Buscar usuario (case-insensitive)
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

        // Eliminar usuario
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

        // Contar usuarios (sin LINQ)
        public int ContarUsuarios()
        {
            return Usuarios.Count;   // ✅ propiedad, no método LINQ
        }

        // Ordenar catálogo global según algoritmo elegido
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
                List<Receta> nuevaLista = Gestor.MergeSort(Gestor.RecetasDisponibles);
                Gestor.RecetasDisponibles = nuevaLista;
                Console.WriteLine("Catálogo ordenado con MergeSort por tiempo.");
            }
            else
            {
                throw new ArgumentException("Algoritmo no reconocido. Use 'quick' o 'merge'.");
            }
        }

        public int OrdenarLibroYCalcularTiempo(Usuario usuario, string nombreLibro)
        {
            if (usuario == null)
            {
                throw new ArgumentNullException("usuario");
            }
            List<Receta> libro = usuario.ObtenerLibro(nombreLibro);
            if (libro == null)
            {
                return 0;
            }
            Gestor.QuickSort(libro);
            int suma = 0;
            foreach (var receta in libro)
            {
                suma += receta.TiempoMinutos;
            }
            return suma;
        }
    }
}