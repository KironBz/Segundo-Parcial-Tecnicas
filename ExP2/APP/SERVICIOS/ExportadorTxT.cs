using APP.INTERFACES;
using APP.MODELOS;

namespace APP.SERVICIOS
{
    public class ExportadorTxT : IExportador
    {
        public bool ExportarATxt(Usuario usuario, string nombreLibro)
        {
            if (usuario == null) throw new ArgumentNullException(nameof(usuario));
            if (string.IsNullOrWhiteSpace(nombreLibro)) throw new ArgumentException("El libro no puede estar vacío.");

            // Validamos que el usuario realmente sea dueño del libro de recetas
            if (!usuario.LibrosRecetas.ContainsKey(nombreLibro))
            {
                return false;
            }

            try
            {
                // Nombre del archivo que exige el caso de prueba
                string rutaArchivo = $"{usuario.Nombre}_{nombreLibro}.txt";
                var recetas = usuario.LibrosRecetas[nombreLibro];

                // Escritura física del archivo .txt
                using (StreamWriter writer = new StreamWriter(rutaArchivo))
                {
                    // Cumplimos con el test: "ContieneNombreUsuario"
                    writer.WriteLine($"Usuario Propietario: {usuario.Nombre}");
                    writer.WriteLine($"Libro de Recetas: {nombreLibro}");

                    foreach (var receta in recetas)
                    {
                        // Cumplimos con el test: "ContieneNombreReceta"
                        writer.WriteLine($"- Receta: {receta.Nombre} | Chef: {receta.Chef} | Duración: {receta.TiempoMinutos} min");
                    }
                }

                return true; // Si se creó el archivo con éxito, retorna true (Pasa el test 'CreaArchivo')
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
