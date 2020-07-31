namespace Intermodular_MVC_VladimirIriarte.Modelo
{
    public class Producto
    {
        public int codigo { get; set; }
        public string nombre { get; set; }
        public string categoria { get; set; }
        public string especificaciones { get; set; }
        public decimal precio { get; set; }
        public bool activo { get; set; }
        public string imagen { get; set; }

        public Producto()
        {
        }

        public Producto(int codigo, string nombre, string categoria
             , string especificaciones, decimal precio, bool activo, string imagen)
        {
            this.codigo = codigo;
            this.nombre = nombre;
            this.categoria = categoria;
            this.especificaciones = especificaciones;
            this.precio = precio;
            this.activo = activo;
            this.imagen = imagen;
        }

        public Producto(string nombre, string categoria
            , string especificaciones, decimal precio, bool activo, string imagen)
        {
            this.nombre = nombre;
            this.categoria = categoria;
            this.especificaciones = especificaciones;
            this.precio = precio;
            this.activo = activo;
            this.imagen = imagen;
        }
    }
}