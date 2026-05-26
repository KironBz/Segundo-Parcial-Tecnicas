using APP.MODELOS;
namespace APP.INTERFACES

{
    /*
    / Se declaran los metodos a usar
    / Se cambia la declaracion class por interface (sintaxis)
    / Se corrigen nombres de los metodos y se eliminan repeticiones
    / Se elimina 'public' al ser una redundancia en los metodos de la interfaz
    / Faltaba metodo merge sort su parametro era una lista, no una clase
    / La busqueda binaria debia regresar un indice
    / Se elimina un metodo no solicitado
    */

    public interface IGestorRecetas // Interface gestor -> Contrato con el gestor
    {
        // Atributos y Propiedades
        List<Receta> RecetasDisponibles { get; set; }

        // Metodos
        // Se implementan los metodos que debera llevar por contrato la clase del gestor de recetas y se 
        void AgregarReceta(Receta receta);
        void EliminarReceta(Receta receta);
        void EliminarPorIndice(int indice);

        List<Receta> BuscarPorNombre(string nombre);

        void LimpiarCatalogo();

        void QuickSort(List<Receta> lista);
        List<Receta> MergeSort(List<Receta> lista);     // No estaba implementado en los comentarios
        int BusquedaBinaria(string nombre);      // Se establecio el nombre correcto

        // Se elimino busqueda por tiempo
    }
}
