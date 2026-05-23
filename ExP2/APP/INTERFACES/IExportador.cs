using APP.MODELOS;

namespace APP.INTERFACES
{
    public interface IExportador
    {
        void ExportarATxt(Usuario usuario, string rutaArchivo);
    }
}
