using Xunit;
using APP.GESTORES;
using APP.MODELOS;

namespace PruebasExamen;

    public class GestorRecetasTests
    {
        // 1. Agregar Receta / Eliminar Receta

        [Fact]
        public void AgregarReceta_AumentaCount_NoPermiteDuplicados()
        {
            // Arrange
            var g = new GestorRecetas();
            var receta = new Receta("Paella Valenciana", "Chef Ramirez", 45);
            
            // Act
            g.AgregarReceta(receta);
            g.AgregarReceta(receta);

            // Assert
            Assert.Equal(1, g.RecetasDisponibles.Count);
        }

        [Fact]
    public void EliminarReceta_DisminuyeCount()
    {
        // Arrange
        var g = new GestorRecetas(); 
        var receta = new Receta("Paella", "Chef Ramirez", 45);
        g.AgregarReceta(receta);

        // Act
        g.EliminarReceta("Paella"); 

        // Assert
        Assert.Equal(0, g.RecetasDisponibles.Count);
    }

    // 2. BuscarPorNombre(Nombre)

    [Fact]
        public void BuscarPorNombre_BusquedaParcial_caseInsensitive()
        {
            // Arrange
            var g = new GestorRecetas();
            g.AgregarReceta(new Receta("Paella Valenciana", "Chef A", 45));
            g.AgregarReceta(new Receta("Tacos Al Pastor", "Chef B", 20));


            // Act 
            var resultados = g.BuscarPorNombre("paella");
            
            //Assert 
            Assert.Contains(resultados, r => r.Nombre == "Paella Valenciana");
        }

        [Fact]
        public void BuscarPorNombre_NoHayCoincidencias()
        {
            // Arrange
            var g = new GestorRecetas();
            g.AgregarReceta(new Receta("Paella", "Chef A", 45));

            // Act
            var resultados = g.BuscarPorNombre("Pizza");

            // Assert
            Assert.Empty(resultados);
        }
    }
