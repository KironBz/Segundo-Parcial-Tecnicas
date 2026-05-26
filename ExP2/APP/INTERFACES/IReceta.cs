namespace APP.INTERFACES
{
    public interface IReceta        // Contrato para la Clase RecetA
    {
        // Atributos y Propiedades
        
        /*
        /   Mejoramos las propiedaes de lectura y escritura
        /   Añadimos el metodo ToString faltante
        */

        string Nombre { get; } // Sin set para que sean solo de lectura
        string Chef { get; }
        int TiempoMinutos { get; }

        // Metodos
        string ToString(); // En la clase se podra establecer el formato
    }
}
