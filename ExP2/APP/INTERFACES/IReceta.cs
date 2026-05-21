namespace APP.INTERFACES
{
    public interface IReceta
    {
        // Interface Receta -> Contrato para Clase RecetA
        // Contrato con propiedades

        // Atributos y Propiedades
        string Nombre { get; } // Sin sset para que sean solo de lectura
        string Chef { get; }
        int TiempoMinutos { get; }

        // Metodos
        string ToString(); // En la clase se podra establecer el formato
    }
}
