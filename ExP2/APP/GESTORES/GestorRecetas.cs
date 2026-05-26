using APP.INTERFACES;
using APP.MODELOS;

namespace APP.GESTORES
{
    /*          CORRECCIONES
    / Se modifican las instrucciones del metodo AgregarReceta
    / Se corrige el parametro de EliminarReceta y se modifica el algoritmo
    / Se corrige el metodo EliminarPorIndice
    / Corereccion del nombre del metodo "BuscarPorNombre"
    / Se agregan los algoritmos QuickSort y MergeSort
    / Se modifico el algoritmo de busqeuda binaria
    */

    public class GestorRecetas : IGestorRecetas
    {
        // Propiedades
        public List<Receta> RecetasDisponibles { get; set; }

        // Constructor
        public GestorRecetas()
        {
            RecetasDisponibles = new List<Receta>();
        }

        // Metodos 
        public void AgregarReceta(Receta receta)
        {
            // Solución a "sin implementación en programa principal": este método se usará al inicio (8 recetas por defecto)
            if (receta == null)
            {
                throw new ArgumentNullException("receta");
            }

            // Solución a "errores de sintaxis" y "LINQ no permitido": se reemplaza Any() por bucle manual
            foreach (var r in RecetasDisponibles)
            {
                if (string.Equals(r.Nombre, receta.Nombre, StringComparison.OrdinalIgnoreCase))
                {
                    throw new InvalidOperationException("La receta ya existe en el catálogo global.");
                }
            }
            RecetasDisponibles.Add(receta);
        }

        public void EliminarReceta(Receta receta)
        {
            if (receta == null)
            {
                return;
            }
            // Búsqueda manual sin LINQ
            int index = -1;     // -1  no encontrado
            for (int i = 0; i < RecetasDisponibles.Count; i++)
            {
                if (string.Equals(RecetasDisponibles[i].Nombre, receta.Nombre, StringComparison.OrdinalIgnoreCase))
                {
                    index = i;  // Guardamos la posición donde la encontramos
                    break;      // Nos salimos del ciclo de inmediato
                }
            }
            if (index != -1)
            {
                RecetasDisponibles.RemoveAt(index);
            }
        }

        public void EliminarPorIndice(int indice)
        {
            if (indice < 0 || indice >= RecetasDisponibles.Count)
            {
                throw new ArgumentOutOfRangeException("indice", "Índice fuera de los límites del catálogo.");
            }
            RecetasDisponibles.RemoveAt(indice);
        }

        public List<Receta> BuscarPorNombre(string nombre)
        {
            List<Receta> resultado = new List<Receta>();
            foreach (var r in RecetasDisponibles)
            {
                if (r.Nombre.IndexOf(nombre, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    resultado.Add(r);
                }
            }
            return resultado;
        }

        public void LimpiarCatalogo()
        {
            RecetasDisponibles.Clear();
        }

        // Algoritmos
        // 1ero - QuiclSort  -  Segun el tiempo se ordena de forma ascendente
        public void QuickSort(List<Receta> lista)
        {
            if (lista == null || lista.Count <= 1)
            {
                return;
            }
            QuickSortRecursivo(lista, 0, lista.Count - 1);
        }

        private void QuickSortRecursivo(List<Receta> lista, int izquierda, int derecha)
        {
            if (izquierda >= derecha)
            {
                return;
            }

            int pivote = Particionar(lista, izquierda, derecha);
            QuickSortRecursivo(lista, izquierda, pivote - 1);
            QuickSortRecursivo(lista, pivote + 1, derecha);
        }

        private int Particionar(List<Receta> lista, int izquierda, int derecha)
        {
            int pivoteValor = lista[derecha].TiempoMinutos;
            int i = izquierda - 1;
            for (int j = izquierda; j < derecha; j++)
            {
                if (lista[j].TiempoMinutos <= pivoteValor)
                {
                    i++;
                    // Intercambio manual
                    Receta temp = lista[i];
                    lista[i] = lista[j];
                    lista[j] = temp;
                }
            }

            // Intercambiar el pivote
            Receta temp2 = lista[i + 1];
            lista[i + 1] = lista[derecha];
            lista[derecha] = temp2;
            return i + 1;
        }


        // 2do - Regresa la lista ordenada?
        public List<Receta> MergeSort(List<Receta> lista)
        {
            if (lista == null)
            {
                return new List<Receta>();
            }
            if (lista.Count <= 1)
            {
                return new List<Receta>(lista);
            }

            int medio = lista.Count / 2;
            List<Receta> izquierda = new List<Receta>();
            List<Receta> derecha = new List<Receta>();

            for (int i = 0; i < medio; i++)
            {
                izquierda.Add(lista[i]);
            }
            for (int i = medio; i < lista.Count; i++)
            {
                derecha.Add(lista[i]);
            }
            izquierda = MergeSort(izquierda);
            derecha = MergeSort(derecha);
            return Mezclar(izquierda, derecha);
        }

        private List<Receta> Mezclar(List<Receta> izq, List<Receta> der)
        {
            List<Receta> resultado = new List<Receta>();
            int i = 0, j = 0;

            while (i < izq.Count && j < der.Count)
            {
                if (izq[i].TiempoMinutos <= der[j].TiempoMinutos)
                {
                    resultado.Add(izq[i]);
                    i++;
                }
                else
                {
                    resultado.Add(der[j]);
                    j++;
                }
            }
            while (i < izq.Count)
            {
                resultado.Add(izq[i++]);
            }
            while (j < der.Count)
            {
                resultado.Add(der[j++]);
            }
            return resultado;
        }

        // 3ro Busqueda Binaria
        public int BusquedaBinaria(string nombre)
        {
            List<Receta> copia = new List<Receta>(RecetasDisponibles);

            // Ordenar copia por nombre (burbuja)
            for (int i = 0; i < copia.Count - 1; i++)
            {
                for (int j = i + 1; j < copia.Count; j++)
                {
                    if (string.Compare(copia[i].Nombre, copia[j].Nombre, StringComparison.OrdinalIgnoreCase) > 0)
                    {
                        Receta temp = copia[i];
                        copia[i] = copia[j];
                        copia[j] = temp;
                    }
                }
            }

            // Búsqueda binaria manual
            int bajo = 0, alto = copia.Count - 1;
            while (bajo <= alto)
            {
                int medio = (bajo + alto) / 2;
                int comparacion = string.Compare(copia[medio].Nombre, nombre, StringComparison.OrdinalIgnoreCase);

                if (comparacion == 0)
                {
                    return medio;
                }
                if (comparacion < 0)
                {
                    bajo = medio + 1;
                }
                else
                {
                    alto = medio - 1;
                }
            }
            return -1;
        }
    }
}