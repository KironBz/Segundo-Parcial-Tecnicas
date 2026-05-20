
using Xunit;
using APP.MODELOS;

// using

namespace PruebasExamen
{
    public class RecetaTest
    {
        // 1. CONSTRUCTOR

        [Fact]
        public void PruebasDeReceta_DebeImplementarUnaReceta()
        {
            // Arrange
            var receta = new Receta("Paella", "Chef Ramirez", 45);

            // Assert
            Assert.Equal("Paella", receta.Nombre);
            Assert.Equal("Chef Ramirez", receta.Chef);
            Assert.Equal(45, receta.TiempoMinutos);

        }

    // 2. ToString()
    
        [Fact]
        public void ToString_RegresaFormatoCorrecto()
        {
            // Arrange
            var receta = new Receta("Paella", "Chef Ramirez", 45);

            // Act, Assert 
            Assert.Equal("Paella - Chef Ramirez(45 min)", receta.ToString());
        }

    // 3. TiempoMinutos negativo o cero

        [Fact]

        public void TiempoMinutos_EvaluaQueNoExistaAmbiguedadDeTiempo()
        {
            // Assert
            Assert.Throws<ArgumentException>(() => new Receta("Test", "Chef", -1));
        }

    }
}