using APP.INTERFACES;
using APP.MODELOS;
using System.Text;

namespace APP.SERVICIOS
{
    public class ExportadorTxT : IExportador
    {
        /*          CORRECCIONES
        / - Se corrige el nombre de clase ExportadorTxt
        / - La firma del método se modifica
        / - Se cambia el retorno a void (no bool)
        / - Se exportan TODOS los libros del usuario
        / - Se incluye fecha en el encabezado
        / - Se usa receta.ToString() para el formato
        */
        public void ExportarATxt(Usuario usuario, string rutaArchivo)
        {
            // Validaciones opcionales
            if (usuario == null)
            {
                throw new ArgumentNullException("usuario");
            }
            if (string.IsNullOrWhiteSpace(rutaArchivo))
            {
                throw new ArgumentException("La ruta del archivo no puede estar vacía.", "rutaArchivo");
            }
            using (StreamWriter writer = new StreamWriter(rutaArchivo, false, Encoding.UTF8))
            {
                // Encabezado con nombre y fecha
                writer.WriteLine($"Usuario: {usuario.Nombre}");
                writer.WriteLine($"Fecha de exportación: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
                writer.WriteLine();
                if (usuario.LibrosRecetas.Count == 0)
                {
                    writer.WriteLine("El usuario no tiene libros registrados.");
                }
                else
                {
                    // Recorre todos los libros
                    foreach (var libro in usuario.LibrosRecetas)
                    {
                        writer.WriteLine($"Libro: {libro.Key}");

                        foreach (var receta in libro.Value)
                        {
                            // Usar ToString() de Receta (formato exacto)
                            writer.WriteLine($"  - {receta.ToString()}");
                        }
                        writer.WriteLine();
                    }
                }
            }
        }
    }
}
