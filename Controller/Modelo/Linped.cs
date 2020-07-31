namespace Intermodular_MVC_VladimirIriarte.Modelo
{
    public class Linped
    {
        public int id_pedido { get; set; }
        public int linea { get; set; }
        public int cantidad { get; set; }
        public int codigo_producto { get; set; }
        public decimal importe { get; set; }

        public Linped(int id_pedido, int linea, int cantidad, int codigo_producto, decimal importe)
        {
            this.id_pedido = id_pedido;
            this.linea = linea;
            this.cantidad = cantidad;
            this.codigo_producto = codigo_producto;
            this.importe = importe;
        }
    }
}