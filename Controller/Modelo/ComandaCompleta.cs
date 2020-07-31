namespace Controller.Controles
{
    public class ComandaCompleta
    {
        public string fecha { get; set; }
        public int id_pedido { get; set; }
        public int codigo_ticket { get; set; }
        public string dni_empleado { get; set; }
        public decimal precio_total { get; set; }
        public int comensales { get; set; }
        public string nombre { get; set; }
        public string apellidos { get; set; }
        public string comentario { get; set; }

        public ComandaCompleta(string fecha, int id_pedido, int codigo_ticket, string dni_empleado
            , decimal precio_total, int comensales, string nombre, string apellidos, string comentario)
        {
            this.fecha = fecha;
            this.id_pedido = id_pedido;
            this.codigo_ticket = codigo_ticket;
            this.dni_empleado = dni_empleado;
            this.precio_total = precio_total;
            this.comensales = comensales;
            this.nombre = nombre;
            this.apellidos = apellidos;
            this.comentario = comentario;
        }
    }
}