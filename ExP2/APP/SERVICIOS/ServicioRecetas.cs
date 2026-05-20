
using APP.MODELOS;
using APP.INTERFACES;

namespace APP.SERVICIOS
{
    public class ServicioRecetas
    {
        public IGestorRecetas Gestor {  get; set; }
        public IExportador Exportador { get; set; }
        public List<Usuario> Usuarios { get; set; }

        // Constructor
        public ServicioRecetas(IGestorRecetas gestor, IExportador exportador, List<Usuario> usuarios)
        {
            Gestor = gestor;
            Exportador = exportador;
            Usuarios = usuarios;
        }

        public Usuario RegistrarUsuario(string nombre)
        {
            if (string.IsNullOrEmpty(nombre))
            {
                throw new ArgumentNullException("Nombre Invalido");
            }

            var nuevoUsuario = new Usuario(nombre);
            Usuarios.Add(nuevoUsuario);
            return nuevoUsuario;
        }

        public Usuario BuscarUsuario(string nombre)
        {
            return Usuarios.FirstOrDefault(u => u.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase));
        }
        
        public int ContarUsuarios()
        {
            return Usuarios.Count();
        }

    }
}
