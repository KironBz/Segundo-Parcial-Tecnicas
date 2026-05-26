using Xunit;
using System;
using System.Collections.Generic;
using APP.GESTORES;
using APP.MODELOS;

namespace PruebasExamen
{
    public class GestorRecetasTests
    {
        // 1. AgregarReceta / EliminarReceta
        [Fact]
        public void AgregarReceta_AumentaCount_NoPermiteDuplicados()
        {
            var g = new GestorRecetas();
            var receta = new Receta("Paella", "Chef A", 45);

            g.AgregarReceta(receta);
            Assert.Equal(1, g.RecetasDisponibles.Count);

            // Duplicado no se agrega
            Assert.Throws<InvalidOperationException>(() => g.AgregarReceta(receta));
            Assert.Equal(1, g.RecetasDisponibles.Count);
        }

        [Fact]
        public void EliminarReceta_DisminuyeCount()
        {
            var g = new GestorRecetas();
            var receta = new Receta("Paella", "Chef A", 45);
            g.AgregarReceta(receta);

            g.EliminarReceta(receta);
            Assert.Equal(0, g.RecetasDisponibles.Count);
        }

        // 2. BuscarPorNombre (búsqueda parcial, case-insensitive)
        [Fact]
        public void BuscarPorNombre_BusquedaParcial_CaseInsensitive()
        {
            var g = new GestorRecetas();
            g.AgregarReceta(new Receta("Paella Valenciana", "Chef A", 45));
            g.AgregarReceta(new Receta("Tacos al Pastor", "Chef B", 20));

            var resultados = g.BuscarPorNombre("paella");

            Assert.Contains(resultados, r => r.Nombre == "Paella Valenciana");
        }

        [Fact]
        public void BuscarPorNombre_SinCoincidencias_RetornaListaVacia()
        {
            var g = new GestorRecetas();
            g.AgregarReceta(new Receta("Paella", "Chef A", 45));

            var resultados = g.BuscarPorNombre("Pizza");

            Assert.Empty(resultados);
        }

        // 3. QuickSort (ordena in-place por TiempoMinutos ascendente)
        [Fact]
        public void QuickSort_OrdenaListaPorTiempoAscendente()
        {
            var g = new GestorRecetas();
            var lista = new List<Receta>
            {
                new Receta("Risotto", "Chef C", 50),
                new Receta("Tacos", "Chef B", 20),
                new Receta("Paella", "Chef A", 45)
            };

            g.QuickSort(lista);

            Assert.True(lista[0].TiempoMinutos <= lista[1].TiempoMinutos);
            Assert.True(lista[1].TiempoMinutos <= lista[2].TiempoMinutos);
        }

        // 3. MergeSort (retorna nueva lista ordenada, no modifica original)
        [Fact]
        public void MergeSort_RetornaNuevaListaOrdenada_NoModificaOriginal()
        {
            var g = new GestorRecetas();
            var original = new List<Receta>
            {
                new Receta("Risotto", "Chef C", 50),
                new Receta("Tacos", "Chef B", 20),
                new Receta("Paella", "Chef A", 45)
            };
            var copiaOriginal = new List<Receta>(original);

            var ordenada = g.MergeSort(original);

            // Verifica orden
            Assert.True(ordenada[0].TiempoMinutos <= ordenada[1].TiempoMinutos);
            Assert.True(ordenada[1].TiempoMinutos <= ordenada[2].TiempoMinutos);
            // Verifica que la lista original no fue modificada (mantiene el orden original)
            Assert.Equal(50, original[0].TiempoMinutos);
            Assert.Equal(20, original[1].TiempoMinutos);
            Assert.Equal(45, original[2].TiempoMinutos);
        }

        // 4. BusquedaBinaria
        [Fact]
        public void BusquedaBinaria_RecetaExistente_RetornaIndiceValido()
        {
            var g = new GestorRecetas();
            g.AgregarReceta(new Receta("Paella", "Chef A", 45));
            g.AgregarReceta(new Receta("Tacos", "Chef B", 20));

            int indice = g.BusquedaBinaria("Tacos");

            Assert.True(indice >= 0);
        }

        [Fact]
        public void BusquedaBinaria_RecetaInexistente_RetornaMenosUno()
        {
            var g = new GestorRecetas();
            g.AgregarReceta(new Receta("Paella", "Chef A", 45));

            int indice = g.BusquedaBinaria("RecetaXYZInexistente");

            Assert.Equal(-1, indice);
        }
    }
}