using APP.MODELOS;
namespace APP.INTERFACES
{
    /*          CORRECCIONES
    / Se modifico el retorno del metodo
    / Se reorganizo el parametro string del metodo
    */
    public interface IExportador
    {
        // Metodos
        void ExportarATxt(Usuario usuario, string rutaArchivo);
    }
}
