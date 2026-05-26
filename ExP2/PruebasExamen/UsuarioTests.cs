using Xunit;
using APP.MODELOS;

namespace PruebasExamen;

public class UsuarioTests
{
    // 1. CrearLibroRecetas(nombre)
    [Fact]
    public void CrearLibroRecetas_CreacionDeLista()
    {
        var u = new Usuario("Luis");
        u.CrearLibroRecetas("Favoritas");

        Assert.True(u.LibrosRecetas.ContainsKey("Favoritas"));
        Assert.Empty(u.LibrosRecetas["Favoritas"]);
    }

    [Fact]
    public void CrearLibroRecetas_Duplicado_Exception()
    {
        var u = new Usuario("Luis");
        u.CrearLibroRecetas("Favoritas");

        Assert.Throws<InvalidOperationException>(() => u.CrearLibroRecetas("Favoritas"));
    }

    // 2. AgregarRecetaALibro(nombreLibro, receta)
    [Fact]
    public void AgregarRecetaALibro_DebeAñadirReceta()
    {
        var u = new Usuario("Luis");
        u.CrearLibroRecetas("Favoritas");
        var receta = new Receta("Paella", "Chef Ramirez", 45);

        u.AgregarRecetaALibro("Favoritas", receta);

        Assert.Single(u.LibrosRecetas["Favoritas"]);
    }

    [Fact]
    public void AgregarRecetaALibro_ListaInexistente_Exception()
    {
        var u = new Usuario("Luis");
        var receta = new Receta("Paella", "Chef Ramirez", 45);

        Assert.Throws<KeyNotFoundException>(() => u.AgregarRecetaALibro("Inexistente", receta));
    }

    // 3. ContarRecetas()  (sin parámetros)
    [Fact]
    public void ContarRecetas_DebeSumarCorrectamente()
    {
        var u = new Usuario("Luis");
        u.CrearLibroRecetas("Libro1");
        u.CrearLibroRecetas("Libro2");

        u.AgregarRecetaALibro("Libro1", new Receta("R1", "C1", 10));
        u.AgregarRecetaALibro("Libro2", new Receta("R2", "C2", 20));

        Assert.Equal(2, u.ContarRecetas());
    }
}
