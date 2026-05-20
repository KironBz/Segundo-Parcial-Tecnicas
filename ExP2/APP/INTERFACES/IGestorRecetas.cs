using APP.MODELOS;

namespace APP.INTERFACES

{
    public class IGestorRecetas // Interface gestor -> Contrato con el gestor
    {
        // Propiedades
        public List<Receta> RecetasDisponibles { get; set; }
        
        public void AgregarReceta(Receta receta)
        {

        }
        public void EliminarReceta(Receta receta)
        {

        }
        public void EliminarPorIndice(int indice)
        {

        }
        public List<Receta> BuscarPorNombre(string nombre)
        {

        }
        public void LimpiarCatalogo()
        {

        }
        public void QuickSort(List<Receta> lista)
        {

        }

        public void MergeSort(Receta receta)
        {

        }

        public void BusquedaBinaria(string nombre)
        {

        }

        // public Receta BuscarPorNombre(string nombre); // Implementará Búsqueda Binaria

        // Aqui hay problemas
        public void OrdenarPorTiempo() { } // Implementará QuickSort o MergeSort
        
    }
}
