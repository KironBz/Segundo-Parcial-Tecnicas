using System;
using System.Collections.Generic;
using APP.MODELOS;
using APP.GESTORES;
using APP.SERVICIOS;
using APP.INTERFACES;

/*
/ Se modifico el program para cumplir con los requerimentos de las diapositivas de la 12 a la 15 debido a que antes estaba vacio
*/

namespace APP
{
    class Program
    {
        static void Main(string[] args)
        {
            // ==========================================
            // INYECCIÓN DE DEPENDENCIAS
            // ==========================================

            IGestorRecetas gestor = new GestorRecetas();
            IExportador exportador = new ExportadorTxt();
            ServicioRecetas servicio = new ServicioRecetas(gestor, exportador);

            // Carga de las 8 recetas de ejemplo (PDF página 12)
            CargarRecetasPorDefecto(gestor);

            // Registro de usuario (PDF página 12)
            Console.WriteLine("--- REGISTRO DE USUARIO ---");
            Console.Write("Por favor, ingrese su nombre de usuario: ");
            string nombreUsuario = Console.ReadLine();
            Usuario usuarioActual = servicio.RegistrarUsuario(nombreUsuario);
            Console.WriteLine($"Usuario '{usuarioActual.Nombre}' registrado. ¡Bienvenido/a, {usuarioActual.Nombre}!");

            Console.Write("Ingrese un nombre para su primer libro de recetas: ");
            string primerLibro = Console.ReadLine();
            usuarioActual.CrearLibroRecetas(primerLibro);
            Console.WriteLine($"Libro '{primerLibro}' creado exitosamente.\n");

            string libroActual = primerLibro;  // libro seleccionado actualmente

            bool salir = false;
            while (!salir)
            {
                Console.Clear();
                Console.WriteLine("==================================================");
                Console.WriteLine("       SISTEMA DE GESTIÓN DE RECETAS DE COCINA     ");
                Console.WriteLine("==================================================");
                Console.WriteLine($"--- MENÚ PRINCIPAL --- Libro actual: '{libroActual}' ({usuarioActual.ObtenerLibro(libroActual).Count} recetas)");
                Console.WriteLine("1. Mostrar recetas disponibles");
                Console.WriteLine("2. Ordenar libro actual (QuickSort o MergeSort) y mostrar tiempo total");
                Console.WriteLine("3. Búsqueda binaria en catálogo");
                Console.WriteLine("4. Crear nuevo libro de recetas");
                Console.WriteLine("5. Cambiar de libro actual");
                Console.WriteLine("6. Ver mis libros");
                Console.WriteLine("7. Exportar mis libros a archivo .txt");
                Console.WriteLine("8. Salir");
                Console.WriteLine("--------------------------------------------------");
                Console.Write("Seleccione una opción: ");

                string opcion = Console.ReadLine();
                Console.WriteLine();

                // Validación de entrada numérica (PDF página 15)
                if (!int.TryParse(opcion, out int opcionNum))
                {
                    Console.WriteLine("Error: Debe ingresar un número. Presione cualquier tecla...");
                    Console.ReadKey();
                    continue;
                }

                switch (opcionNum)
                {
                    case 1:
                        // Mostrar catálogo global (recetas disponibles)
                        MostrarCatalogo(gestor.RecetasDisponibles);
                        break;

                    case 2:
                        // Ordenar libro actual y mostrar tiempo total
                        Console.Write("¿Qué algoritmo desea usar? (quick/merge): ");
                        string alg = Console.ReadLine()?.ToLowerInvariant();
                        if (alg != "quick" && alg != "merge")
                        {
                            Console.WriteLine("Algoritmo no válido. Use 'quick' o 'merge'.");
                            break;
                        }

                        List<Receta> libro = usuarioActual.ObtenerLibro(libroActual);
                        int tiempoTotal = 0;

                        if (alg == "quick")
                        {
                            // QuickSort modifica la lista original
                            tiempoTotal = servicio.OrdenarLibroYCalcularTiempo(usuarioActual, libroActual);
                            Console.WriteLine($"Libro ordenado con QuickSort. Tiempo total: {tiempoTotal} min");
                        }
                        else // merge
                        {
                            // MergeSort retorna nueva lista
                            List<Receta> ordenada = gestor.MergeSort(libro);
                            // Reemplazar contenido del libro original
                            libro.Clear();
                            foreach (var r in ordenada)
                                libro.Add(r);
                            // Calcular suma manualmente (o usar método auxiliar)
                            tiempoTotal = 0;
                            foreach (var r in libro)
                                tiempoTotal += r.TiempoMinutos;
                            Console.WriteLine($"Libro ordenado con MergeSort. Tiempo total: {tiempoTotal} milisegundos");
                        }
                        break;

                    case 3:
                        // Búsqueda binaria en catálogo (PDF página 13)
                        Console.Write("Ingrese el nombre exacto de la receta a buscar: ");
                        string nombreBuscar = Console.ReadLine();
                        int indice = gestor.BusquedaBinaria(nombreBuscar);
                        if (indice != -1)
                        {
                            // Mostrar opciones y permitir agregar al libro actual (PDF página 13)
                            Console.WriteLine($"Receta encontrada en índice {indice}: {gestor.RecetasDisponibles[indice].ToString()}");
                            Console.Write("¿Desea agregarla al libro actual? (s/n): ");
                            if (Console.ReadLine()?.ToLowerInvariant() == "s")
                            {
                                try
                                {
                                    usuarioActual.AgregarRecetaALibro(libroActual, gestor.RecetasDisponibles[indice]);
                                    Console.WriteLine("Receta agregada exitosamente.");
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine($"Error: {ex.Message}");
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("Receta no encontrada en el catálogo.");
                        }
                        break;

                    case 4:
                        // Crear nuevo libro
                        Console.Write("Nombre del nuevo libro: ");
                        string nuevoLibro = Console.ReadLine();
                        try
                        {
                            usuarioActual.CrearLibroRecetas(nuevoLibro);
                            Console.WriteLine($"Libro '{nuevoLibro}' creado.");
                        }
                        catch (InvalidOperationException ex)
                        {
                            Console.WriteLine($"Error: {ex.Message}");
                        }
                        break;

                    case 5:
                        // Cambiar de libro actual
                        if (usuarioActual.LibrosRecetas.Count == 0)
                        {
                            Console.WriteLine("No hay libros disponibles. Cree uno primero.");
                        }
                        else
                        {
                            Console.WriteLine("Libros disponibles:");
                            foreach (var libroName in usuarioActual.LibrosRecetas.Keys)
                            {
                                Console.WriteLine($"- {libroName}");
                            }
                            Console.Write("Ingrese el nombre del libro al que desea cambiar: ");
                            string selected = Console.ReadLine();
                            if (usuarioActual.LibrosRecetas.ContainsKey(selected))
                            {
                                libroActual = selected;
                                Console.WriteLine($"Ahora está trabajando en el libro '{libroActual}'.");
                            }
                            else
                            {
                                Console.WriteLine("El libro no existe.");
                            }
                        }
                        break;

                    case 6:
                        // Ver mis libros (mostrar todos con recetas)
                        usuarioActual.MostrarLibros();
                        break;

                    case 7:
                        // Exportar a .txt (todos los libros)
                        string rutaExportacion = $"{usuarioActual.Nombre}_Exportacion.txt";
                        try
                        {
                            exportador.ExportarATxt(usuarioActual, rutaExportacion);
                            Console.WriteLine($"Archivo exportado correctamente: {rutaExportacion}");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error al exportar: {ex.Message}");
                        }
                        break;

                    case 8:
                        salir = true;
                        Console.WriteLine("¡Gracias por utilizar el sistema!");
                        break;

                    default:
                        Console.WriteLine("Opción no válida. Presione cualquier tecla...");
                        break;
                }

                if (!salir)
                {
                    Console.WriteLine("\nPresione cualquier tecla para continuar...");
                    Console.ReadKey();
                }
            }
        }

        // ==========================================
        // MÉTODOS AUXILIARES
        // ==========================================

        static void MostrarCatalogo(List<Receta> recetas)
        {
            Console.WriteLine("--- RECETAS DISPONIBLES EN EL CATÁLOGO ---");
            if (recetas.Count == 0)
            {
                Console.WriteLine("(No hay recetas disponibles)");
                return;
            }
            for (int i = 0; i < recetas.Count; i++)
            {
                Console.WriteLine($"[{i}] {recetas[i].ToString()}");
            }
        }

        static void CargarRecetasPorDefecto(IGestorRecetas gestor)
        {
            // Según PDF página 12: 8 recetas específicas
            gestor.AgregarReceta(new Receta("Paella", "Chef Ramirez", 45));
            gestor.AgregarReceta(new Receta("Tacos", "Chef Silva", 30));
            gestor.AgregarReceta(new Receta("Risotto", "Chef Bianchi", 50));
            gestor.AgregarReceta(new Receta("Ceviche", "Chef Gomez", 20));
            gestor.AgregarReceta(new Receta("Ramen", "Chef Tanaka", 90));
            gestor.AgregarReceta(new Receta("Guacamole", "Chef Lopez", 10));
            gestor.AgregarReceta(new Receta("Croissant", "Chef Dupont", 120));
            gestor.AgregarReceta(new Receta("Tiramisu", "Chef Rossi", 40));
        }
    }
}
