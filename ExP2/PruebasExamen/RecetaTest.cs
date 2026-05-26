
using Xunit;
using APP.MODELOS;
using System;  //  añadido por claridad para ArgumentException

namespace PruebasExamen
{
    public class RecetaTest
    {
        // 1. CONSTRUCTOR
        [Fact]
        public void PruebasDeReceta_DebeImplementarUnaReceta()
        {
            var receta = new Receta("Paella", "Chef Ramirez", 45);

            Assert.Equal("Paella", receta.Nombre);
            Assert.Equal("Chef Ramirez", receta.Chef);
            Assert.Equal(45, receta.TiempoMinutos);
        }

        // 2. ToString()
        [Fact]
        public void ToString_RegresaFormatoCorrecto()
        {
            var receta = new Receta("Paella", "Chef Ramirez", 45);
            // Formato  "Nombre - Chef (XX min)"
            Assert.Equal("Paella - Chef Ramirez (45 min)", receta.ToString());
        }

        // 3. TiempoMinutos negativo o cero
        [Fact]
        public void TiempoMinutos_NegativoOCero_LanzaArgumentException()
        {
            Assert.Throws<ArgumentException>(() => new Receta("Test", "Chef", -1));
        }
    }
}