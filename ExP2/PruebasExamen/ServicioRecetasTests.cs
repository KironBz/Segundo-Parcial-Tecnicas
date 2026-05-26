using Xunit;
using System;
using System.IO;
using APP.MODELOS;
using APP.GESTORES;
using APP.SERVICIOS;
using APP.INTERFACES;

namespace PruebasExamen
{
    public class ServicioRecetasTests : IDisposable
    {
        private readonly IGestorRecetas _gestor;
        private readonly IExportador _exportador;
        private readonly ServicioRecetas _servicio;
        private readonly string _rutaPrueba = "PruebaExportacion.txt";

        // Constructor: se ejecuta antes de cada prueba
        public ServicioRecetasTests()
        {
            _gestor = new GestorRecetas();
            _exportador = new ExportadorTxt();   // ✅ nombre corregido
            _servicio = new ServicioRecetas(_gestor, _exportador);
        }

        // Limpieza: se ejecuta después de cada prueba
        public void Dispose()
        {
            if (File.Exists(_rutaPrueba))
                File.Delete(_rutaPrueba);
        }

        // ==========================================
        // 1. RegistrarUsuario(nombre)
        // ==========================================
        [Fact]
        public void RegistrarUsuario_AgregaUsuarioALista()
        {
            // Arrange
            string nombre = "TestUser";

            // Act
            var usuario = _servicio.RegistrarUsuario(nombre);

            // Assert
            Assert.NotNull(_servicio.BuscarUsuario(nombre));
            Assert.Equal(1, _servicio.ContarUsuarios());
            Assert.Equal(nombre, usuario.Nombre);
        }

        // ==========================================
        // 2. BuscarUsuario(nombre)
        // ==========================================
        [Fact]
        public void BuscarUsuario_UsuarioExistente_RetornaUsuario()
        {
            // Arrange
            _servicio.RegistrarUsuario("Ana");

            // Act
            var encontrado = _servicio.BuscarUsuario("ANA");  // case-insensitive

            // Assert
            Assert.NotNull(encontrado);
            Assert.Equal("Ana", encontrado.Nombre);
        }

        [Fact]
        public void BuscarUsuario_UsuarioInexistente_RetornaNull()
        {
            // Arrange (sin registrar usuarios)

            // Act
            var encontrado = _servicio.BuscarUsuario("Inexistente");

            // Assert
            Assert.Null(encontrado);
        }

        // ==========================================
        // 3. ExportarLibros(usuario, ruta)
        // ==========================================
        [Fact]
        public void ExportarLibros_CreaArchivoConNombreYRecetas()
        {
            // Arrange
            var usuario = _servicio.RegistrarUsuario("Carlos");
            usuario.CrearLibroRecetas("Favoritas");
            usuario.AgregarRecetaALibro("Favoritas", new Receta("Paella", "Chef Ramirez", 45));
            usuario.AgregarRecetaALibro("Favoritas", new Receta("Tacos", "Chef Silva", 30));

            // Act
            _exportador.ExportarATxt(usuario, _rutaPrueba);

            // Assert
            Assert.True(File.Exists(_rutaPrueba));
            string contenido = File.ReadAllText(_rutaPrueba);
            Assert.Contains("Carlos", contenido);
            Assert.Contains("Paella", contenido);
            Assert.Contains("Tacos", contenido);
        }
    }
}