using APP.MODELOS;

namespace APP.INTERFACES
{
    public interface IExportador
    {
        bool ExportarATxt(Usuario usuario, string nombreLibro);
    }
}
